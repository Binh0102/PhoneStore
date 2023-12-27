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
    public class ProductService:IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        public List<ProductDto> GetAll()
        {
            return _mapper.Map<List<ProductDto>>(_productRepo.GetAll());
        }
        public ProductDto Get(int id)
        {
            return _mapper.Map<ProductDto>(_productRepo.Get(id));
        }
        public bool Add(ProductDto product)
        {
            return _productRepo.Add(_mapper.Map<Product>(product));
        }
        public bool Update(ProductDto product)
        {
            return _productRepo.Update(_mapper.Map<Product>(product));
        }
        public bool Delete(int id)
        {
            return _productRepo.Delete(id);
        }
    }
}
