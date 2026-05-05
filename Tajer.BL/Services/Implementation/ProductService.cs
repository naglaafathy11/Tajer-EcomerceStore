using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tajer.BL.DTO;
using Tajer.BL.Services.Interfaces;
using Tajer.DAL.Models;
using Tajer.DAL.Repo.Interfaces;

namespace Tajer.BL.Services.Implementation
{
    public class ProductService(IUnitOfWork _unit , IMapper _mapper) : IProductService
    {
        

       

        public void DeleteProduct(int id)
        {
            var Repo = _unit.GetRepo<Product, int>();
            Repo.Delete(id);
        }




        public void UpdateProduct(ProductDTO productdto)
        {
            var Repo = _unit.GetRepo<Product, int>();
            var product = _mapper.Map<Product>(productdto);
            Repo.Update(product);
        }

       public async Task<IEnumerable<ProductDTO>>GetAllProducts()
        {
            var Repo = _unit.GetRepo<Product, int>();
            var products = await  Repo.GetAll();
            var result =   _mapper.Map<IEnumerable<ProductDTO>>(products);
            return  result;

        }

      public  async Task<ProductDTO> GetProductById(int id)
        {
            var Repo = _unit.GetRepo<Product, int>();
            var product =await  Repo.GetById(id);
            if(product == null)
                {
                throw new Exception("Product not found");
            }
            else
            {
                var result = _mapper.Map<ProductDTO>(product);
                return result;
            }


        }

        void IProductService.AddProduct(ProductDTO productdto)
        {
            var Repo = _unit.GetRepo<Product, int>();
            var product = _mapper.Map<Product>(productdto);
             Repo.Add(product);
            _unit.Save();
        }
    }
}
