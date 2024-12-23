using System.Diagnostics.CodeAnalysis;
using BeerSender.Domain.Boxes;

namespace BeerSender.Domain.Tests.Boxes;

[SuppressMessage(
    "Maintainability",
    "CA1515:Consider making public types internal",
    Justification = "Used for unit tests")]
public abstract class BoxTest<TCommand> : CommandHandlerTest<TCommand>
{
    protected Guid Box_Id => _aggregateId;
    
    // Events
    protected BoxCreated Box_created_with_capacity(int capacity)
        => new(new BoxCapacity(capacity));

    protected BoxCreated Small_box_created()
        => Box_created_with_capacity(6);
   
    protected BoxCreated Medium_box_created()
        => Box_created_with_capacity(12);
   
    protected BoxCreated Large_box_created()
        => Box_created_with_capacity(24);
    
    protected BeerBottleAdded Beer_bottle_added(BeerBottle bottle)
        => new(bottle);

    protected FailedToAddBeerBottle Failed_to_add_bottle_because_box_was_full()
        => new(FailedToAddBeerBottle.FailReason.BoxWasFull);

    protected ShippingLabelAdded Shipping_label_added(ShippingLabel label)
        => new(label);

    protected FailedToAddShippingLabel Failed_to_add_shipping_label_because_label_has_invalid_tracking_number()
        => new(FailedToAddShippingLabel.FailReason.TrackingCodeInvalid);

    protected BoxClosed Box_closed()
        => new();
    
    protected FailedToCloseBox Failed_to_close_box_because_box_was_empty()
        => new(FailedToCloseBox.FailReason.BoxWasEmpty);

    protected BoxShipped Box_shipped()
        => new();

    protected FailedToShipBox Failed_to_ship_box_because_box_was_not_ready()
        => new(FailedToShipBox.FailReason.BoxWasNotReady);

    protected FailedToShipBox Failed_to_ship_box_because_box_was_already_shipped()
        => new(FailedToShipBox.FailReason.BoxWasAlreadyShipped);

    protected FailedToShipBox Failed_to_ship_box_because_box_has_no_shipping_label()
        => new(FailedToShipBox.FailReason.BoxHasNoShippingLabel);
    
    // Test data
    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    protected BeerBottle carte_blanche = new(
        "Wolf",
        "Carte Blanche",
        0.0,
        BeerType.Triple
    );
    
    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    protected BeerBottle Oatmeal_double_ipa = new(
        "Fate",
        "Oatmeal Double IPA",
        7.3,
        BeerType.Ipa
    );

    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    protected ShippingLabel Valid_FexEx_shipping_label = new(
        Carrier.FedEx,
        "DEF12345");

    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    protected ShippingLabel FexEx_shipping_label_with_invalid_tracking_number = new(
        Carrier.FedEx,
        "ABC12345");
}