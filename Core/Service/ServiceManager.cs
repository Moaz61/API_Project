using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy< IBasketService> _BasketService=new Lazy<IBasketService>(() => new BasketService(basketRepository,mapper));

        public IProductService ProductService => _LazyProductService.Value;

        public IBasketService BasketService => _BasketService.Value;
    }
}
