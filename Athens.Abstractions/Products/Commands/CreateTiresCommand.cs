using Mediator;

namespace Athens.Abstractions.Products.Commands;

public class CreateTiresCommand : ICommand<Unit>
{
    public IEnumerable<CreateTireCommand> Tires { get; set; }
}