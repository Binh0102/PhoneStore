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
    public class OrderService:IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepo orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }
        public List<OrderDto> GetAll()
        {
            return _mapper.Map<List<OrderDto>>(_orderRepo.GetAll());
        }
        public OrderDto Get(int id)
        {
            return _mapper.Map<OrderDto>(_orderRepo.Get(id));
        }
        public bool Add(OrderDto order)
        {
            return _orderRepo.Add(_mapper.Map<Order>(order));
        }
        public bool Update(OrderDto order)
        {
            return _orderRepo.Update(_mapper.Map<Order>(order));
        }
        public bool Delete(int id)
        {
            return _orderRepo.Delete(id);
        }
    }
}
