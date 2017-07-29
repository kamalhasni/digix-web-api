using DigiX.Web.Api.Filters;
using DigiX.Web.Api.Models;
using DigiX.Web.Global.Objects;
using DigiX.Web.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiX.Web.Api.Controllers
{
    [BasicAuthentication]
    public class ProductsController : ApiController
    {
        private Lazy<ProductService> _productService = new Lazy<ProductService>();

        /// <summary>
        /// This method used for getting all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            var products = _productService.Value.GetProducts();
            if (products != null && products.Any())
                return Ok(products);
            else
                return NotFound();
        }

        /// <summary>
        /// This method used for getting specific product with its respective deals info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest();

            var product = _productService.Value.GetProduct(id);
            if (product != null)
            {
                var productModel = new ProductModel { Product = product };                
                productModel.Deals = _productService.Value.GetProductDeals(product.ProductId, 0, true);

                return Ok(productModel);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// This method used for adding new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddProduct([FromBody]Product product)
        {
            if (product == null)
                BadRequest();

            product = _productService.Value.AddProduct(product);
            return Ok(product);
        }

        /// <summary>
        /// This method used for updating the product info
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult UpdateProduct(int id, [FromBody]Product product)
        {
            if (id <= 0 || product == null)
                return BadRequest();

            product.ProductId = id;
            var updatedProduct = _productService.Value.GetProduct(product.ProductId);
            if (updatedProduct == null)
                BadRequest();

            _productService.Value.UpdateProduct(product);
            return Ok();
        }
    }
}
