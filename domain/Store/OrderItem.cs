using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderItem
    {
        public int BallId { get; }

        public int Count { get; }

        public decimal Price { get; }

        public OrderItem(int ballId, int count, decimal price) 
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Count must be greater than 0"); 

            BallId = ballId;
            Count = count;
            Price = price;
        }
    }
}
