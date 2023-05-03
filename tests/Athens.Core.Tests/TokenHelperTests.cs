using Athens.Abstractions.Identity.Models;
using Athens.Core.Configuration;
using Athens.Core.Identity.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Athens.Core.Tests;

public class TokenHelperTests
{
    public TokenHelperTests()
    {
    }

    [Fact]
    public void GenerateAccessToken_Returns()
    {
        // Arrange
        var mockJwtSettings = new Mock<IOptions<JwtConfiguration>>();
        mockJwtSettings.Setup(x => x.Value)
            .Returns(new JwtConfiguration
            {
                Secret = "sddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd"
            });

        var tokenHelper = new TokenHelper(mockJwtSettings.Object);

        // Act
        var token = tokenHelper.GenerateAccessToken(new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "test",
            Email = "t@t.com",
            FirstName = "test",
            LastName = "test",
        }, new[] { "test" });

        // Assert
        token.Should().NotBeNull();

        token.ValidTo.Should().BeBefore(DateTime.Now.AddMinutes(1));
    }
}