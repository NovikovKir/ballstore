using Moq;
using Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Web.App;

namespace BallStore.Tests
{
    public class BallServiceTests
    {
        [Fact]
        public void GetAllByQuery_WithBrand_CallsGetAllByBrandOrModel()
        {
            var ballRepositoryStub = new Mock<IBallRepository>();
            ballRepositoryStub.Setup(x => x.GetAllByBrandOrModel(It.IsAny<string>()))
                .Returns(new[] {new Ball(1, "", "", "", "", 1m) });

            var ballService = new BallService(ballRepositoryStub.Object);
            var validBrand = "MIKASA";

            var actual = ballService.GetAllByQuery(validBrand);

            Assert.Collection(actual, ball => Assert.Equal(1, ball.Id));
        }

        [Fact]
        public void GetAllByQuery_WithInvalidBrand_CallsGetAllByByBrandOrModel()
        {
            var ballRepositoryStub = new Mock<IBallRepository>();
            ballRepositoryStub.Setup(x => x.GetAllByBrandOrModel(It.IsAny<string>()))
                .Returns(new[] { new Ball(2, "", "", "", "", 0m )});

            var ballService = new BallService(ballRepositoryStub.Object);
            var invalidBrand = "     ";

            var actual = ballService.GetAllByQuery(invalidBrand);

            Assert.Collection(actual, ball => Assert.Equal(2, ball.Id));
        }
    }
}
