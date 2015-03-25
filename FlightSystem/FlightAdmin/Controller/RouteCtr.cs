using System;
using System.Collections.Generic;
using System.ServiceModel;
using FlightAdmin.Exceptions;
using FlightAdmin.MainService;
using Flight = FlightAdmin.MainService.Flight;

namespace FlightAdmin.Controller {
    public class RouteCtr {

        #region Create

        public Route CreateRoute(Airport from, Airport to, List<Flight> flights, decimal price) { //TODO Better Exception
            Route route;

            if (RouteValidation(from, to, flights)) {
                route = new Route {From = from, To = to, Flights = flights, Price = price};

                using (var client = new RouteServiceClient()) {
                    try {
                        route = client.AddRoute(route);
                    } catch (FaultException<DatabaseInsertFault> dbException) {
                        throw new DatabaseException(dbException.Message);
                    } catch (FaultException<AlreadyExistFault> existException) {
                        throw new AlreadyExistException("The Route with that 'To' and 'From' Airport already exists");
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
            Route retRoute;
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
            Route route;

            using (var client = new RouteServiceClient()) {
                try {
                    route = client.GetRoute(id);
                } catch (FaultException<NullPointerFault>) {
                    throw new NullException("The requested Airport does not exist");
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }

            return route;
        }

        public Route GetRouteByAirports(Airport from, Airport to) {
            Route route;

            using (var client = new RouteServiceClient()) {
                try {
                    route = client.GetRouteByAirports(from, to);
                } catch (FaultException<NullPointerFault>) {
                    throw new NullException("There are no routes between thoes Airports");
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }


            return route;
        }

        public List<Route> GetRoutesByAirport(Airport from) {
            List<Route> routes;

            using (var client = new RouteServiceClient()) {
                try {
                    routes = client.GetRoutesByAirport(from);
                } catch (FaultException<NullPointerFault>) {
                    throw new NullException("No routes exists from that Airport");
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }


            return routes;
        }

        #endregion

        #region Misc

        private bool RouteValidation(Airport from, Airport to, List<Flight> flights) {
            if (from == null || to == null) {
                return false;
            }

            return true;
        }

        #endregion
    }
}