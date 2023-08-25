namespace Ecommerce.Models
{
    public class ProductCategories
    {
        public int Id { get; set; }
        public List<Products> Products { get; set; }
        public int CategoriesId { get; set; }
        public Categories categories { get; set; }
    }
}
