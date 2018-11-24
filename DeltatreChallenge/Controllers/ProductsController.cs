using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeltatreChallenge.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        // GET api/products
        [HttpGet]
        public List<Product> Get()
        {
            // I'll need to return an array/list of products
            var products = ProductRepository.RepoInstance.GetProducts();

            return products;
        }

        // GET api/products/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "product2";
        //}

        // POST api/products
        [HttpPost]
        public List<Product> Post([FromBody]Product product)
        {
            // If product with name already exists, retun error
           
            // Add new product to memory
            var addedProduct = ProductRepository.RepoInstance.AddProduct(product);

            // return list of products with newly added product to client
            var newList = ProductRepository.RepoInstance.GetProducts();
            return newList;
        }

        // PUT api/products/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string product)
        //{

        //}

        // DELETE api/products/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }

    public sealed class ProductRepository
    {
        private static ProductRepository instance = null;

        private readonly List<Product> products = new List<Product>();

        private ProductRepository()
        {
            products = new List<Product>()
            {
                new Product {Name = "TV", Desc = "1080p Flat Screen", Quantity = 5},
                new Product {Name = "Dishwasher", Desc = "Low Water, High Effeciency Dishwasher", Quantity = 3},
                new Product {Name = "Lightbulb", Desc = "High Effeciency LED Lightbulb", Quantity = 8}
            };
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public Product AddProduct(Product product)
        {
            bool alreadyExists = products.Any(item => item.Name == product.Name);

            if (alreadyExists)
            {
                throw new Exception("Item already exists");
            }
            try
            {
                products.Add(product);
                return product;
            }
            catch
            {
                throw new System.SystemException("Couldn't add product");
            }
        }

        public static ProductRepository RepoInstance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ProductRepository();
                }
                return instance;
            }
        }
    }


    public class Product
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Quantity { get; set; }
    }
}
