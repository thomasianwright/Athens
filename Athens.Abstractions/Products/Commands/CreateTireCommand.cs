using Mediator;

namespace Athens.Abstractions.Products.Commands;

public class CreateTireCommand : ICommand<Unit>
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Profile { get; set; }
    public int Section { get; set; }
    public int RimSize { get; set; }
    public char WetRating { get; set; }
    public char FuelRating { get; set; }
}