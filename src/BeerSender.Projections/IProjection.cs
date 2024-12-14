using BeerSender.Projections.Database.Repositories;

namespace BeerSender.Projections;

internal interface IProjection
{
    List<Type> RelevantEventTypes { get; }
    int BatchSize { get; }
    int WaitTime { get; }
    void Project(IEnumerable<StoredEventWithVersion> events);
}
