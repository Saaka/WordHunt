using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using WordHunt.WebAPI.Auth.Token;
using WordHunt.WebAPI.Controllers.Auth;
using WordHunt.WebAPI.Models;
using Xunit;

namespace WordHunt.Web.UnitTests.Controllers
{
    public class AuthControllerTokenTests
    {
        [Fact]
        public async void Calls_GenerateToken_For_AnyValidParameters()
        {
            CredentialsModel model = new CredentialsModel()
            {
                UserName = "validname",
                Password = "validpassword"
            };
            Mock<ILogger<AuthController>> logger = new Mock<ILogger<AuthController>>();
            Mock<ITokenGenerator> tokenGenMock = new Mock<ITokenGenerator>();
            tokenGenMock.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(new TokenGeneratorResult()).Verifiable("GenerateToken was not called");

            AuthController controller = new AuthController(tokenGenMock.Object, logger.Object);

            var result = await controller.CreateToken(model) as TokenGeneratorResult;

            tokenGenMock.Verify();
        }
    }
}
