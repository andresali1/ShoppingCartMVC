namespace EntityLayer
{
    public class Shopping_Cart
    {
        public int CartId { get; set; }
        public Client oClient { get; set; }
        public Product oProduct { get; set; }
        public int AMOUNT { get; set; }
    }
}
