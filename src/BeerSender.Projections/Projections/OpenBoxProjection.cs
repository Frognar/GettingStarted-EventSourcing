using BeerSender.Domain.Boxes;
using BeerSender.Projections.Database.Repositories;

namespace BeerSender.Projections.Projections;

internal sealed class OpenBoxProjection(OpenBoxRepository openBoxRepository) : IProjection
{
    public List<Type> RelevantEventTypes => [
        typeof(BoxCreated),
        typeof(BeerBottleAdded),
        typeof(BoxClosed),
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
                case BoxCreated created:
                    openBoxRepository.CreateOpenBox(boxId, created.Capacity.NumberOfSpots);
                    break;
                
                case BeerBottleAdded:
                    openBoxRepository.AddBottleToBox(boxId);
                    break;
                
                case BoxClosed:
                    openBoxRepository.RemoveOpenBox(boxId);
                    break;
            }
        }
    }
}
