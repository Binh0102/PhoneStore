﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Application.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string MemoryStorage { get; set; }
        public int CategoryId { get; set; }
    }
}
