using System.Globalization;
using Athens.Abstractions.Products.Models;
using Athens.Core.Data;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Athens.Core.Products;

public class SeedData : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public SeedData(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        using var reader = new StreamReader(Path.Join(Directory.GetCurrentDirectory(), "Data", "tires.csv"));
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<TireClassMap>();
        var records = csv.GetRecords<TireProduct>();
        
        await dbContext.TireProducts.AddRangeAsync(records, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private sealed class TireClassMap : ClassMap<TireProduct>
    {
        public TireClassMap()
        {
            Map(m => m.Code).Name("STCODE");
            Map(m => m.Description).Name("DESCRIPN");
            Map(m => m.Section).Name("SECTION");
            Map(m => m.RimSize).Name("RIM");
            Map(m => m.Profile).Name("PROFILE");
            Map(m => m.FuelRating).Name("TYFUELC");
            Map(m => m.WetRating).Name("TYWGC");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}