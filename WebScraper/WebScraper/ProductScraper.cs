using System.Text.RegularExpressions;
using System.Xml;

namespace WebScraper;
public static class ProductScraper
{
    public static List<Product> Scrape(string htmlContent)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml("<root>" + htmlContent + "</root>");

        XmlNodeList itemsNodes = doc.SelectNodes("//div[@class='item']");

        List<Product> products = new();

        foreach (XmlNode itemNode in itemsNodes)
        {
            string productName = itemNode.SelectSingleNode(".//@alt").Value;
            string productPrice = itemNode.SelectSingleNode(".//p[@class='price']/span/span").InnerText;
            string productRating = itemNode.Attributes["rating"].Value;

            var product = new Product
            {
                Name = productName,
                Price = StripCurrency(productPrice),
                Rating = NormalizeRating(rating: decimal.Parse(productRating), maxRating: 10)
            };
            products.Add(product);
        }

        return products;
    }

    private static string StripCurrency(string price) =>
         Regex.Replace(price, "[^0-9.]", "").Trim();

    private static string NormalizeRating(decimal rating, decimal maxRating)
    {
        if (rating <= 5)
        {
            return rating.ToString();
        }

        decimal normalizedRating = (rating / maxRating) * 5;

        return normalizedRating.ToString("0.#");
    }
}
