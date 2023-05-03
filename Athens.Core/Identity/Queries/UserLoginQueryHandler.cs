using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Athens.Abstractions.Identity.Models;
using Athens.Abstractions.Identity.Queries;
using Athens.Core.Configuration;
using Athens.Core.Identity.Helpers;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Athens.Core.Identity.Queries;

public class UserLoginQueryHandler : IQueryHandler<UserLoginQuery, LoginResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly ITokenHelper _tokenHelper;
    private readonly JwtConfiguration _jwtConfiguration;

    public UserLoginQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager,
        IOptions<JwtConfiguration> jwtConfiguration, ITokenHelper tokenHelper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenHelper = tokenHelper;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async ValueTask<LoginResponse> Handle(UserLoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(query.Username);

        if (user is null)
        {
            // TODO: Fake timing for user enumeration
            throw new Exception("Username or password not found");
        }

        var result = await _userManager.CheckPasswordAsync(user, query.Password);

        if (!result) throw new Exception("Username or password not found");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenHelper.GenerateAccessToken(user, roles);
        var refreshToken = _tokenHelper.GenerateRefreshToken(user);
        
        return new LoginResponse
        {
            Token = tokenHandler.WriteToken(token),
            RefreshToken = tokenHandler.WriteToken(refreshToken),
        };
    }
}