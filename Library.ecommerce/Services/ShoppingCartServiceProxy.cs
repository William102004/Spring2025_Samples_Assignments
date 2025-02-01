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

                return ShoppingCart.Select(s => s?.Id ?? 0).Max();
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

        public  Product AddOrUpdate(Product product, List<Product> list)
        {
            Product? newItem = product;
            newItem.InCart = true;
            var existingProduct = list.FirstOrDefault(p => p?.Name == newItem.Name);
            if(product.Id == 0)
            {
                if(existingProduct != null && product.Quantity > existingProduct.Quantity)
                {
                    Console.WriteLine("Error: Not enough in stock");
                }
                else
                {
                    var productInList = list.FirstOrDefault(p => p?.Name == newItem.Name);
                    if (productInList != null)
                    {
                        productInList.Quantity -= newItem.Quantity;
                    }
                    else
                    {
                        Console.WriteLine("Error: Product not found in the list");
                    }
                    existingProduct = list.FirstOrDefault(p => p?.Name == newItem.Name);
                    if (existingProduct != null)
                    {
                        newItem.Price = existingProduct.Price;
                    }
                    else
                    {
                        Console.WriteLine("Error: Product not found in the list");
                    }
                }
                newItem = new Product(product , product.Quantity);
                newItem.Id = LastKey + 1;
                ShoppingCart.Add(newItem);
            }
            return newItem;
        }

        public Product? Delete(int id, List<Product> list)
        {
            if(id == 0)
            {
                return null;
            }
            

            Product? shoppingCartItem = ShoppingCart.FirstOrDefault(p => p?.Id == id);
            if (shoppingCartItem == null)
            {
                Console.WriteLine("Error: Product not found in Shopping Cart");
                return null;
            }

            var existingProduct = list.FirstOrDefault(p => p?.Name == shoppingCartItem?.Name);
            
            
            if (existingProduct != null)
            {
                existingProduct.Quantity += shoppingCartItem.Quantity;
            }
            else
            {
                ProductServiceProxy.Current.AddOrUpdate(shoppingCartItem) ;
            }
            ShoppingCart.Remove(shoppingCartItem);

            return shoppingCartItem;
        }

        
        

    }
}