using Microsoft.AspNetCore.Mvc;
using ProductViewerWithReact.Models;
using ProductViewerWithReact.Services;

namespace ProductViewerWithReact.Controllers;

/// <summary>
/// Controller for the Product API endpoint
/// </summary>
[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    #region Properties
    
    private readonly IProductsApi _productsApi;

    #endregion

    #region Constructor
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productsApi">Class for interacting with the products api. Initialized via dependency injection in the constructor</param>
    public ProductController(IProductsApi productsApi)
    {
        _productsApi = productsApi;
    }

    #endregion

    #region Methods

    #region Get Routes
    
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>A list of products or null if there are no products</returns>
    [HttpGet]
    public List<Product>? Get()
    {
        return _productsApi.RetrieveAllProductsAsync();
    }
    
    /// <summary>
    /// Gets a product by id
    /// </summary>
    /// <param name="id">The id of the product</param>
    /// <returns>The specific product or null if there is no product with that id</returns>
    [HttpGet("{id}")]
    public Product? Get(int id)
    {
        return _productsApi.RetrieveProductAsync(id);
    }
    
    #endregion
    
    #region Post Route

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">Contains details of the new product to be created <see cref="Product"/> </param>
    /// <returns>Truthy value indicating whether the creation was successful or not</returns>
    [HttpPost]
    public bool Post(Product product)
    {
        return _productsApi.CreateProductAsync(product);
    }

    #endregion
    
    #region Put Route
    
    /// <summary>
    /// Updates an existing product with new values
    /// </summary>
    /// <param name="id">Id of the product to update</param>
    /// <param name="product">Contains details to which the product should be updated to </param>
    /// <returns>Truthy value indicating whether the update was successful or not</returns>
    [HttpPut("{id}")]
    public bool Put(int id, Product product)
    {
        return _productsApi.UpdateProductAsync(id, product);
    }

    #endregion
    
    #region Delete Route
    
    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id">Id of the product to be deleted</param>
    /// <returns>Truthy value indicating whether the deletion was successful or not</returns>
    [HttpDelete("{id}")]
    public bool Delete(int id)
    {
        return _productsApi.DeleteProductAsync(id);
    }

    #endregion
    
    #endregion
}