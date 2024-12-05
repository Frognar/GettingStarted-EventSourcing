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
        _beerBottles.Add(@event.BeerBottle);
    }

    public void Apply(BoxClosed @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        IsClosed = true;
    }

    public void Apply(BoxShipped @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        IsShipped = true;
    }

    public BoxCapacity? Capacity { get; private set; }

    public ShippingLabel? ShippingLabel { get; private set; }

    private readonly List<BeerBottle> _beerBottles = [];

    public IEnumerable<BeerBottle> BeerBottles => _beerBottles;

    public bool IsClosed { get; private set; }
    public bool IsShipped { get; private set; }

    public bool IsFull => _beerBottles.Count >= Capacity?.NumberOfSpots;
}
