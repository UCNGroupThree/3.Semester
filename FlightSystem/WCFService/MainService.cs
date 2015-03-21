using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Interface;

namespace WCFService {
    public class MainService : IAdministratorService, IAirportService, IFlightService, IPlaneService, IRouteService, IUserService {

        #region Administrator Service 

        public int AddAdministrator(Administrator administrator) {
            return new AdministratorService().AddAdministrator(administrator);
        }

        public Administrator UpdateAdministrator(Administrator administrator) {
            return new AdministratorService().UpdateAdministrator(administrator);
        }

        public void DeleteAdministrator(Administrator administrator) {
            new AdministratorService().DeleteAdministrator(administrator);
        }

        public Administrator GetAdministrator(int id) {
            return new AdministratorService().GetAdministrator(id);
        }

        #endregion

        #region Airport Service
        
        public int AddAirport(Airport airport) {
            return new AirportService().AddAirport(airport);
        }

        public Airport UpdateAirport(Airport airport) {
            return new AirportService().UpdateAirport(airport);
        }

        public void DeleteAirport(Airport airport) {
            new AirportService().DeleteAirport(airport);
        }

        public Airport GetAirport(int id) {
            return new AirportService().GetAirport(id);
        }

        public List<Airport> GetAirportsByCountry(string country) {
            return new AirportService().GetAirportsByCountry(country);
        }

        public List<Airport> GetAirportsByCity(string city) {
            return new AirportService().GetAirportsByCity(city);
        }

        public List<Airport> GetAirportsByName(string name) {
            return new AirportService().GetAirportsByName(name);
        }

        #endregion

        #region Flight Service

        public int AddFlight(Flight flight) {
            return new FlightService().AddFlight(flight);
        }

        public Flight UpdateFlight(Flight flight) {
            return new FlightService().UpdateFlight(flight);
        }

        public void DeleteFlight(Flight flight) {
            new FlightService().DeleteFlight(flight);
        }

        public Flight GetFlight(int id) {
            return new FlightService().GetFlight(id);
        }

        #endregion

        #region Plane Service

        public int AddPlane(Plane plane) {
            return new PlaneService().AddPlane(plane);
        }

        public Plane UpdatePlane(Plane plane) {
            return new PlaneService().UpdatePlane(plane);
        }

        public void DeletePlane(Plane plane) {
            new PlaneService().DeletePlane(plane);
        }

        public Plane GetPlane(int id) {
            return new PlaneService().GetPlane(id);
        }

        #endregion

        #region Route Service

        public Route AddRoute(Route route) {
            return new RouteService().AddRoute(route);
        }

        public Route UpdateRoute(Route route) {
            return new RouteService().UpdateRoute(route);
        }

        public void DeleteRoute(Route route) {
            new RouteService().DeleteRoute(route);
        }

        public Route GetRoute(int id) {
            return new RouteService().GetRoute(id);
        }

        public Route GetRoute(Airport from, Airport to) {
            return new RouteService().GetRoute(from, to);
        }

        public List<Route> GetRoutes(Airport from) {
            return new RouteService().GetRoutes(from);
        }

        #endregion

        #region User Service

        public int AddUser(User user) {
            return new UserService().AddUser(user);
        }

        public User UpdateUser(User user) {
            return new UserService().UpdateUser(user);
        }

        public void DeleteUser(User user) {
            new UserService().DeleteUser(user);
        }

        public User GetUser(int id) {
            return new UserService().GetUser(id);
        }

        #endregion
    }
}
