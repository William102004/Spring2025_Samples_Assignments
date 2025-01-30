// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Xml.Serialization;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char choice;
            string? input = Console.ReadLine();
            choice = input[0];

            Console.WriteLine("Welcome to Amazon!");
            Console.WriteLine("Would you like to shop(S) or manage inventory(I) ?");

          /*  Console.WriteLine("What would you like to do?");
            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read all inventory items");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory item");
            Console.WriteLine("Q. Quit");
            */
            
            if( choice == 'S' || choice == 's')
            {
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("A. Add item to Shopping Cart");
                    Console.WriteLine("R. Read all inventory items");
                    Console.WriteLine("Rs. Read all items in the Shopping Cart");
                    Console.WriteLine("Us. Update a item in the Shopping Cart");
                    Console.WriteLine("Rs. Remove a item from the Shopping Cart");
                    Console.WriteLine("I. Move to managing inventory");
                    Console.WriteLine("Ch. Checkout");
             }
            if( choice == 'I' || choice == 'i')
            {
                    Console.WriteLine("C. Create new inventory item");
                    Console.WriteLine("R. Read all inventory items");
                    Console.WriteLine("U. Update an inventory item");
                    Console.WriteLine("D. Delete an inventory item");
                    Console.WriteLine("Q. Quit");
            }

            List<Product?> list = ProductServiceProxy.Current.Products;
            List<Product?> ShoppingCart = ShoppingCartServiceProxy.Current.ShoppingCart;
           
            do
            {
                input = Console.ReadLine();
                choice = input[0];
            
                switch (choice)
                {
                    case 'C':
                    case 'c':
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine(),
                            Price = double.Parse(Console.ReadLine() ?? "0.0"),
                            Quantity = int.Parse(Console.ReadLine() ?? "0")

                        });
                        break;
                    case 'R':
                    case 'r':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        //select one of the products
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProd = list.FirstOrDefault(p => p.Id == selection);

                        if(selectedProd != null)
                        {
                            selectedProd.Name = Console.ReadLine() ?? "ERROR";
                            ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                        }
                        break;
                    case 'D':
                    case 'd':
                        //select one of the products
                        //throw it away
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');

            Console.ReadLine();
        }
    }


}