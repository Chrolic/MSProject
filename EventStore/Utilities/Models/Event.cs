namespace EventStore.Utilities.Models
{
    public record Event(long SequenceNumber, DateTimeOffset OccuredAt, string Name, object Content)
    {

    }
}
