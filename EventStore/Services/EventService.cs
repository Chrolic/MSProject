using EventStore.Services.Interfaces;
using EventStore.Utilities.DTOs;
using EventStore.Utilities.Models;

namespace EventStore.Services
{
    public class EventService : IEventService
    {
        private static long currentSequenceNumber = 0;
        private static readonly IList<Event> _database = new List<Event>();

        public EventService()
        {

        }


        public IEnumerable<ReadEventDto> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            var result = _database.Where(e =>
            e.SequenceNumber >= firstEventSequenceNumber &&
            e.SequenceNumber <= lastEventSequenceNumber)
                .OrderBy(e => e.SequenceNumber);

            

            return result.Select(x => new ReadEventDto()
            {
                SequenceNumber = x.SequenceNumber,
                OccuredAt = x.OccuredAt,
                Name = x.Name,
                Content = x.Content
            });
        }

        public ReadEventDto CreateEvent(CreateEventDto dto)
        {
            var newEvent = Raise(dto.EventName, dto.Content);
            return new ReadEventDto
            { 
                SequenceNumber = newEvent.SequenceNumber,
                OccuredAt = newEvent.OccuredAt,
                Name = newEvent.Name,
                Content = newEvent.Content
            };
        }

        public Event Raise(string eventName, object content)
        {
            var seqNumber = Interlocked.Increment(ref currentSequenceNumber);
            var newEvent = new Event(seqNumber, DateTimeOffset.UtcNow, eventName, content);
            _database.Add(newEvent);
            return newEvent;
        }
    }
}
