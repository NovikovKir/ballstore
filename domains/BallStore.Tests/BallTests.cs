using Store;

namespace BallStore.Tests
{
    public class BallTests
    {
        [Fact]
        public void IsBrandOrModel_WithNull_ReturnFalse()
        {
            bool actual = Ball.IsBrandOrModel(null);

            Assert.False(actual);
        }
    }
}