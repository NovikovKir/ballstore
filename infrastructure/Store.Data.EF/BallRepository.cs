using System.Linq;
using System;

namespace Store.Data.EF
{
    public class BallRepository : IBallRepository
    {
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
