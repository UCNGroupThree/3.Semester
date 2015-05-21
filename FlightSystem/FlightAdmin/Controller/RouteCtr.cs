using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using Common;
using Common.Exceptions;
using FlightAdmin.GUI;
using FlightAdmin.MainService;

namespace FlightAdmin.Controller {
    public class RouteCtr {

        #region Create

        /// <exception cref="DatabaseException"/>
        /// <exception cref="ConnectionException"/>
        /// <exception cref="ValidationException"/>
        /// <exception cref="AlreadyExistException"/>
        public Route CreateRoute(Airport from, Airport to, List<Flight> flights, decimal price) { //TODO Better Exception
            Route route;

            if (RouteValidation(from, to, flights)) {
                route = new Route {From = from, To = to, Flights = flights, Price = price};

                using (var client = new RouteServiceClient()) {
                    try {
                        route = client.AddRoute(route);
                    } catch (FaultException<DatabaseInsertFault> dbException) {
                        throw new DatabaseException(dbException.Detail.Message);
                    } catch (FaultException<AlreadyExistFault> existException) {
                        throw new AlreadyExistException("The Route with that 'To' and 'From' Airport already exists");
                    } catch (Exception e) {
                        Console.WriteLine(e);
                        throw new ConnectionException("WCF Service Exception", e);
                    }
                }
            } else {
                throw new ValidationException("RouteValidation Exception");
            }

            return route;
        }

        #endregion

        #region Update

        /// <exception cref="DatabaseException"/>
        /// <exception cref="DBConcurrencyException"/>
        /// <exception cref="DeleteException"/>
        /// <exception cref="ConnectionException"/>
        public Route AddOrUpdateFlights(Route route, List<Flight> flights) { //TODO Better Exception
            Route retRoute;
                using (var client = new RouteServiceClient()) {
                    try {
                        route.Flights = flights;

                        foreach (var flight in flights) {
                            flight.RouteID = route.ID;
                            flight.PlaneID = flight.Plane.ID;
                        }

                        retRoute = client.AddOrUpdateFlights(route);
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
                    } catch (FaultException<DeleteFault> deleteFault) {
#if DEBUG
                        deleteFault.DebugGetLine();
#endif

                        throw new DeleteException(deleteFault.Detail.Message);
                    } catch (Exception e) {

#if DEBUG
                        e.DebugGetLine();
#endif
                        throw new ConnectionException("WCF Service Exception", e);
                    }
                }

            return retRoute;
        }


        /// <exception cref="DatabaseException"/>
        /// <exception cref="DBConcurrencyException"/>
        /// <exception cref="DeleteException"/>
        /// <exception cref="ConnectionException"/>
        /// <exception cref="ValidationException"/>
        public Route UpdateRoute(Route route, Airport from, Airport to, List<Flight> flights, decimal price) { //TODO Better Exception
            Route retRoute;
            if (RouteValidation(from, to, flights)) {
                using (var client = new RouteServiceClient()) {
                    try {
                        route.From = from;
                        route.FromID = from.ID;

                        route.To = to;
                        route.ToID = to.ID;

                        route.Flights = flights;

                        foreach (var flight in flights) {
                            flight.RouteID = route.ID;
                            flight.PlaneID = flight.Plane.ID;
                        }

                        route.Price = price;

                        retRoute = client.UpdateRoute(route);
                    } catch (FaultException<OptimisticConcurrencyFault> concurrencyException) {
                        throw new DBConcurrencyException(concurrencyException.Detail.Message);
                    } catch (FaultException<DatabaseUpdateFault> updateException) {
                        throw new DatabaseException(updateException.Detail.Message);
                    } catch (FaultException<DeleteFault> deleteFault) {
                        throw new DeleteException(deleteFault.Detail.Message);
                    } catch (Exception e) {
                        throw new ConnectionException("WCF Service Exception", e);
                    }
                }
            } else {
                throw new ValidationException("Invalid Route!");
            }

            return retRoute;
        }

        #endregion

        #region Delete

        /// <exception cref="NullException"/>
        /// <exception cref="DatabaseException"/>
        /// <exception cref="ConnectionException"/>
        public void DeleteRoute(Route route) { //TODO Better Exception
            using (var client = new RouteServiceClient()) {
                try {
                    client.DeleteRoute(route);
                } catch (FaultException<NullPointerFault> nullException) {
                    throw new NullException(nullException.Message);
                } catch (FaultException<DatabaseDeleteFault> dbException) {
                    throw new DatabaseException(dbException.Message);
                } catch (Exception e) {
                    throw new ConnectionException("WCF Service Exception", e);
                }
            }
        }

        #endregion

        #region Read

        /// <exception cref="NullException"/>
        /// <exception cref="ConnectionException"/>
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

        /// <exception cref="NullException"/>
        /// <exception cref="ConnectionException"/>
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

        /// <exception cref="NullException"/>
        /// <exception cref="ConnectionException"/>
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