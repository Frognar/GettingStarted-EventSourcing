using BeerSender.Domain;
using Microsoft.AspNetCore.SignalR;

namespace BeerSender.Web.EventPublishing;

#pragma warning disable CA1812
internal sealed class NotificationService(IHubContext<EventHub> hubContext) : INotificationService
#pragma warning restore CA1812
{
    private readonly IHubContext<EventHub> _hubContext = hubContext;

    public void PublishEvent(Guid aggregateId, object @event)
    {
        _hubContext.Clients.Group(aggregateId.ToString())
            .SendAsync("PublishEvent", aggregateId, @event, @event.GetType().Name);
    }
}
