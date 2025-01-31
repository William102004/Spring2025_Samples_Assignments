using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class Product
    {
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

        public Product()
        {
            Name = string.Empty;
            Price = 0.0;
            Quantity = 0;
        }

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
