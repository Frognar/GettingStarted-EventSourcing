using BeerSender.Domain.Boxes;
using BeerSender.Projections.Database.Repositories;

namespace BeerSender.Projections.Projections;

internal sealed class UnshippedBoxProjection(UnshippedBoxRepository unshippedBoxRepository) : IProjection
{
    public List<Type> RelevantEventTypes =>
    [
        typeof(BoxCreated),
        typeof(BoxClosed),
        typeof(BoxShipped)
    ];

    public int BatchSize => 50;

    public int WaitTime => 5000; // 5s

    public void Project(IEnumerable<StoredEventWithVersion> events)
    {
        foreach (StoredEventWithVersion storedEvent in events)
        {
            Guid boxId = storedEvent.AggregateId;
            switch (storedEvent.EventData)
            {
                case BoxCreated:
                    unshippedBoxRepository.CreateOpenBox(boxId);
                    break;
                
                case BoxClosed:
                    unshippedBoxRepository.CloseBox(boxId);
                    break;
                
                case BoxShipped:
                    unshippedBoxRepository.ShipBox(boxId);
                    break;
            }
        }
    }
}
