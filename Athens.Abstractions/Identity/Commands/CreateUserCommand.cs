using Mediator;

namespace Athens.Abstractions.Identity.Commands;

public class CreateUserCommand : ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}