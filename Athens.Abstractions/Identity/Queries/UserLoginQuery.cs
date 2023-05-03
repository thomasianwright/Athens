using Athens.Abstractions.Identity.Models;
using Mediator;

namespace Athens.Abstractions.Identity.Queries;

public class UserLoginQuery : IQuery<LoginResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}