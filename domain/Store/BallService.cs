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
        public Ball[] GetAllByQuery(string query)
        {
            if (Ball.IsBrandOrModel(query))
            {
                return ballRepository.GetAllByBrandOrModel(query);
            }
            return ballRepository.GetAllByBrandOrModel(query);
        }
    }
}
