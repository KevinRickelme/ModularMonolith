using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Abstractions.Messaging
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task<Result> Handle(TEvent @event, CancellationToken cancellationToken);
    }

    public interface IEventHandler<in TEvent, TResponse>
        where TEvent : IEvent
    {
        Task<Result<TResponse>> Handle(TEvent @event, CancellationToken cancellationToken);
    }
}
