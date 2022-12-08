using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace ProductViewer.Test;

public class ProductsApiTest
{
    [Fact]
    public void RetrieveAllProductAsyncTest()
    {
        // Arrange
        var mockConfiguration = new Mock<IConfiguration>();
        const string mockBaseUri = "http://localhost:5000";
        const string mockProductUri = "/api/products";
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
        // create a mock response
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(mockProducts))
        };
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);
        var mockHttpClient = new HttpClient(mockHandler.Object);
        // mock the Gendac:baseURI configuration setting to return the mock url endpoint
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:baseURI").Value).Returns(mockBaseUri);
        mockConfiguration.SetupGet(x => x.GetSection("Gendac:productsURI").Value).Returns(mockProductUri);
        
        // Act
        var productsApi = new ProductsApi(mockConfiguration.Object, mockHttpClient);
        var result = productsApi.RetrieveAllProductsAsync();
        
        // Assert
        
        // if result is null, the test will fail
        Assert.NotNull(result);
        // trim the result for any whitespace
        var trimmedResult = (result ?? throw new InvalidOperationException()).Select(x => x.Name?.Trim());
        Assert.Equal(mockProducts.Select(x => x.Name?.Trim()), trimmedResult);
    }
}