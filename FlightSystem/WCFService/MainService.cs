using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;
using WCFService.Dijkstra;
using WCFService.Logging;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Interface;

namespace WCFService {
    public class MainService : IAdministratorService, IAirportService, IFlightService, IPlaneService, IRouteService, IUserService, IPostalService, IDijkstra, IReservationService {

        #region Initial

        private readonly IAdministratorService _adminService = new AdministratorService();
        private readonly IAirportService _airportService = new AirportService();
        private readonly IFlightService _flightService = new FlightService();
        private readonly IPlaneService _planeService = new PlaneService();
        private readonly IRouteService _routeService = new RouteService();
        private readonly IUserService _userService = new UserService();
        private readonly IPostalService _postalService = new PostalService();
        private readonly IDijkstra _dijkstraService = new WCF.Dijkstra();
        private readonly IReservationService _resService = new ReservationService();

        public static void Configure(ServiceConfiguration config) {
            config.LoadFromConfiguration();
            Trace.Listeners.Add(new TextWriterTraceListener(new Logger()));
            ReservationService.DeleteDeadTickets();
        }

        #endregion

        #region Administrator Service

        public Administrator AddAdministrator(Administrator administrator) {
            return _adminService.AddAdministrator(administrator);
        }

        public Administrator UpdateAdministrator(Administrator administrator) {
            return _adminService.UpdateAdministrator(administrator);
        }
        
        public void DeleteAdministrator(Administrator administrator) {
            _adminService.DeleteAdministrator(administrator);
        }

        public Administrator GetAdministrator(int id) {
            return _adminService.GetAdministrator(id);
        }

        public List<Administrator> GetAdministratorsByUsername(string username, bool equalsTo) {
            return _adminService.GetAdministratorsByUsername(username, equalsTo);
        }

        public List<Administrator> GetAllAdministrators() {
            return _adminService.GetAllAdministrators();
        }

        public bool ValidateLogin(string username, string password) {
            return _adminService.ValidateLogin(username, password);
        }

        #endregion

        #region Airport Service
        
        public Airport AddAirport(Airport airport) {
            return _airportService.AddAirport(airport);
        }

        public Airport UpdateAirport(Airport airport) {
            return _airportService.UpdateAirport(airport);
        }

        public void DeleteAirport(Airport airport) {
            _airportService.DeleteAirport(airport);
        }

        public Airport GetAirport(int id) {
            return _airportService.GetAirport(id);
        }

        public List<Airport> GetAirportsByCountry(string country) {
            return _airportService.GetAirportsByCountry(country);
        }

        public List<Airport> GetAirportsByCity(string city) {
            return _airportService.GetAirportsByCity(city);
        }

        public List<Airport> GetAirportsByName(string name) {
            return _airportService.GetAirportsByName(name);
        }

        public List<Airport> GetAirportsByShortName(string shortName, bool equalsTo) {
            return _airportService.GetAirportsByShortName(shortName, equalsTo);
        }

        public List<string> GetCountries() {
            return _airportService.GetCountries();
        }

        #endregion

        #region Flight Service

        public int AddFlight(Flight flight) {
            return _flightService.AddFlight(flight);
        }

        public Flight UpdateFlight(Flight flight) {
            return _flightService.UpdateFlight(flight);
        }

        public void DeleteFlight(Flight flight) {
            _flightService.DeleteFlight(flight);
        }

        public Flight GetFlight(int id) {
            return _flightService.GetFlight(id);
        }
        public List<Flight> GetFlights(Airport from, Airport to) {
            return _flightService.GetFlights(from, to);
        }

        #endregion

        #region Plane Service

        public int AddPlane(Plane plane) {
            return _planeService.AddPlane(plane);
        }

        public Plane UpdatePlane(Plane plane) {
            return _planeService.UpdatePlane(plane);
        }

        public void DeletePlane(Plane plane) {
            _planeService.DeletePlane(plane);
        }

        public List<Plane> GetPlanesByName(string name) {
            return _planeService.GetPlanesByName(name);
        }

        public Plane GetPlaneByID(int id) {
            return _planeService.GetPlaneByID(id);
        }

        public List<Plane> GetPlanesWithSeatNumber(int seats) {
            return _planeService.GetPlanesWithSeatNumber(seats);
        }

        public List<Plane> GetPlanesWithLessOrEqualSeatNumber(int seats) {
            return _planeService.GetPlanesWithLessOrEqualSeatNumber(seats);
        }

        public List<Plane> GetPlanesWithMoreOrEqualSeatNumber(int seats) {
            return _planeService.GetPlanesWithMoreOrEqualSeatNumber(seats);
        }
        
        public List<Plane> GetAllPlanes() {
            return _planeService.GetAllPlanes();
        }

        #endregion

        #region Route Service

        public Route AddRoute(Route route) {
            return _routeService.AddRoute(route);
        }

        public Route UpdateRoute(Route route) {
            return _routeService.UpdateRoute(route);
        }

        public Route AddOrUpdateFlights(Route route) {
            return _routeService.AddOrUpdateFlights(route);
        }

        public void DeleteRoute(Route route) {
            _routeService.DeleteRoute(route);
        }

        public Route GetRoute(int id) {
            return _routeService.GetRoute(id);
        }

        public Route GetRouteByAirports(Airport from, Airport to) {
            return _routeService.GetRouteByAirports(from, to);
        }

        public List<Route> GetRoutesByAirport(Airport from) {
            return _routeService.GetRoutesByAirport(from);
        }

        #endregion

        #region User Service

        public int AddUser(User user) {
            return _userService.AddUser(user);
        }

        public User UpdateUser(User user) {
            return _userService.UpdateUser(user);
        }

        public void DeleteUser(User user) {
            _userService.DeleteUser(user);
        }

        public User GetUser(int id) {
            return _userService.GetUser(id);
        }

        public List<User> GetUserByName(string name)
        {
            return _userService.GetUserByName(name);
        }

        public List<User> GetUsersByEmail(string email, bool equalsTo) {
            return _userService.GetUsersByEmail(email, equalsTo);
        }

        public List<User> GetAllUsers() {
            return _userService.GetAllUsers();
        }

        public bool AuthenticateUser(string email, string password)
        {
            return _userService.AuthenticateUser(email, password);
        }

        #endregion

        #region Postal Service

        public int AddPostal(Postal postal)
        {
            return _postalService.AddPostal(postal);
        }

        public int DeletePostal(Postal postal)
        {
            return _postalService.DeletePostal(postal);
        }

        public Postal GetPostal(int postalNumber) {
            return _postalService.GetPostal(postalNumber);
        }

        public Postal UpdatePostal(Postal postal) {
            return _postalService.UpdatePostal(postal);
        }

        #endregion

        #region Dijktra

        public List<Flight> DijkstraStuff(int fromId, int toId, int seats, DateTime startTime) {
            return _dijkstraService.DijkstraStuff(fromId, toId, seats,startTime);
        }

        public void DijkstraTest(int from, int to, int seats, DateTime dt) {
            _dijkstraService.DijkstraTest(from, to, seats, dt);
        }

        #endregion

        #region Reservation Service
        
        public Ticket MakeSeatsOccupiedRandom(List<Flight> flights, int noOfSeats, User user) {
            return _resService.MakeSeatsOccupiedRandom(flights, noOfSeats, user);
        }

        public void Cancel() {
            _resService.Cancel();
        }

        public void Complete() {
            _resService.Complete();
        }

        #endregion
    }
}
