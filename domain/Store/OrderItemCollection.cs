using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderItemCollection : IReadOnlyCollection<OrderItem>
    {
        private readonly List<OrderItem> items;
        public OrderItemCollection(IEnumerable<OrderItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            this.items = new List<OrderItem>(items);
        }
        public int Count => items.Count;
        public IEnumerator<OrderItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (items as IEnumerable).GetEnumerator();
        }
        public OrderItem Get(int ballId)
        {
            if (TryGet(ballId, out OrderItem orderItem))
                return orderItem;
            throw new InvalidOperationException("Ball not found.");
        }
        public bool TryGet(int ballId, out OrderItem orderItem)
        {
            var index = items.FindIndex(item => item.BallId == ballId);
            if (index == -1)
            {
                orderItem = null;
                return false;
            }
            orderItem = items[index];
            return true;
        }
        public OrderItem Add(int ballId, decimal price, int count)
        {
            if (TryGet(ballId, out OrderItem orderItem))
                throw new InvalidOperationException("Ball already exists.");
            orderItem = new OrderItem(ballId, price, count);
            items.Add(orderItem);
            return orderItem;
        }
        public void Remove(int ballId)
        {
            items.Remove(Get(ballId));
        }
    }
}
