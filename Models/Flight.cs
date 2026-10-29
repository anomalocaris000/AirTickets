namespace AirTickets.Models;

public class Flight
{
    public int Id { get; set; }
    public string Number { get; set; } = "";
    public string Airline { get; set; } = "";
    public decimal TicketPrice { get; set; }
}
