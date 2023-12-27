using ShopPhone.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Application.Services
{
    public interface IOrderService
    {
        List<OrderDto> GetAll();
        OrderDto Get(int id);
        bool Add(OrderDto order);
        bool Update(OrderDto order);
        bool Delete(int id);
    }
}
