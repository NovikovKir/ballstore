using System.Linq;
using System;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF
{
    public class BallRepository : IBallRepository
    {
        private readonly DbContextFactory dbContextFactory;

        public BallRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public Ball[] GetAllByIds(IEnumerable<int> ballIds)
        {
            var dbContext = dbContextFactory.Create(typeof(BallRepository));

            return dbContext.Balls
                            .Where(ball => ballIds.Contains(ball.Id))
                            .AsEnumerable()
                            .Select(Ball.Mapper.Map)
                            .ToArray();
        }

        public Ball[] GetAllByBrandOrModel(string brandOrModel)
        {
            var dbContext = dbContextFactory.Create(typeof(BallRepository));
            var parameter = new SqlParameter("@brandOrModel", brandOrModel);
            return dbContext.Balls
                            .FromSqlRaw("SELECT * FROM Balls WHERE CONTAINS((Brand, Model), @brandOrModel)",
                                        parameter)
                            .AsEnumerable()
                            .Select(Ball.Mapper.Map)
                            .ToArray();
        }

        

        public Ball GetById(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(BallRepository));
            var dto = dbContext.Balls
                               .Single(ball => ball.Id == id);

            return Ball.Mapper.Map(dto);
        }
    }
}
