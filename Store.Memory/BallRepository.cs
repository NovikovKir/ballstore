using System.Linq;
using System;

namespace Store.Memory
{
    public class BallRepository : IBallRepository
    {
        /*private readonly Ball[] balls = new[]
        {
            new Ball(1, "Волейбольный мяч", "DEMIX", "VLPU440 Super Touch", "Волейбольный мяч Demix для тренировок и игр в зале и на открытых площадках.", 1999m),
            new Ball(2, "Волейбольный мяч", "TORRES", "BM850 V42325", "Волейбольный мяч BM850 — это классическая модель волейбольного мяча, самая универсальная из всего модельного ряда мячей Torres.", 3150m),
            new Ball(3, "Волейбольный мяч", "MIKASA", "V330W", "Волейбольный мяч для тренировок и любительских соревнований от Mikasa.", 7999m),
            new Ball(4, "Волейбольный мяч", "MIKASA", "V200W", "Официальный игровой мяч Mikasa предназначен для проведения соревнований самого высокого уровня.", 14999m),
        };


        public Ball[] GetAllByBrandOrModel(string query)
        {
            return balls.Where(ball => ball.Brand.Contains(query) || ball.Model.Contains(query))
                .ToArray();

        }

        public Ball[] GetAllByIds(IEnumerable<int> ballIds)
        {
            var foundBalls = from ball in balls
                             join ballId in ballIds on ball.Id equals ballId
                             select ball;
            return foundBalls.ToArray();
        }

        public Ball GetById(int id)
        {
            return balls.Single(ball => ball.Id == id);
        }*/
        public Ball[] GetAllByBrandOrModel(string brandOrModel)
        {
            throw new NotImplementedException();
        }

        public Ball[] GetAllByIds(IEnumerable<int> ballIds)
        {
            throw new NotImplementedException();
        }

        public Ball GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}