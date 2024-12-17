using Microsoft.AspNetCore.SignalR;

namespace BeerSender.Web.EventPublishing;

#pragma warning disable CA1812
internal sealed class EventHub : Hub
#pragma warning restore CA1812
{
    public async Task PublishEvent(Guid aggregateId, object @event)
    {
        await Clients.Group(aggregateId.ToString())
            .SendAsync("PublishEvent", aggregateId, @event).ConfigureAwait(false);
    }

    public async Task SubscribeToAggregate(Guid aggregateId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, aggregateId.ToString())
            .ConfigureAwait(false);
    }
}
