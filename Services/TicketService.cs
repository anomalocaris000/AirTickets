using AirTickets.Models;

namespace AirTickets.Services;

public class TicketService
{
    private readonly Dictionary<int, Flight> flights = new();
    private readonly Dictionary<int, Airport> airports = new();
    private readonly Dictionary<int, Customer> customers = new();
    private readonly Dictionary<int, Ticket> tickets = new();
    private int nextFlightId = 1;
    private int nextAirportId = 1;
    private int nextCustomerId = 1;
    private int nextTicketId = 1;

    public IEnumerable<Flight> Flights => flights.Values;
    public IEnumerable<Airport> Airports => airports.Values;
    public IEnumerable<Customer> Customers => customers.Values;
    public IEnumerable<Ticket> Tickets => tickets.Values;
    public decimal Revenue => tickets.Values.Sum(t => t.Price);

    public Flight AddFlight(string number, string airline, decimal price)
    {
        var item = new Flight { Id = nextFlightId++, Number = number, Airline = airline, TicketPrice = price };
        flights.Add(item.Id, item);
        return item;
    }

    public bool EditFlight(int id, string number, string airline, decimal price)
    {
        if (!flights.TryGetValue(id, out var item) || price <= 0) return false;
        item.Number = number;
        item.Airline = airline;
        item.TicketPrice = price;
        return true;
    }

    public Airport AddAirport(string name, string city, string code)
    {
        var item = new Airport { Id = nextAirportId++, Name = name, City = city, Code = code };
        airports.Add(item.Id, item);
        return item;
    }

    public bool EditAirport(int id, string name, string city, string code)
    {
        if (!airports.TryGetValue(id, out var item)) return false;
        item.Name = name;
        item.City = city;
        item.Code = code;
        return true;
    }

    public Customer AddCustomer(string name, string email)
    {
        var item = new Customer { Id = nextCustomerId++, FullName = name, Email = email };
        customers.Add(item.Id, item);
        return item;
    }

    public bool EditCustomer(int id, string name, string email)
    {
        if (!customers.TryGetValue(id, out var item)) return false;
        item.FullName = name;
        item.Email = email;
        return true;
    }

    public Ticket? SellTicket(int flightId, int customerId)
    {
        if (!flights.TryGetValue(flightId, out var flight) || !customers.ContainsKey(customerId))
            return null;

        var item = new Ticket
        {
            Id = nextTicketId++,
            FlightId = flightId,
            CustomerId = customerId,
            SaleDate = DateTime.Now,
            Price = flight.TicketPrice
        };
        tickets.Add(item.Id, item);
        return item;
    }

    public Flight? FindFlight(int id) => flights.GetValueOrDefault(id);
    public Airport? FindAirport(int id) => airports.GetValueOrDefault(id);
    public Customer? FindCustomer(int id) => customers.GetValueOrDefault(id);
}
