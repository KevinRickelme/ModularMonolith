using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public interface IEvent
    {
        Guid StreamId { get; }
        DateTime Timestamp { get; }
    }
    public abstract record Event(Guid StreamId) : IEvent, INotification
    {
        public DateTime Timestamp { get; init; } = DateTime.Now;
    }
}
