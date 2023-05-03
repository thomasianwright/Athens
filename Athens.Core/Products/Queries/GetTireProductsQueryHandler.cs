using Athens.Abstractions.Products.Models;
using Athens.Abstractions.Products.Queries;
using Athens.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Athens.Core.Products.Queries;

public class GetTireProductsQueryHandler : IQueryHandler<GetTireProductsQuery, IEnumerable<TireProduct>>
{
    private readonly ApplicationContext _dbContext;

    public GetTireProductsQueryHandler(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<IEnumerable<TireProduct>> Handle(GetTireProductsQuery query, CancellationToken cancellationToken)
    {
        var products = _dbContext.TireProducts.AsQueryable();
        
        if (!string.IsNullOrEmpty(query.Search))
        {
            products = products.Where(x => x.Code.Contains(query.Search) || x.Description.Contains(query.Search));
        }

        if (query.Profile is not null)
        {
            products = products.Where(x => x.Profile == query.Profile);
        }
        
        if (query.RimSize is not null)
        {
            products = products.Where(x => x.RimSize == query.RimSize);
        }
        
        if (query.Section is not null)
        {
            products = products.Where(x => x.Section == query.Section);
        }
        
        if (query.FuelRating is not null)
        {
            products = products.Where(x => x.FuelRating == query.FuelRating);
        }
        
        return await products.ToListAsync(cancellationToken);
    }
}