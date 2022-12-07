using Microsoft.AspNetCore.Mvc;
using ProductViewerWithReact.Services;

namespace ProductViewerWithReact.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    #region Properties

    private readonly IProductsApi _productsApi;

    #endregion

    #region Constructor

    public ProductController(IProductsApi productsApi)
    {
        _productsApi = productsApi;
    }

    #endregion

    #region Methods

    #region Get Routes

    [HttpGet]
    public List<Product>? Get()
    {
        return _productsApi.RetrieveAllProductsAsync();
    }

    [HttpGet("{id}")]
    public Product? Get(int id)
    {
        return _productsApi.RetrieveProductAsync(id);
    }
    
    #endregion
    
    #region Post Route

    [HttpPost]
    public bool Post(Product product)
    {
        return _productsApi.CreateProductAsync(product);
    }

    #endregion
    
    #region Put Route

    [HttpPut("{id}")]
    public bool Put(int id, Product product)
    {
        return _productsApi.UpdateProductAsync(id, product);
    }

    #endregion
    
    #region Delete Route

    [HttpDelete("{id}")]
    public bool Delete(int id)
    {
        return _productsApi.DeleteProductAsync(id);
    }

    #endregion
    
    #endregion
}