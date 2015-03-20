using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.ServiceModel;
using FlightAdmin.Exceptions;
using FlightAdmin.RouteService;
using Flight = FlightAdmin.RouteService.Flight;

namespace FlightAdmin.Controller {
    public class RouteCtr {

        #region Create

        public Route CreateRoute(Airport from, Airport to, List<Flight> flights) { //TODO Better Exception
            Route route = null;

            if (RouteValidation(from, to, flights)) {
                route = new Route {From = from, To = to, Flights = flights};

                using (var client = new RouteServiceClient()) {
                    try {
                        route = client.AddRoute(route);
                    } catch (FaultException<DatabaseInsertFault> dbException) {
                        throw new Exception(dbException.Message);
                    } catch (Exception e) {
                        Console.WriteLine(e);
                        throw new ConnectionException("WCF Service Exception", e);
                    }
                }
            } else {
                throw new Exception("RouteValidation Exception");
            }

            return route;
        }

        #endregion

        #region Update

        public Route UpdateRoute(Route route, Airport from, Airport to, List<Flight> flights) { //TODO Better Exception
            Route retRoute = null;
            if (RouteValidation(from, to, flights)) {
                using (var client = new RouteServiceClient()) {
                    try {
                        route.From = from;
                        route.To = to;
                        route.Flights = flights;

                        retRoute = client.UpdateRoute(route);
                    } catch (FaultException<OptimisticConcurrencyFault> concurrencyException) {
                        throw new Exception(concurrencyException.Message);
                    } catch (FaultException<DatabaseUpdateFault> updateException) {
                        throw new Exception(updateException.Message);
                    } catch (Exception e) {
                        throw new ConnectionException("WCF Service Exception", e);
                    }
                }
            } else {
                throw new Exception("RouteValidation Exception");
            }

            return retRoute;
        }

        #endregion

        #region Delete

        public void DeleteRoute(Route route) { //TODO Better Exception
            using (var client = new RouteServiceClient()) { 
                try {
                    client.DeleteRoute(route);
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

        #region Read

        public Route GetRoute(int id) { //TODO Better Exception
            Route route = null;

            using (var client = new RouteServiceClient()) {
                try {
                    route = client.GetRoute(id);
                } catch (FaultException<NullPointerFault> nullException) {
                    throw new Exception(nullException.Message);
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }

            return route;
        }

        #endregion

        #region Misc

        private bool RouteValidation(Airport from, Airport to, List<Flight> flights) {
            var ret = true;

            if (from == null || to == null || flights == null) {
                ret = false;
            }

            return ret;
        }

        #endregion
    }
}