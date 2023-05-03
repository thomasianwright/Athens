using Athens.Abstractions.Products.Commands;
using Athens.Abstractions.Products.Models;
using Athens.Core.Data;
using Mediator;

namespace Athens.Core.Products.Commands;

public class CreateTireProductCommandHandler : ICommandHandler<CreateTireCommand, Unit>
{
    private readonly ApplicationContext _dbContext;

    public CreateTireProductCommandHandler(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<Unit> Handle(CreateTireCommand command, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(new TireProduct
        {
            Code = command.Code,
            Description = command.Description,
            Section = command.Section,
            RimSize = command.RimSize,
            Profile = command.Profile,
            CreatedAt = DateTime.Now,
            FuelRating = command.FuelRating,
            WetRating = command.WetRating,
            UpdatedAt = DateTime.Now,
            Id = Guid.NewGuid()
        }, cancellationToken: cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}