namespace BeerSender.Domain.Boxes;

public class Box : AggregateRoot
{
    public void Apply(BoxCreated @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        Capacity = @event.Capacity;
    }

    public void Apply(ShippingLabelAdded @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        ShippingLabel = @event.Label;
    }

    public void Apply(BeerBottleAdded @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        beerBottles.Add(@event.BeerBottle);
    }

    public BoxCapacity? Capacity { get; private set; }

    public ShippingLabel? ShippingLabel { get; private set; }

    private List<BeerBottle> beerBottles = [];

    public IEnumerable<BeerBottle> BeerBottles => beerBottles;

    public bool IsFull => beerBottles.Count >= Capacity?.NumberOfSpots;
}
