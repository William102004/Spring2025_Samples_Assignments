using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class Product
    {

        //variable for a product, an Id for the product, a name, a price, and a quantity. There is a private version with a public getter and setter for each variable.
        public int Id { get; set; }

        public bool InCart = false;

        public string? Name { get; set; }

        private double price;
        public double Price 
        {
            get
            {
                return price;
            }
            set
            {
                price = Math.Round(value, 2);
            }
        }
        private int quantity;
        public int Quantity 
        {
            get 
            {
                return quantity;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Quantity), "Quantity cannot be less than zero.");
           
                }
                quantity = value;
            }
        }

        //function to displat the product, the name, price, and quantity of the product. If the product is in the cart, it will display the quantity in the cart. If the product is not in the cart, it will display the quantity in stock.
        public string? Display
        {
            get
            {
                if(InCart == true)
                {
                    return $"{Id}. {Name}: ${Price} , {Quantity} in Cart.";
                }
                else
                {
                    return $"{Id}. {Name}: ${Price} , {Quantity} in Stock.";
                }
            }
        }

        //constructor for the product, setting the name to an empty string, the price to 0.0, and the quantity to 0.
        public Product()
        {
            Name = string.Empty;
            Price = 0.0;
            Quantity = 0;
        }

        //constructor that takes in a product and a quantity, setting the name, price, and quantity to the product's name, price, and quantity.
       public Product(Product product, int quantity)
       {
            Name = product.Name;
            Price = product.Price;
            Quantity = quantity;

       }

        
        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
