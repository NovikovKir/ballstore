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

        private int count;

        public int Count 
        { 
            get { return count; }
            set 
            {
                ThrowIfInvalidCount(value);
                count = value;
            }
        }

        public decimal Price { get; }

        public OrderItem(int ballId, decimal price, int count)
        {
            ThrowIfInvalidCount(count);

            BallId = ballId;
            Count = count;
            Price = price;
        }

        private static void ThrowIfInvalidCount(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Count must be greater than 0");
        }
    }
}
