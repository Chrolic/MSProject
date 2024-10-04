﻿using EventStore.Utilities.DTOs;
using EventStore.Utilities.Models;

namespace EventStore.Services.Interfaces
{
    public interface IEventService
    {
        ReadEventDto CreateEvent(CreateEventDto dto);
        IEnumerable<ReadEventDto> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        Event Raise(string eventName, object content);
    }
}
