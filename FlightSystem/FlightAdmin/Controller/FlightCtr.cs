using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Windows.Forms;
using Common;
using Common.Exceptions;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    public class FlightCtr {


        #region Create

        /*
        public Flight CreateFlight(DateTime arrival, DateTime departure, Plane plane) {
            Flight flight = null;

            if (FlightValidation(arrival, departure, plane)) {
                flight = new Flight {ArrivalTime = arrival, DepartureTime = departure, Plane = plane};

                try {
                    using (var client = new FlightServiceClient()) {
                        flight.ID = client.AddFlight(flight);
                    }
                } catch (FaultException<DatabaseInsertFault> dbException) {
                    throw new Exception(dbException.Message);
                } catch (Exception e) {
                    throw new ConnectionException("WCF Service Exception", e);
                }
            } else {
                throw new Exception("FlightValidation Exception");
            }

            return flight;
        }
         */
        

        #endregion

        #region Read

        public Flight GetFlight(int id) {
            Flight flight = null;

            using (var client = new FlightServiceClient()) {
                try {
                    flight = client.GetFlight(id);
                } catch (FaultException<NullPointerFault>) {
                    throw new NullException("No Flights were found!");
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }

            return flight;
        }

        /// <exception cref="NullException">Condition.</exception>
        public List<Flight> GetFlights(Airport from, Airport to) {
            List<Flight> flights = null;

            using (var client = new FlightServiceClient()) {
                try {
                    flights = client.GetFlights(from, to);
                } catch (FaultException<NullPointerFault> nullException) {
                    nullException.DebugGetLine();
                    throw new NullException("No Flights were found!");
                } catch (Exception e) {
                    e.DebugGetLine();
                    Console.WriteLine(e.Message);
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }

            return flights;
        }

        #endregion

        #region Update

        public Flight UpdateFlight(Flight flight, DateTime arrival, DateTime departure, Plane plane) {
            Flight retFlight = null;

            if (flight == null) {
                flight = new Flight();
            }

            if (FlightValidation(arrival, departure, plane)) {
                using (var client = new FlightServiceClient()) {
                    try {
                        flight.ArrivalTime = arrival;
                        flight.DepartureTime = departure;
                        flight.Plane = plane;

                        retFlight = client.UpdateFlight(flight);
                    } catch (FaultException<OptimisticConcurrencyFault> concurrencyException) {
#if DEBUG
                        concurrencyException.DebugGetLine();
#endif
                        throw new DBConcurrencyException(concurrencyException.Detail.Message);
                    } catch (FaultException<DatabaseUpdateFault> updateException) {
#if DEBUG
                        updateException.DebugGetLine();
#endif
                        throw new DatabaseException(updateException.Detail.Message);
                    } catch (Exception e) {
#if DEBUG
                        e.DebugGetLine();
#endif
                        throw new ConnectionException("WCF Service Exception", e);
                    }
                }
            } else {
                throw new ValidationException("FlightValidation Exception");
            }

            return retFlight;
        }

        #endregion

        #region Delete

        public void DeleteFlight(Flight flight) {
            using (var client = new FlightServiceClient()) {
                try {
                    client.DeleteFlight(flight);
                } catch (FaultException<NullPointerFault> nullException) {
#if DEBUG
                    nullException.DebugGetLine();
#endif
                    throw new NullException(nullException.Detail.Message);
                } catch (FaultException<DatabaseDeleteFault> dbException) {
                    #if DEBUG
                    dbException.DebugGetLine();
#endif
                    throw new DatabaseException(dbException.Detail.Message);
                } catch (Exception e) {
#if DEBUG
                    e.DebugGetLine();
#endif
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }
        }

        #endregion   

        #region Misc

        private bool FlightValidation(DateTime arrival, DateTime departure, Plane plane) {
            var ret = (plane != null);

            return ret;
        }

        #endregion
    
    }
}