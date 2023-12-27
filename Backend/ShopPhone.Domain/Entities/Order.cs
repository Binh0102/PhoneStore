using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public double Total { get; set; }
        public DateTime Orderdate { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
