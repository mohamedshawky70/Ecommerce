using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.Models
{
    public class Products
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public double Price { get; set; }
        [MaxLength(200)]
        public string Descriptions { get; set; }
        //public byte[]? Image { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedDate { get; set; }
        //[JsonIgnore]
        public List<ProductOptions> productOptions { get; set; } = new List<ProductOptions>();
        public int ProductCategoriesId { get; set; }
        public ProductCategories productCategories { get; set; }
        [JsonIgnore]

        public List<OrderDetails> orderDetails { get; set; } = new List<OrderDetails>();


    }
}
