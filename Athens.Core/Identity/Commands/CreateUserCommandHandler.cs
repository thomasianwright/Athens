using Athens.Abstractions.Identity.Commands;
using Athens.Abstractions.Identity.Models;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace Athens.Core.Identity.Commands;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async ValueTask<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
        
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
        }
        
        await _userManager.AddToRoleAsync(user, "User");
        
        return Unit.Value;
    }
}