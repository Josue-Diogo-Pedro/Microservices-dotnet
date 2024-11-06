using AutoMapper;
using GeekShopping.ProductAPI.Data.DTOs;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly MYSQLContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(MYSQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> FindAllAsync()
        => _mapper.Map <IEnumerable<ProductDTO>>(await _context?.Products?
                                                                .AsNoTracking()?
                                                                .ToListAsync());

    public async Task<ProductDTO> FindByIdAsync(long id)
        => _mapper.Map<ProductDTO>(await _context?.Products?
                                         .AsNoTracking()?.
                                          SingleOrDefaultAsync(product => product.Id == id));

    public async Task<ProductDTO> CreateAsync(ProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);
        await _context.AddAsync(product);
        return await SaveChangesAsync() ? _mapper.Map<ProductDTO>(productDTO) : null;
    }

    public async Task<ProductDTO> UpdateAsync(ProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);
        _context?.Products.Update(product);
        return await SaveChangesAsync() ? _mapper.Map<ProductDTO>(product) : null;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var product = await FindByIdAsync(id);
            if (product.Id <= 0) return false;

            _context?.Products?.Remove(_mapper.Map<Product>(product));
            return await SaveChangesAsync();
        }
        catch
        {
            return false;
        }
    }

    private async Task<bool> SaveChangesAsync() => await _context?.SaveChangesAsync() > 0;
}
