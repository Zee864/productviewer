using System.Text.Json.Serialization;

namespace ProductViewer.Models;
/// <summary>
/// Represents the filers that can be applied to the products
/// </summary>
public class ProductFilter
{
    /// <summary>
    /// Gets or sets the amount of pages that must be returned.
    /// </summary>
    [JsonPropertyName("page")]
    public int NumberOfPages { get; set; }

    /// <summary>
    /// Gets or sets the amount of products that must be returned per page.
    /// </summary>
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the term for which the products must be ordered by.
    /// <remarks>Id is used as a default to order by</remarks>
    /// <value> <c>Id</c> <see cref="Product"/> </value>
    /// <value> <c>Name</c> <see cref="Product"/> </value>
    /// <value> <c>Category</c> <see cref="Product"/> </value>
    /// <value> <c>Price</c> <see cref="Product"/> </value>
    /// </summary>
    [JsonPropertyName("orderBy")] 
    public string OrderBy { get; set; } = "Id";

    /// <summary>
    /// Gets or sets the direction for which the products must be ordered by.
    /// </summary>
    /// <remarks>Boolean value representing whether the value should be ascending or not.
    /// Default value is <c>true</c> which means ascending.
    /// </remarks>
    [JsonPropertyName("ascending")] 
    public bool Ascending { get; set; } = true;

    /// <summary>
    /// Gets or sets the term for which the products must be filtered by.
    /// <value> <c>Id</c> <see cref="Product"/> </value>
    /// <value> <c>Name</c> <see cref="Product"/> </value>
    /// <value> <c>Category</c> <see cref="Product"/> </value>
    /// <value> <c>Price</c> <see cref="Product"/> </value>
    /// </summary>
    [JsonPropertyName("filter")]
    public string? Filter { get; set; }
}