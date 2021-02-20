﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderItem
    {
        public int BookId { get; }

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

        public OrderItem(int bookId, decimal price, int count)
        {
            ThrowIfInvalidCount(count);

            BookId = bookId;            
            Price = price;
            Count = count;
        }

        private static void ThrowIfInvalidCount(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Count must be greater than zero.");
        }
    }
}
