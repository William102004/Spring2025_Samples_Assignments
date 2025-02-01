// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char choiceM;
            char choiceA;

            Console.WriteLine("Welcome to Amazon!");
            Console.WriteLine("Would you like to shop(S) or manage inventory(I) ?");

            string? input = Console.ReadLine();
            if(input == null || input.Length == 0)
            {
                choiceM = 'I';
            }
            else
            choiceM= input[0];

            
            
            List<Product?> list = ProductServiceProxy.Current.Products; 
            List<Product?> ShoppingCart = ShoppingCartServiceProxy.Current.ShoppingCart; 
           
            do
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("C. Create new inventory item or Add an item to the Shopping Cart");
                Console.WriteLine("R. Read all inventory items or Read all items in the Shopping Cart");
                Console.WriteLine("U. Update an inventory item or update an item in the Shopping Cart");
                Console.WriteLine("D. Delete an inventory item or remove an item from the shopping Cart");
                Console.WriteLine("P. Checkout");
                Console.WriteLine("S. Move to Shopping from inventory management");
                Console.WriteLine("I. Move to inventory management from Shopping");
                Console.WriteLine("Q. Quit");

                input = Console.ReadLine();
                if(input == null || input.Length == 0)
                {
                    choiceA = 'Q';
                }
                else
                {
                    choiceA = input[0];
                }

                switch (choiceA)
                {
                    case 'C':
                    case 'c':
                        if(choiceM == 'S' || choiceM == 's')
                        {
                            if(list?.Count == 0)
                            {
                                Console.WriteLine("Error: No items in inventory");
                                break;
                            }
                            else
                            ShoppingCartServiceProxy.Current.AddOrUpdate(new Product
                            {
                                InCart = true,
                                Name = Console.ReadLine(),
                                Price = 0.0,
                                Quantity = int.Parse(Console.ReadLine() ?? "0"),
                            }, list);
                            
                        
                        }
                        else
                        {
                            ProductServiceProxy.Current.AddOrUpdate(new Product
                            {
                                Name = Console.ReadLine(),
                                Price = double.Parse(Console.ReadLine() ?? "0"),
                                Quantity = int.Parse(Console.ReadLine() ?? "0")

                            });
                        }
                        break;
                    case 'R':
                    case 'r':
                        if(choiceM == 'S' || choiceM == 's')
                        {
                           if(list?.Count == 0)
                           {
                               Console.WriteLine("Error: No items in inventory");
                               break;
                           }
                           else
                           {
                               ShoppingCart.ForEach(Console.WriteLine);
                           }
                        }
                        else
                        {    
                                if (list != null)
                                {
                                    list.ForEach(Console.WriteLine);
                                }
                                else
                                {
                                    Console.WriteLine("Error: No items in inventory");
                                }
                        }
                        break;
                    case 'U':
                    case 'u':
                        //select one of the products
                         int selection;

                        if(choiceM == 'S' || choiceM == 's')
                        {
                            if(list?.Count == 0)
                            {
                                Console.WriteLine("Error: No items in inventory");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Which product in Shopping Cart would you like to update?");
                                selection = int.Parse(Console.ReadLine() ?? "-1");
                                var selectedProd = ShoppingCart.FirstOrDefault(p => p?.Id == selection);

                                if(selectedProd != null)
                                {
                                    Console.WriteLine("Enter new quantity:");
                                    int newQuantity = int.Parse(Console.ReadLine() ?? "0");
                                    selectedProd.Quantity = newQuantity;
                                    //selectedProd.Name = Console.ReadLine() ?? "ERROR";
                                    if (list != null)
                                    {
                                        ShoppingCartServiceProxy.Current.AddOrUpdate(selectedProd, list);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: No items in inventory");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Which product would you like to update?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            var selectedProd = list?.FirstOrDefault(p => p?.Id == selection);

                            if(selectedProd != null)
                            {
                                Console.WriteLine("Enter new quantity:");
                                int newQuantity = int.Parse(Console.ReadLine() ?? "0");
                                selectedProd.Quantity = newQuantity;
                               // selectedProd.Name = Console.ReadLine() ?? "ERROR";
                                ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                            }
                        }
                        break;
                        
                    case 'D':
                    case 'd':
                        //select one of the products
                        //throw it away
                        if(choiceM == 'S' || choiceM == 's')
                        {
            
                            Console.WriteLine("Which product in Shopping Cart would you like to delete?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            if (list != null)
                            {
                                ShoppingCartServiceProxy.Current.Delete(selection, list);
                            }
                            else
                            {
                                Console.WriteLine("Error: No items in inventory");
                            }
                            
                        
                        }
                        else
                        {
                            Console.WriteLine("Which product would you like to delete?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            ProductServiceProxy.Current.Delete(selection);
                        }
                        break;

                    case 'P':
                    case 'p':
                        //checkout
                       if(list?.Count == 0)
                        {
                            Console.WriteLine("Error: No items in inventory");
                            break;
                        }
                        else
                        {
                            double total = 0.0;
                            ShoppingCart.ForEach(p => total += p.Price * p.Quantity);
                            
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine("   Receipt for Amazon Purchase    ");
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine("         Items in Cart             ");
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine("Product Name: Price, Quantity     ");
                            ShoppingCart.ForEach(Console.WriteLine);
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine("Subtotal: $" + total);
                            Console.WriteLine("Tax: 7%");
                            Console.WriteLine("-----------------------------------");
                            total = total + (total * 0.07);
                            total = Math.Round(total, 2);
                            Console.WriteLine($"Total: ${total}");
                            Console.WriteLine("-----------------------------------"); 
                            Console.WriteLine("Thank you for shopping with us!");
                            ShoppingCart.Clear();
                        }
                        break;
                    case 'S':
                    case 's':
                        choiceM = 'S';
                        break;
                    case 'I':
                    case 'i':
                        choiceM = 'I';
                        break;  
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choiceA != 'Q' && choiceA != 'q');

            Console.ReadLine();
        }
    }


}