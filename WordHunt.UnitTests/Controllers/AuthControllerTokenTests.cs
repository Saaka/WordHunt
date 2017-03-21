using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using WordHunt.WebAPI.Auth.Token;
using WordHunt.WebAPI.Controllers;
using WordHunt.WebAPI.Models;
using Xunit;

namespace WordHunt.UnitTests.Controllers
{
    public class AuthControllerTokenTests
    {
        [Fact]
        public async void Calls_GenerateToken_For_AnyValidParameters()
        {
            CredentialsModel model = new CredentialsModel()
            {
                Email = "valid@email.com",
                Password = "validpassword"
            };
            Mock<ILogger<AuthController>> logger = new Mock<ILogger<AuthController>>();
            Mock<ITokenGenerator> tokenGenMock = new Mock<ITokenGenerator>();
            tokenGenMock.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(new TokenGeneratorResult() { ResultStatus = TokenGeneratorResultStatus.Success }).Verifiable();

            AuthController controller = new AuthController(tokenGenMock.Object, logger.Object);

            var result = await controller.CreateToken(model) as ObjectResult;

            tokenGenMock.Verify();
        }

        [Fact]
        public async void Returns_OkStatusCode_For_ValidRequest()
        {
            CredentialsModel model = new CredentialsModel()
            {
                Email = "valid@email.com",
                Password = "validpassword"
            };

            Mock<ILogger<AuthController>> logger = new Mock<ILogger<AuthController>>();
            Mock<ITokenGenerator> tokenGenMock = new Mock<ITokenGenerator>();

            tokenGenMock.Setup(x=> x.GenerateToken(model.Email, model.Password))
                    .ReturnsAsync(new TokenGeneratorResult() { ResultStatus = TokenGeneratorResultStatus.Success}).Verifiable("GenerateToken was not called");
            
            AuthController controller = new AuthController(tokenGenMock.Object, logger.Object);

            var result = await controller.CreateToken(model) as ObjectResult;
            
            tokenGenMock.Verify();
            Assert.Equal(result.StatusCode, (int?)HttpStatusCode.OK);
        }
    }
}
