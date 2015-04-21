﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.Model;
using WCFService.WCF;
using WCFService.WCF.Interface;

namespace WCFService {
    public class MainService : IAdministratorService, IAirportService, IFlightService, IPlaneService, IRouteService, IUserService, IPostalService, IDijkstra {

        #region Administrator Service 
        
        public Administrator AddAdministrator(Administrator administrator) {
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

        public List<Administrator> GetAdministratorsByUsername(string username, bool equalsTo) {
            return new AdministratorService().GetAdministratorsByUsername(username, equalsTo);
        }

        public List<Administrator> GetAllAdministrators() {
            return new AdministratorService().GetAllAdministrators();
        }

        #endregion

        #region Airport Service
        
        public Airport AddAirport(Airport airport) {
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
        public List<Flight> GetFlights(Airport from, Airport to) {
            return new FlightService().GetFlights(from, to);
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

        public List<Plane> GetPlanesByName(string name) {
            return new PlaneService().GetPlanesByName(name);
        }

        public Plane GetPlaneByID(int id) {
            return new PlaneService().GetPlaneByID(id);
        }

        public List<Plane> GetPlanesWithSeatNumber(int seats) {
            return new PlaneService().GetPlanesWithSeatNumber(seats);
        }

        public List<Plane> GetPlanesWithLessOrEqualSeatNumber(int seats) {
            return new PlaneService().GetPlanesWithLessOrEqualSeatNumber(seats);
        }

        public List<Plane> GetPlanesWithMoreOrEqualSeatNumber(int seats) {
            return new PlaneService().GetPlanesWithMoreOrEqualSeatNumber(seats);
        }
        
        public List<Plane> GetAllPlanes() {
            return new PlaneService().GetAllPlanes();
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

        public List<User> GetAllUsers() {
            return new UserService().GetAllUsers();
        } 

        #endregion

        #region Postal Service

        public int AddPostal(Postal postal)
        {
            return new PostalService().AddPostal(postal);
        }

        public int DeletePostal(Postal postal)
        {
            return new PostalService().DeletePostal(postal);
        }

        public Postal GetPostal(int postalNumber) {
            return new PostalService().GetPostal(postalNumber);
        }

        public Postal UpdatePostal(Postal postal) {
            return new PostalService().UpdatePostal(postal);
        }

        #endregion

        #region Dijktra

        public List<Route> DijkstraStuff(Airport from, Airport to, DateTime startTime) {
            return new WCF.Dijkstra().DijkstraStuff(from, to, startTime);
        }

        #endregion

    }
}
