using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductViewer.Models;

/// <summary>
/// Represents a product.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the product identifier.
    /// </summary>
    [JsonPropertyName("ID")]
    [Required]
    public int? Id { get; set; }
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    [JsonPropertyName("Name")]
    [Required]
    public string? Name { get; set; }
    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    [JsonPropertyName("Price")]
    [Required]
    public decimal? Price { get; set; }
    /// <summary>
    /// Gets or sets the product category. Integers are used to represent the category.
    /// </summary>
    [JsonPropertyName("Category")]
    [Required]
    public int? Category { get; set; }
}