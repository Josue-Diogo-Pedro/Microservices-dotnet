using GeekShopping.ProductAPI.Data.DTOs;

namespace GeekShopping.ProductAPI.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductDTO>> FindAllAsync();
    Task<ProductDTO> FindByIdAsync(long id);
    Task<ProductDTO> CreateAsync(ProductDTO productDTO);
    Task<ProductDTO> UpdateAsync(ProductDTO productDTO);
    Task<bool> DeleteAsync(long id); 
}
