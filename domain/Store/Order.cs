using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Order
    {
        public int Id { get; }

        private List<OrderItem> items;
        public IReadOnlyCollection<OrderItem> Items 
        {
            get { return items; } 
        }

        public int TotalCount => items.Sum(item => item.Count);

        public decimal TotalPrice => items.Sum(item => item.Price * item.Count);

        public Order(int id, IEnumerable<OrderItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items)); 
            }

            Id = id;
            this.items = new List<OrderItem>(items);
        }

        public OrderItem GetItem(int ballId)
        {
            int index = items.FindIndex(item => item.BallId ==  ballId);

            if (index == -1)
                ThrowItemException("Ball not found", ballId);

            return items[index];
        }


        public void AddOrUpdateItem(Ball ball, int count)
        {
            if (ball == null)
                throw new ArgumentNullException(nameof(ball));

            int index = items.FindIndex(item => item.BallId == ball.Id);
            if (index == -1)
            {
                items.Add(new OrderItem(ball.Id, count, ball.Price));
            }
            else
            {
                items[index].Count += count;
            }
        }
        public void RemoveItem(int ballId)
        {
            int index = items.FindIndex(item => item.BallId == ballId);

            if (index == -1)
                ThrowItemException("Order does not contain specified item.", ballId);

            items.RemoveAt(index);
        }

        private void ThrowItemException(string message, int ballId)
        {
            var exception = new InvalidOperationException(message);
            exception.Data["BallId"] = ballId;

            throw exception;
        }

    }
}
