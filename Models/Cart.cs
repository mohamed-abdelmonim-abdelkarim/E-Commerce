namespace E_Commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public User? Users { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
