using System.Linq;
using System;
namespace Store.Memory
{
    public class BallRepository : IBallRepository
    {
        private readonly Ball[] balls = new[]
        {
            new Ball(1, "Мяч волейбольный Demix VLPU440 Super Touch"),
            new Ball(2, "Мяч волейбольный TORRES BM850 V42325"),
            new Ball(3, "Мяч волейбольный MIKASA V330W"),
        };


        public Ball[] GetAllByTitle(string titlePart)
        {
            return balls.Where(ball => ball.Name.Contains(titlePart))
                    .ToArray();
        }
    }
}
