using System.Linq;
using System;
namespace Store.Memory
{
    public class BallRepository : IBallRepository
    {
        private readonly Ball[] balls = new[]
        {
            new Ball(1, "Волейбольный мяч", "DEMIX", "VLPU440 Super Touch"),
            new Ball(2, "Волейбольный мяч", "TORRES", "BM850 V42325"),
            new Ball(3, "Волейбольный мяч", "MIKASA", "V330W"),
            new Ball(4, "Волейбольный мяч", "MIKASA", "V200W"),
        };


        public Ball[] GetAllByBrandOrModel(string query)
        {
            return balls.Where(ball => ball.Brand.Contains(query) || ball.Model.Contains(query))
                .ToArray();

        }
    }
}
