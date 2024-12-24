using Store.Web.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class BallService
    {
        private readonly IBallRepository ballRepository;

        public BallService(IBallRepository ballRepository)
        {
            this.ballRepository = ballRepository;
        }

        public BallModel GetById(int id)
        {
            var ball = ballRepository.GetById(id);
            return Map(ball);
        }
        public IReadOnlyCollection<BallModel> GetAllByQuery(string query)
        {
            var balls = Ball.IsBrandOrModel(query.ToUpper())
                ? ballRepository.GetAllByBrandOrModel(query.ToUpper())
                : ballRepository.GetAllByBrandOrModel(query.ToUpper());
            return balls.Select(Map)
                        .ToArray();
        }
        private BallModel Map(Ball ball)
        {
            return new BallModel
            {
                Id = ball.Id,
                Name = ball.Name,
                Brand = ball.Brand,
                Model = ball.Model,
                Description = ball.Description,
                Price = ball.Price,
            };
        }
    }
}
