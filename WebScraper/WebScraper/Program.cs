using System.Text.Encodings.Web;
using System.Text.Json;
using WebScraper;

string htmlContent = File.ReadAllText("..\\..\\..\\excerpt.html");

List<Product> products = ProductScraper.Scrape(htmlContent);

string productsAsJson = JsonSerializer.Serialize(products, new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
});

Console.WriteLine(productsAsJson);