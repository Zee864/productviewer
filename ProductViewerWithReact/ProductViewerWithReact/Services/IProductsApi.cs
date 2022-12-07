namespace ProductViewerWithReact.Services;

public interface IProductsApi
{
    public IConfiguration Configuration { get; set; }
    public HttpClient HttpClient { get; set; }
    public string BaseUri { get; set; }
    public string ProductUri { get; set; }
    public Product? RetrieveProductAsync(int id);
    public List<Product>? RetrieveAllProductsAsync();
    public List<Product>? RetrieveProductsByFilter(ProductFilter productFilter);
    public bool CreateProductAsync(Product product);
    public bool UpdateProductAsync(int id, Product product);
    public bool DeleteProductAsync(int id);

}