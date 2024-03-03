using Microsoft.AspNetCore.Mvc;
using RabbitMQProductAPI.Publisher.Models;
using RabbitMQProductAPI.Publisher.Services;

namespace RabbitMQProductAPI.Publisher.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IRabbitMQProducer _rabitMQProducer;

        public ProductController(IProductService _productService, IRabbitMQProducer rabitMQProducer)
        {
            productService = _productService;
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpGet("ProductList")]
        public IEnumerable<Product> ProductList()
        {
            var productList = productService.GetProductList();
            return productList;

        }
        [HttpGet("GetProductById")]
        public Product GetProductById(int Id)
        {
            return productService.GetProductById(Id);
        }

        [HttpPost("AddProduct")]
        public Product AddProduct(Product product)
        {
            var productData = productService.AddProduct(product);

            //eklenen ürün verilerini kuyruğa gönder ve consumer bu verileri kuyruktan dinleyecek
            _rabitMQProducer.SendProductMessage(productData);

            return productData;
        }

        [HttpPut("UpdateProduct")]
        public Product UpdateProduct(Product product)
        {
            return productService.UpdateProduct(product);
        }

        [HttpDelete("DeleteProduct")]
        public bool DeleteProduct(int Id)
        {
            return productService.DeleteProduct(Id);
        }
    }
}
