namespace Doctorly.Data.Models;

public class Attendee
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public bool Accepted { get; set; }
}