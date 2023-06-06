namespace EntityLayer
{
    public class Product
    {
        public int ProductId { get; set; }
        public string P_name { get; set; }
        public string P_description { get; set; }
        public Brand oBrand { get; set; }
        public Category oCategory { get; set; }
        public decimal Price { get; set; }
        public string PriceStr { get; set; }
        public int Stock { get; set; }
        public string ImageRoute { get; set; }
        public string ImageName { get; set; }
        public bool Active { get; set; }
        public string Base64 { get; set; }
        public string Extension { get; set; }
    }
}
