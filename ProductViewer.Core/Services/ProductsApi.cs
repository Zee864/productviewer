using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductViewer.Models;

namespace ProductViewer.Services;

/// <summary>
/// Class for working with the products api service 
/// </summary>
public class ProductsApi : IProductsApi
{
    #region Properties

    public IConfiguration Configuration { get; set; }
    public HttpClient HttpClient { get; set; }
    public string BaseUri { get; set; }
    public string ProductUri { get; set; }
    public ILogger<ProductsApi> Logger { get; set; }

    #endregion
    
    #region Constructor

    /// <summary>
    /// Constructor for the ProductsApi class
    /// </summary>
    /// <param name="configuration">Dependency injection via constructor for the configuration class</param>
    /// <param name="httpClient">Dependency injection via constructor for the HttpClient class</param>
    /// <param name="logger">Dependency injection via constructor for the logger for the ProductsApi class</param>
    public ProductsApi(IConfiguration configuration, HttpClient httpClient, ILogger<ProductsApi> logger)
    {
        Configuration = configuration;
        HttpClient = httpClient;
        BaseUri = Configuration.GetSection("Gendac:baseURI").Value;
        ProductUri = Configuration.GetSection("Gendac:productsURI").Value;
        Logger = logger;
    }

    #endregion

    #region Methods

    #region Retrieve Product(s)
    
    /// <summary>
    /// Method to retrieve a single product from the product api
    /// </summary>
    /// <param name="id">The id of the product to be retrieved</param>
    /// <returns>Returns the product or null if the product cannot be retrieved</returns>
    [HttpGet]
    public Product? RetrieveProductAsync(int id)
    {
        try
        {
            // make the call to the API to retrieve all the products
            var response = HttpClient.GetAsync($"{BaseUri}{ProductUri}/{id}");
            // check if response is successful
            if (!response.Result.IsSuccessStatusCode) return null;
            // get the response content
            var content = response.Result.Content.ReadAsStringAsync().Result;
            // content is a string containing a single json object
            // deserialize the json object into a ProductModel object
            return JsonConvert.DeserializeObject<Product>(content);
        }
        catch (Exception e)
        {
            // log the exception and return null
            Logger.Log(LogLevel.Error, e.Message);
            return null;
        }
    }
    
    /// <summary>
    /// Method to retrieve all the products from the product api
    /// </summary>
    /// <returns>The list of products or null if the products cannot be retrieved</returns>
    [HttpGet]
    public List<Product>? RetrieveAllProductsAsync()
    {
        try
        {
            // make the call to the API to retrieve all the products
            var response = HttpClient.GetAsync($"{BaseUri}{ProductUri}");
            // check if response is successful
            if (!response.Result.IsSuccessStatusCode) return null;
            // get the response content
            var content = response.Result.Content.ReadAsStringAsync().Result;
            // content is a string containing an array of json objects
            // deserialize the json objects into ProductModel objects
            var products = JsonConvert.DeserializeObject<List<Product>>(content);
            return products;
        }
        catch (Exception e)
        {
            // log the exception and return null
            Logger.Log(LogLevel.Error, e.Message);
            return null;
        }
    }
    
    /// <summary>
    /// Method to retrieve all the products from the product api using filtering and sorting conditions
    /// </summary>
    /// <param name="productFilter">Conditions for filtering and sorting <see cref="ProductFilter"/></param>
    /// <returns>The list of products from the filtering conditions or null if the products cannot be retrieved</returns>
    [HttpGet]
    public List<Product>? RetrieveProductsByFilter(ProductFilter productFilter)
    {
        try
        {
            // make the call to the API to retrieve all the products
            var response = HttpClient.GetAsync($"{BaseUri}{ProductUri}?page={productFilter.NumberOfPages}&pageSize={productFilter.PageSize}&orderBy={productFilter.OrderBy}&ascending={productFilter.Ascending}&filter={productFilter.Filter}");
            // check if response is successful
            if (!response.Result.IsSuccessStatusCode) return null;
            // get the response content
            var content = response.Result.Content.ReadAsStringAsync().Result;
            // content is a string containing an array of json objects
            // deserialize the json objects into ProductModel objects
            return JsonConvert.DeserializeObject<List<Product>>(content);
        }
        catch (Exception e)
        {
            // log the exception and return null
            Logger.Log(LogLevel.Error, e.Message);
            return null;
        }
    }

    #endregion
    
    #region Create Product
    
    /// <summary>
    /// This method creates a new product in the database
    /// </summary>
    /// <param name="product">Contains details of the product to be created <seealso cref="Product"/> </param>
    /// <returns>Truthy value indicating whether the create operation was successful or not</returns>
    [HttpPost]
    public bool CreateProductAsync(Product product)
    {
        try
        {
            // serialize the product object into a json string
            var json = JsonConvert.SerializeObject(product);
            // create a string content object from the json string
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // make the call to the API to create the product
            var response = HttpClient.PostAsync($"{BaseUri}{ProductUri}", content);
            // check if response is successful
            return response.Result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            // log the exception and return false
            Logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }
    
    #endregion
    
    #region Update Product
    
    /// <summary>
    /// This method updates a product in the database
    /// </summary>
    /// <param name="id">The id of the product that needs to be updated</param>
    /// <param name="product">Contains details of the product to be updated <seealso cref="Product"/> </param>
    /// <returns>Truthy value indicating whether the update was successful or not</returns>
    [HttpPut]
    public bool UpdateProductAsync(int id, Product product)
    {
        try
        {
            // serialize the product object into a json string
            var json = JsonConvert.SerializeObject(product);
            // create a string content object from the json string
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // make the call to the API to update the product
            var response = HttpClient.PutAsync($"{BaseUri}{ProductUri}/{id}", content);
            // check if response is successful
            return response.Result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            // log the exception and return false
            Logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }
    
    #endregion
    
    #region Delete Product
    
    /// <summary>
    /// This method deletes a product from the database
    /// </summary>
    /// <param name="id">The id of the product to be deleted</param>
    /// <returns>Truthy value indicating whether the deletion was successful or not</returns>
    [HttpDelete]
    public bool DeleteProductAsync(int id)
    {
        try
        {
            // make the call to the API to delete the product
            var response = HttpClient.DeleteAsync($"{BaseUri}{ProductUri}/{id}");
            // check if response is successful
            return response.Result.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            // log the exception and return false
            Logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }
    
    #endregion

    #endregion
    
}