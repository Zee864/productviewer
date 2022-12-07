using System.Text.Json.Serialization;

namespace ProductViewerWithReact;

/// <summary>
/// Represents a product.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the product identifier.
    /// </summary>
    [JsonPropertyName("ID")]
    public int? Id { get; set; }
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    [JsonPropertyName("Name")]
    public string? Name { get; set; }
    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    [JsonPropertyName("Price")]
    public decimal? Price { get; set; }
    /// <summary>
    /// Gets or sets the product category. Integers are used to represent the category.
    /// </summary>
    [JsonPropertyName("Category")]
    public int? Category { get; set; }
}