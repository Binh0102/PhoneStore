using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string MemoryStorage { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Orderdetail> OrderDetails { get; set; }
    }
}
