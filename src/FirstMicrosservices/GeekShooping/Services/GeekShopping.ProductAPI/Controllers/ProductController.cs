using GeekShopping.ProductAPI.Data.DTOs;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository) => 
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> FindAll() =>
        Ok(await _productRepository.FindAllAsync());

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDTO>> FindById(long productId)
    {
        var product = await _productRepository.FindByIdAsync(productId);
        if(product.Id <= 0) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDTO)
    {
        if(productDTO is null) return BadRequest();
        var product = await _productRepository.CreateAsync(productDTO);
        
        return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDTO>> Update(ProductDTO productDTO)
    {
        if (productDTO is null) return BadRequest();
        var product = await _productRepository.UpdateAsync(productDTO);

        return Ok(product);
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult> Delete(long productId)
    {
        var status = await _productRepository.DeleteAsync(productId);
        if(!status) return NotFound();

        return Ok(status);  
    }
}
