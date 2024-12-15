using System.Data;
using BeerSender.Projections.Database;
using BeerSender.Projections.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeerSender.Projections;

internal sealed class ProjectionService<TProjection>(
    IServiceProvider serviceProvider,
    EventStoreRepository eventStoreRepository) : BackgroundService
    where TProjection : class, IProjection
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        byte[] checkpoint = GetCheckpoint();
        while (stoppingToken.IsCancellationRequested == false)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            ReadStoreConnection connection = scope.ServiceProvider
                .GetRequiredService<ReadStoreConnection>();

            using IDbTransaction transaction = connection.GetTransaction();
            TProjection projection = scope.ServiceProvider
                .GetRequiredService<TProjection>();

            List<StoredEventWithVersion> events = eventStoreRepository.GetEvents(
                projection.RelevantEventTypes,
                checkpoint,
                projection.BatchSize);

            if (events.Count == 0)
            {
                await Task.Delay(projection.WaitTime, stoppingToken).ConfigureAwait(false);
            }
            else
            {
                projection.Project(events);
                checkpoint = events.Last().RowVersion;
                CheckpointRepository checkpointRepo = scope.ServiceProvider
                    .GetRequiredService<CheckpointRepository>();

                checkpointRepo.SetCheckpoint(typeof(TProjection).Name, checkpoint);
            }
            
            transaction.Commit();
        }
    }

    private byte[] GetCheckpoint()
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        CheckpointRepository checkpointService = scope.ServiceProvider
            .GetRequiredService<CheckpointRepository>();

        byte[] checkpoint = checkpointService.GetCheckpoint(typeof(TProjection).Name);
        return checkpoint;
    }
}
