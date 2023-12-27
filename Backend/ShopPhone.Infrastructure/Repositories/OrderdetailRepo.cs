using ShopPhone.Domain.Entities;
using ShopPhone.Domain.Repositories;
using ShopPhone.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Infrastructure.Repositories
{
    public class OrderdetailRepo:Repo<Orderdetail> , IOrderdetailRepo
    {
        public OrderdetailRepo(ShopDbContext context) : base(context) { }
    }
}
