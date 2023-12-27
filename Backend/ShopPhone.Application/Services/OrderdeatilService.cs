using AutoMapper;
using ShopPhone.Application.Dto;
using ShopPhone.Domain.Entities;
using ShopPhone.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Application.Services
{
    public class OrderdeatilService:IOrderdetailService
    {
        private readonly IOrderdetailRepo _orderdetailRepo;
        private readonly IMapper _mapper;
        public OrderdeatilService(IOrderdetailRepo orderdetailRepo, IMapper mapper)
        {
            _orderdetailRepo = orderdetailRepo;
            _mapper = mapper;
        }
        public List<OrderdetailDto> GetAll()
        {
            return _mapper.Map<List<OrderdetailDto>>(_orderdetailRepo.GetAll());
        }
        public OrderdetailDto Get(int id)
        {
            return _mapper.Map<OrderdetailDto>(_orderdetailRepo.Get(id));
        }
        public bool Add(OrderdetailDto orderdetail)
        {
            return _orderdetailRepo.Add(_mapper.Map<Orderdetail>(orderdetail));
        }
        public bool Update(OrderdetailDto orderdetail)
        {
            return _orderdetailRepo.Update(_mapper.Map<Orderdetail>(orderdetail));
        }
        public bool Delete(int id)
        {
            return _orderdetailRepo.Delete(id);
        }
    }
}
