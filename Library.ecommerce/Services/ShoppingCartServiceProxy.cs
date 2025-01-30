using Spring2025_Samples.Models;


namespace Library.eCommerce.Services
{
    public class ShoppingCartServiceProxy
    {
        private ShoppingCartServiceProxy()
        {
            ShoppingCart = new List<Product?>();
        }
        private int LastKey
        {
            get
            {
                if(!ShoppingCart.Any())
                {
                    return 0;
                }

                return ShoppingCart.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();
        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
                    }
                }

                return instance;
            }
        }
        public List<Product?> ShoppingCart { get; private set; }

        public Product AddOrUpdate(Product product)
        {
            if(product.Id == 0)
            {
                product.Id = LastKey + 1;
                ShoppingCart.Add(product);
                
                
            }

            return product;
        }

        public Product? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            Product? product = ShoppingCart.FirstOrDefault(p => p?.Id == id);
            ShoppingCart.Remove(product);

            return product;
        }

        
        

    }
}