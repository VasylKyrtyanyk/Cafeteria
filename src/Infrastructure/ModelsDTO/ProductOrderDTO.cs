﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ModelsDTO
{
    public class ProductOrderDTO
    {
        public int ProductId { get; set; }
        //TODO Add productDTO
        //public ProductDTO Product { get; set; }

        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
    }
}