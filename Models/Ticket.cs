namespace AirTickets.Models;

public class Ticket
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int CustomerId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal Price { get; set; }
}
