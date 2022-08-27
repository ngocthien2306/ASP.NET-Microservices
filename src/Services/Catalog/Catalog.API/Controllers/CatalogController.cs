using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Products>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
            var products =  await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Products>> GetProductById(string id)
        {
            var product = await _productRepository.GetProductsById(id);
            if(product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/catogory", Name = "GetProductCatogory")]
        [HttpGet]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Products>> GetProductCatogoryName(string catogoryName)
        {
            var product = await _productRepository.GetProdyctByCatogory(catogoryName);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Products>> CreateProduct([FromBody] Products product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] Products product)
        {
            return Ok(await _productRepository.UpdateProduct(product));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProduct(string pid)
        {
            return Ok(await _productRepository.DeleteProduct(pid));
        }
    }
}
