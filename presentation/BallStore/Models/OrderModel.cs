﻿using Store;
using System.Collections.Generic;

namespace BallStore.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public OrderItemModel[] Items { get; set; } = new OrderItemModel[0];

        public int TotalCount {  get; set; }

        public decimal TotalPrice {  get; set; }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}