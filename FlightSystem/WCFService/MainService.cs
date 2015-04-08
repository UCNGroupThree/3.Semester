using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Interface;

namespace WCFService {
    public class MainService : IAdministratorService, IAirportService, IFlightService, IPlaneService, IRouteService, IUserService, IDijkstra {

        #region Administrator Service 
        
        public int AddAdministrator(Administrator administrator) {
            return new AdministratorService().AddAdministrator(administrator);
        }

        public void UpdateAdministrator(Administrator administrator) {
            new AdministratorService().UpdateAdministrator(administrator);
        }

        public void UpdatePassword(Administrator administrator) {
            new AdministratorService().UpdatePassword(administrator);
        }
        
        public void DeleteAdministrator(Administrator administrator) {
            new AdministratorService().DeleteAdministrator(administrator);
        }

        public Administrator GetAdministrator(int id) {
            return new AdministratorService().GetAdministrator(id);
        }

        public List<Administrator> GetAllAdministrators() {
            return new AdministratorService().GetAllAdministrators();
        }

        #endregion

        #region Airport Service
        
        public int AddAirport(Airport airport) {
            return new AirportService().AddAirport(airport);
        }

        public void UpdateAirport(Airport airport) {
            new AirportService().UpdateAirport(airport);
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

        public List<Airport> GetAirportsByShortName(string shortName, bool equalsTo) {
            return new AirportService().GetAirportsByShortName(shortName, equalsTo);
        }

        public List<string> GetCountries() {
            return new AirportService().GetCountries();
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

        public List<Plane> GetAllPlanes() {
            return new PlaneService().GetAllPlanes();
        }

        public List<Plane> GetPlanesWithEqualSeatNumber(int seats) {
            return new PlaneService().GetPlanesWithEqualSeatNumber(seats);
        }

        public List<Plane> GetPlaneswithLessThanOrEqualSeatNumber(int seats) {
            return new PlaneService().GetPlaneswithLessThanOrEqualSeatNumber(seats);
        }

        public List<Plane> GetPlaneswithMoreOrEqualSeatNumber(int seats) {
            return new PlaneService().GetPlaneswithMoreOrEqualSeatNumber(seats);
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

        public Route GetRouteByAirports(Airport from, Airport to) {
            return new RouteService().GetRouteByAirports(from, to);
        }

        public List<Route> GetRoutesByAirport(Airport from) {
            return new RouteService().GetRoutesByAirport(from);
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

        public List<User> GetUserByName(string name)
        {
            return new UserService().GetUserByName(name);
        }

        #endregion

        #region Dijktra

        public List<Route> DijkstraStuff(Airport from, Airport to) {
            return new WCF.Dijkstra().DijkstraStuff(from, to);
        }

        #endregion

    }
}
