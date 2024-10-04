﻿namespace EventStore.Utilities.DTOs
{
    public class ReadEventDto()
    {
        public long SequenceNumber { get; set; }
        public DateTimeOffset OccuredAt { get; set; }
        public string Name { get; set; }
        public object Content { get; set; }
    }
}