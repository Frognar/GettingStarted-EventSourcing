using System.Diagnostics.CodeAnalysis;

namespace BeerSender.Domain.Boxes;

[SuppressMessage(
    "Naming",
    "CA1720:Identifiers should not contain type names",
    Justification = "Double is a valid beer type name")]
public enum BeerType
{
    Ipa,
    Stout,
    Sour,
    Double,
    Triple, 
    Quadruple
}