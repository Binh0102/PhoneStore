using ShopPhone.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Application.Services
{
    public interface IOrderdetailService
    {
        List<OrderdetailDto> GetAll();
        OrderdetailDto Get(int id);
        bool Add(OrderdetailDto orderdetail);
        bool Update(OrderdetailDto orderdetail);
        bool Delete(int id);
    }
}
