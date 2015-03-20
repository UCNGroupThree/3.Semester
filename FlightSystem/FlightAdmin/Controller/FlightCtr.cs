﻿using System;
using System.ServiceModel;
using System.Windows.Forms;
using FlightAdmin.Exceptions;
using FlightAdmin.FlightService;

namespace FlightAdmin.Controller {
    public class FlightCtr {

        #region Create

        public Flight CreateFlight(DateTime arrival, DateTime departure, Plane plane, decimal price) { //TODO Better Exception
            Flight flight = null;

            if (FlightValidation(arrival, departure, plane, price)) {
                flight = new Flight {ArrivalTime = arrival, DepartureTime = departure, Plane = plane, Price = price};

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

        #endregion

        #region Read

        public Flight GetFlight(int id) {
            Flight flight = null;

            using (var client = new FlightServiceClient()) {
                try {
                    flight = client.GetFlight(id);
                } catch (FaultException<NullPointerFault> nullException) {
                    throw new Exception(nullException.Message);
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }

            return flight;
        }

        #endregion

        #region Update

        public Flight UpdateFlight(Flight flight, DateTime arrival, DateTime departure, Plane plane, decimal price) { //TODO Better Exception
            Flight retFlight = null;

            if (FlightValidation(arrival, departure, plane, price)) {
                using (var client = new FlightServiceClient()) {
                    try {
                        flight.ArrivalTime = arrival;
                        flight.DepartureTime = departure;
                        flight.Plane = plane;
                        flight.Price = price;

                        retFlight = client.UpdateFlight(flight);
                    } catch (FaultException<OptimisticConcurrencyFault> concurrencyException) {
                        throw new Exception(concurrencyException.Message);
                    } catch (FaultException<DatabaseUpdateFault> updateException) {
                        throw new Exception(updateException.Message);
                    } catch (Exception e) {
                        throw new ConnectionException("WCF Service Exception", e);
                    }
                }
            } else {
                throw new Exception("FlightValidation Exception");
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
                    throw new Exception(nullException.Message);
                } catch (FaultException<DatabaseDeleteFault> dbException) {
                    throw new Exception(dbException.Message);
                } catch (Exception e) {
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }
        }

        #endregion   

        #region Misc

        private bool FlightValidation(DateTime arrival, DateTime departure, Plane plane, decimal price) {
            var ret = (plane != null);

            return ret;
        }

        #endregion
    
    }
}