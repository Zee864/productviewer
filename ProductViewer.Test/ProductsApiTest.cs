namespace ProductViewer.Test;

/// <summary>
/// Contains the tests for the <see cref="ProductsApi"/> class.
/// </summary>
public class ProductsApiTest
{
    #region RetrieveProducts Tests

    /// <summary>
    /// Testing a successful call to the RetrieveProductAsync method.
    /// </summary>
    /// <exception cref="InvalidOperationException">Exception thrown when result is null instead of a valid response</exception>
    [Fact]
    public void RetrieveProductAsyncSuccessTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock product to return
        var mockProduct = new Product()
        {
            Id = 1,
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(mockProduct))
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.RetrieveProductAsync(1);

        #endregion

        #region Assert

        // if result is null, the test will fail
        Assert.NotNull(result);
        // check if the result contains the expected values
        // trim the result for any whitespace
        var trimmedResult = JsonConvert.SerializeObject(result).Trim();
        Assert.Equal(JsonConvert.SerializeObject(mockProduct).Trim(), trimmedResult);

        #endregion
    }
    
    /// <summary>
    /// Testing a failed call to the RetrieveProductAsync method.
    /// </summary>
    [Fact]
    public void RetrieveProductAsyncUnsuccessfulTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent("Request Failed")
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.RetrieveProductAsync(1);

        #endregion
        
        #region Assert

        // Result should be null as the request failed
        Assert.Null(result);

        #endregion
    }
    
    /// <summary>
    /// Testing an unsuccessful call to the RetrieveProductAsync method where the JSON response is invalid.
    /// </summary>
    [Fact]
    public void RetrieveProductAsyncInvalidJsonTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a list of mock products to return
        var mockProducts = new List<Product>()
        {
            new()
            {
                Id = 1,
                Name = "Test Product",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 2,
                Name = "Test Product 2",
                Category = 3,
                Price = (decimal?) 10.0,
            }
        };
        
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(mockProducts))
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.RetrieveProductAsync(1);

        #endregion

        #region Assert

        // Exception should be logged and result should be null
        // check if the exception was logged
        mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
        Assert.Null(result);

        #endregion
    }
    
    /// <summary>
    /// Testing a successful call to the RetrieveAllProductAsync method.
    /// </summary>
    /// <exception cref="InvalidOperationException">Exception thrown when result is null instead of a valid response</exception>
    [Fact]
    public void RetrieveAllProductAsyncSuccessTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a list of mock products to return
        var mockProducts = new List<Product>()
        {
            new()
            {
                Id = 1,
                Name = "Test Product",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 2,
                Name = "Test Product 2",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 3,
                Name = "Test Product 3",
                Category = 3,
                Price = 10,
            }
        };
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(mockProducts))
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.RetrieveAllProductsAsync();

        #endregion

        #region Assert

        // if result is null, the test will fail
        Assert.NotNull(result);
        // trim the result for any whitespace
        var trimmedResult = (result ?? throw new InvalidOperationException()).Select(x => x.Name?.Trim());
        // check if the result contains the expected values
        Assert.Equal(mockProducts.Select(x => x.Name?.Trim()), trimmedResult);

        #endregion

    }
    
    /// <summary>
    /// Testing a failed call to the RetrieveAllProductAsync method.
    /// </summary>
    [Fact]
    public void RetrieveAllProductAsyncUnsuccessfulTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent("Request Failed")
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.RetrieveAllProductsAsync();

        #endregion
        
        #region Assert

        // Result should be null as the request failed
        Assert.Null(result);

        #endregion
    }
    
    /// <summary>
    /// Testing an unsuccessful call to the RetrieveAllProductsAsync method where the JSON response is invalid.
    /// </summary>
    [Fact]
    public void RetrieveAllProductsAsyncInvalidJsonTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock product to return
        var mockProduct = new Product()
        {
            Id = 1,
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(mockProduct))
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.RetrieveAllProductsAsync();

        #endregion

        #region Assert

        // Exception should be logged and result should be null
        // check if the exception was logged
        mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
        Assert.Null(result);

        #endregion
    }
    
    /// <summary>
    /// Testing a successful call to the RetrieveProductsByFilter method.
    /// </summary>
    /// <exception cref="InvalidOperationException">Exception thrown when result is null instead of a valid response</exception>
    [Fact]
    public void RetrieveProductsByFilterSuccessTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock filter to return
        var mockFilter = new ProductFilter()
        {
            NumberOfPages = 3,
            PageSize = 10,
            OrderBy = "Category",
            Ascending = false,
            Filter = null
        };
        // Create a list of mock products to return based on the filter
        var mockProducts = new List<Product>()
        {
            new()
            {
                Id = 1,
                Name = "Test Product 1",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 2,
                Name = "Test Product 2",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 3,
                Name = "Test Product 3",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 4,
                Name = "Test Product 4",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 5,
                Name = "Test Product 5",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 6,
                Name = "Test Product 6",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 7,
                Name = "Test Product 7",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 8,
                Name = "Test Product 8",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 9,
                Name = "Test Product 9",
                Category = 3,
                Price = (decimal?) 10.0,
            },
            new()
            {
                Id = 10,
                Name = "Test Product 10",
                Category = 3,
                Price = (decimal?) 10.0,
            },
        };
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(mockProducts))
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.RetrieveAllProductsAsync();

        #endregion

        #region Assert

        // if result is null, the test will fail
        Assert.NotNull(result);
        // trim the result for any whitespace
        var trimmedResult = (result ?? throw new InvalidOperationException()).Select(x => x.Name?.Trim());
        // check if the result contains the expected values
        Assert.Equal(mockProducts.Select(x => x.Name?.Trim()), trimmedResult);

        #endregion

    }
    
    /// <summary>
    /// Testing a failed call to the RetrieveProductsByFilter method.
    /// </summary>
    [Fact]
    public void RetrieveProductsByFilterUnsuccessfulTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock filter to return
        var mockFilter = new ProductFilter()
        {
            NumberOfPages = 3,
            PageSize = 10,
            OrderBy = "Category",
            Ascending = false,
            Filter = null
        };
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent("Request Failed")
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.RetrieveProductsByFilter(mockFilter);

        #endregion
        
        #region Assert

        // Result should be null as the request failed
        Assert.Null(result);

        #endregion
    }
    
    /// <summary>
    /// Testing an unsuccessful call to the RetrieveProductsByFilter method where the JSON response is invalid.
    /// </summary>
    [Fact]
    public void RetrieveProductsByFilterInvalidJsonTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock filter to return
        var mockFilter = new ProductFilter()
        {
            NumberOfPages = 3,
            PageSize = 10,
            OrderBy = "Category",
            Ascending = false,
            Filter = null
        };
        // Create a mock product to return
        var mockProduct = new Product()
        {
            Id = 1,
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(mockProduct))
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveProductsByFilter method
        var result = productsApi.RetrieveProductsByFilter(mockFilter);

        #endregion

        #region Assert

        // Exception should be logged and result should be null
        // check if the exception was logged
        mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
        Assert.Null(result);

        #endregion
    }
    
    #endregion
    
    #region CreateProductAsync Tests

    /// <summary>
    /// Testing a successful call to the CreateProductAsync method.
    /// </summary>
    [Fact]
    public void CreateProductAsyncSuccessTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };
        // Create a mock product that will be used to create a new product
        var mockProduct = new Product()
        {
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.CreateProductAsync(mockProduct);

        #endregion

        #region Assert

        // if result is null, the test will fail
        Assert.NotNull(result);
        // check if the result returns true
        Assert.True(result);

        #endregion

    }
    
    /// <summary>
    /// Testing a failed call to the CreateProductAsync method.
    /// </summary>
    [Fact]
    public void CreateProductAsyncUnsuccessfulTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock product that will be used to create a new product
        var mockProduct = new Product()
        {
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the RetrieveAllProductsAsync method
        var result = productsApi.CreateProductAsync(mockProduct);

        #endregion
        
        #region Assert

        // Result should be false as the request failed
        Assert.False(result);

        #endregion
    }
    
    /// <summary>
    /// Testing a failed call to the CreateProductAsync method due to an exception.
    /// </summary>
    [Fact]
    public void CreateProductAsyncExceptionTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock product that will be used to create a new product
        var mockProduct = new Product()
        {
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return an Exception
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Throws(new Exception());
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the CreateProductAsync method
        var result = productsApi.CreateProductAsync(mockProduct);

        #endregion
        
        #region Assert

        // Result should be false as the request failed
        Assert.False(result);
        // Check if the logger was called
        mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);

        #endregion
    }

    #endregion

    #region CreateProductAsync Tests

    /// <summary>
    /// Testing a successful call to the UpdateProductAsync method.
    /// </summary>
    [Fact]
    public void UpdateProductAsyncSuccessTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock product that will be used to update a product
        var mockProduct = new Product()
        {
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the UpdateProductAsync method
        var result = productsApi.UpdateProductAsync(1, mockProduct);

        #endregion

        #region Assert

        // if result is null, the test will fail
        Assert.NotNull(result);
        // check if the result returns true
        Assert.True(result);

        #endregion

    }
    
    /// <summary>
    /// Testing a failed call to the UpdateProductAsync method.
    /// </summary>
    [Fact]
    public void UpdateProductAsyncUnsuccessfulTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock product that will be used to update a product
        var mockProduct = new Product()
        {
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the UpdateProductAsync method
        var result = productsApi.UpdateProductAsync(1, mockProduct);

        #endregion
        
        #region Assert

        // Result should be false as the request failed
        Assert.False(result);

        #endregion
    }
    
    /// <summary>
    /// Testing a failed call to the UpdateProductAsync method due to an exception.
    /// </summary>
    [Fact]
    public void UpdateProductAsyncExceptionTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock product that will be used to update a product
        var mockProduct = new Product()
        {
            Name = "Test Product",
            Category = 3,
            Price = (decimal?) 10.0,
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return an Exception
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Throws(new Exception());
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the UpdateProductAsync method
        var result = productsApi.UpdateProductAsync(1, mockProduct);

        #endregion
        
        #region Assert

        // Result should be false as the request failed
        Assert.False(result);
        // Check if the logger was called
        mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);

        #endregion
    }

    #endregion

    #region DeleteProductAsync Tests
    
    /// <summary>
    /// Testing a successful call to the DeleteProductAsync method.
    /// </summary>
    [Fact]
    public void DeleteProductAsyncSuccessTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion

        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the DeleteProductAsync method
        var result = productsApi.DeleteProductAsync(1);

        #endregion

        #region Assert

        // if result is null, the test will fail
        Assert.NotNull(result);
        // check if the result returns true
        Assert.True(result);

        #endregion

    }
    
    /// <summary>
    /// Testing a failed call to the DeleteProductAsync method.
    /// </summary>
    [Fact]
    public void DeleteProductAsyncUnsuccessfulTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        // Create a mock response message
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return a mocked HttpResponseMessage
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the DeleteProductAsync method
        var result = productsApi.DeleteProductAsync(1);

        #endregion
        
        #region Assert

        // Result should be false as the request failed
        Assert.False(result);

        #endregion
    }
    
    /// <summary>
    /// Testing a failed call to the DeleteProductAsync method due to an exception.
    /// </summary>
    [Fact]
    public void DeleteProductAsyncExceptionTest()
    {
        #region Arrange
        
        // Create a mock of the IConfiguration interface
        var mockConfiguration = new Mock<IConfiguration>();
        // Create a mock of the Logger interface
        var mockLogger = new Mock<ILogger<ProductsApi>>();
        // Setup mock uri's for the API
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
        var mockHandler = new Mock<HttpMessageHandler>();
        // Setup the HttpMessageHandler to return an Exception
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Throws(new Exception());
        // Create the HttpClient using the mocked HttpMessageHandler
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the configuration section settings to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);

        #endregion
        
        #region Act
        
        // Create a new instance of the ProductsApi class
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient, mockLogger.Object);
        // Call the DeleteProductAsync method
        var result = productsApi.DeleteProductAsync(1);

        #endregion
        
        #region Assert

        // Result should be false as the request failed
        Assert.False(result);
        // Check if the logger was called
        mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);

        #endregion
    }

    #endregion

}