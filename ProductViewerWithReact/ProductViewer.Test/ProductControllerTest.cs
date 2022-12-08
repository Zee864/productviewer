namespace ProductViewer.Test;

/// <summary>
/// Contains the test methods for the <see cref="ProductController"/> class.
/// </summary>
public class ProductControllerTest
{
    /// <summary>
    /// Tests the Get method.
    /// </summary>
    [Fact]
    public void GetTest()
    {
        #region Arrange

        // Create a mock IProductsApi
        var mockProductsApi = new Mock<IProductsApi>();
        var controller = new ProductController(mockProductsApi.Object);
        // Create a mock list of products to return
        var mockProducts = new List<Product>
        {
            new () { Id = 1, Name = "Product 1", Category = 1, Price = 1.99m },
            new () { Id = 2, Name = "Product 2", Category = 2, Price = 2.99m },
            new () { Id = 3, Name = "Product 3", Category = 3, Price = 3.99m }
        };
        // Create a mock method for the RetrieveAllProductsAsync method
        mockProductsApi.Setup(x => x.RetrieveAllProductsAsync()).Returns(mockProducts);

        #endregion
        
        #region Act

        var result = controller.Get();

        #endregion
        
        #region Assert
        
        Assert.NotNull(result);
        Assert.IsType<List<Product>>(result);
        Assert.Equal(mockProducts, result);
        
        #endregion
        
    }
    
    /// <summary>
    /// Tests the Get method with an id.
    /// </summary>
    [Fact]
    public void GetWithIdTest()
    {
        #region Arrange

        // Create a mock IProductsApi
        var mockProductsApi = new Mock<IProductsApi>();
        var controller = new ProductController(mockProductsApi.Object);
        // Create a mock product to return
        var mockProduct = new Product { Id = 1, Name = "Product 1", Category = 1, Price = 1.99m };
        // Create a mock method for the RetrieveProductAsync method
        mockProductsApi.Setup(x => x.RetrieveProductAsync(1)).Returns(mockProduct);

        #endregion
        
        #region Act

        var result = controller.Get(1);

        #endregion
        
        #region Assert
        
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
        Assert.Equal(mockProduct, result);
        
        #endregion
        
    }
    
    /// <summary>
    /// Tests the Post method.
    /// </summary>
    [Fact]
    public void PostTest()
    {
        #region Arrange

        // Create a mock IProductsApi
        var mockProductsApi = new Mock<IProductsApi>();
        var controller = new ProductController(mockProductsApi.Object);
        // Create a mock product that will be used to create a new product
        var mockProduct = new Product { Id = 1, Name = "Product 1", Category = 1, Price = 1.99m };
        // Create a mock method for the CreateProductAsync method
        mockProductsApi.Setup(x => x.CreateProductAsync(mockProduct)).Returns(true);

        #endregion
        
        #region Act

        var result = controller.Post(mockProduct);

        #endregion
        
        #region Assert
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
        
        #endregion
        
    }
    
    /// <summary>
    /// Tests the Put method.
    /// </summary>
    [Fact]
    public void PutTest()
    {
        #region Arrange

        // Create a mock IProductsApi
        var mockProductsApi = new Mock<IProductsApi>();
        var controller = new ProductController(mockProductsApi.Object);
        // Create a mock product that will be used to update the product
        var mockProduct = new Product { Name = "Product 1", Category = 1, Price = 1.99m };
        // Create a mock method for the UpdateProductAsync method
        mockProductsApi.Setup(x => x.UpdateProductAsync(1,mockProduct)).Returns(true);

        #endregion
        
        #region Act

        var result = controller.Put(1,mockProduct);

        #endregion
        
        #region Assert
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
        
        #endregion
        
    }
    
    /// <summary>
    /// Tests the Delete method.
    /// </summary>
    [Fact]
    public void DeleteTest()
    {
        #region Arrange

        // Create a mock IProductsApi
        var mockProductsApi = new Mock<IProductsApi>();
        var controller = new ProductController(mockProductsApi.Object);
        // Create a mock method for the DeleteProductAsync method
        mockProductsApi.Setup(x => x.DeleteProductAsync(1)).Returns(true);

        #endregion
        
        #region Act

        var result = controller.Delete(1);

        #endregion
        
        #region Assert
        
        Assert.NotNull(result);
        Assert.IsType<bool>(result);
        Assert.Equal(true, result);
        
        #endregion
        
    }
}