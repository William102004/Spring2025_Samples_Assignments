namespace Spring2025_Samples.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? Name { get; set; }

        public string? Display
        {
            get
            {
                return $"{ShoppingCartId}. {Name}: ${Price} , {Quantity} in Cart.";
            }
        }
        public List<Product?> ShoppingCart { get; set; }
        
        public ShoppingCartItem()
        {
            ShoppingCart = new List<Product?>();
        }

      
    }
}