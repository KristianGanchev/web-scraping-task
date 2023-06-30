using System.Text.Json.Serialization;

namespace WebScraper;
public class Product
{
    [JsonPropertyName("productName")]
    public string Name { get; set; }
    public string Price { get; set; }
    public string Rating { get; set; }
}