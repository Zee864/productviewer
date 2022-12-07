using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductViewerWithReact.Controllers;

namespace ProductViewerWithReact.Services;

public class ProductsApi : IProductsApi
{
    #region Properties

    public IConfiguration Configuration { get; set; }
    public HttpClient HttpClient { get; set; }
    public string BaseUri { get; set; }
    public string ProductUri { get; set; }
    
    #endregion
    
    #region Constructor

    public ProductsApi(IConfiguration configuration, HttpClient httpClient)
    {
        Configuration = configuration;
        HttpClient = httpClient;
        BaseUri = Configuration.GetSection("Gendac:baseURI").Value;
        ProductUri = Configuration.GetSection("Gendac:productsURI").Value;
    }

    #endregion

    #region Methods

    #region Retrieve Product(s)

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
            var product = JsonConvert.DeserializeObject<Product>(content);
            return product;
        }
        catch (Exception e)
        {
            // log the exception using log4net and return null
            log4net.LogManager.GetLogger(typeof(ProductController)).Error(e);
            return null;
        }
    }
    
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
            // log the exception using log4net and return null
            log4net.LogManager.GetLogger(typeof(ProductController)).Error(e);
            return null;
        }
    }

    [HttpGet]
    public List<Product>? RetrieveProductsByFilter(ProductFilter productFilter)
    {
        try
        {
            // make the call to the API to retrieve all the products
            var response = HttpClient.GetAsync($"{BaseUri}{ProductUri}?page={productFilter.Page}&pageSize={productFilter.PageSize}&orderBy={productFilter.OrderBy}&ascending={productFilter.Ascending}&filter={productFilter.Filter}");
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
            // log the exception using log4net and return null
            log4net.LogManager.GetLogger(typeof(ProductController)).Error(e);
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
            // log the exception using log4net and return false
            log4net.LogManager.GetLogger(typeof(ProductController)).Error(e);
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
            // log the exception using log4net and return false
            log4net.LogManager.GetLogger(typeof(ProductController)).Error(e);
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
            // log the exception using log4net and return false
            log4net.LogManager.GetLogger(typeof(ProductController)).Error(e);
            return false;
        }
    }
    
    #endregion

    #endregion
    
}