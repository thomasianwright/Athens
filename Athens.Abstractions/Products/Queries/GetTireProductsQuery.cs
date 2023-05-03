using Athens.Abstractions.Products.Models;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Athens.Abstractions.Products.Queries;

public class GetTireProductsQuery : IQuery<IEnumerable<TireProduct>>
{
    [FromQuery] public string? Search { get; set; }
    [FromQuery] public int? Profile { get; set; }
    [FromQuery] public int? Section { get; set; }
    [FromQuery] public int? RimSize { get; set; }
    [FromQuery] public char? WetRating { get; set; }
    [FromQuery] public char? FuelRating { get; set; }
}