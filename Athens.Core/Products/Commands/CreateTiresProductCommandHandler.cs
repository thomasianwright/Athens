using Athens.Abstractions.Products.Commands;
using Athens.Abstractions.Products.Models;
using Athens.Core.Data;
using Mediator;

namespace Athens.Core.Products.Commands;

public class CreateTiresProductCommandHandler : ICommandHandler<CreateTiresCommand, Unit>
{
    private readonly ApplicationContext _dbContext;

    public CreateTiresProductCommandHandler(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Unit> Handle(CreateTiresCommand command, CancellationToken cancellationToken)
    {
        var tires = command.Tires.Select(x => new TireProduct
        {
            Code = x.Code,
            Description = x.Description,
            Section = x.Section,
            RimSize = x.RimSize,
            Profile = x.Profile,
            CreatedAt = DateTime.Now,
            FuelRating = x.FuelRating,
            WetRating = x.WetRating,
            UpdatedAt = DateTime.Now,
            Id = Guid.NewGuid()
        });

        await _dbContext.AddRangeAsync(tires, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}