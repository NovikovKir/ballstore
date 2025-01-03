﻿using Store.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderItemCollection : IReadOnlyCollection<OrderItem>
    {
        private readonly OrderDto orderDto;
        private readonly List<OrderItem> items;
        public OrderItemCollection(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ArgumentNullException(nameof(orderDto));
            this.orderDto = orderDto;
            items = orderDto.Items
                            .Select(OrderItem.Mapper.Map)
                            .ToList();
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

            var orderItemDto = OrderItem.DtoFactory.Create(orderDto, ballId, price, count);
            orderDto.Items.Add(orderItemDto);
            orderItem = OrderItem.Mapper.Map(orderItemDto);

            items.Add(orderItem);

            return orderItem;
        }
        public void Remove(int ballId)
        {
            items.Remove(Get(ballId));

            var index = items.FindIndex(item => item.BallId == ballId);
            if (index == -1)
                throw new InvalidOperationException("Can't find ball to remove from order.");
            orderDto.Items.RemoveAt(index);
            items.RemoveAt(index);
        }
    }
}
