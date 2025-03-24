using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductListApp
{
    // Class representing a single product
    class Product
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    // Class for managing the list of products
    class ProductList
    {
        private List<Product> products = new List<Product>();

        // Add a product to the list
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        // Display products with optional search term
        public void DisplayProducts(string searchTerm = null)
        {
            if (!products.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No products in the list yet.");
                Console.ResetColor();
                return;
            }

            // Sort products by price (low to high)
            var sortedProducts = products.OrderBy(p => p.Price).ToList();

            // Print header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("Category\tProduct\t\tPrice");
            Console.ResetColor();

            decimal totalPrice = 0;

            // Print each product, applying color highlights as necessary
            foreach (var product in sortedProducts)
            {
                if (!string.IsNullOrEmpty(searchTerm) &&
                    product.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Highlight searched product in green
                }
                else if (product.Category.Equals("electronic", StringComparison.OrdinalIgnoreCase) &&
                         product.Name.Equals("mobile", StringComparison.OrdinalIgnoreCase) &&
                         product.Price == 1330)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta; // Highlight "electronic mobile 1330" in magenta
                }
                else if (product.Category.Equals("electronic", StringComparison.OrdinalIgnoreCase) &&
                         product.Name.Equals("computer", StringComparison.OrdinalIgnoreCase) &&
                         product.Price == 2700)
                {
                    Console.ForegroundColor = ConsoleColor.White; // Highlight "electronic computer 2700" in white
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Default color for other products
                }

                Console.WriteLine($"{product.Category}\t{product.Name}\t\t{product.Price:C}");
                Console.ResetColor();

                totalPrice += product.Price;
            }

            // Print total price
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Total price: {totalPrice:C}");
        }

        // Search for a specific product
        public void SearchProduct(string searchTerm)
        {
            DisplayProducts(searchTerm);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProductList productList = new ProductList();

            while (true)
            {
                Console.WriteLine("\nOptions: [P] Add product, [S] Search product, [D] Display all, [Q] Quit");
                string input = Console.ReadLine().ToLower();

                if (input == "q") break;

                try
                {
                    if (input == "p")
                    {
                        // Add product
                        Console.Write("Enter product category: ");
                        string category = Console.ReadLine();

                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter product price: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            throw new FormatException("Invalid price format. Please enter a numeric value.");
                        }

                        productList.AddProduct(new Product { Category = category, Name = name, Price = price });

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Product added successfully!");
                        Console.ResetColor();
                    }
                    else if (input == "s")
                    {
                        // Search product
                        Console.Write("Enter search term: ");
                        string searchTerm = Console.ReadLine();
                        productList.SearchProduct(searchTerm);
                    }
                    else if (input == "d")
                    {
                        // Display all products
                        productList.DisplayProducts();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Invalid option. Please enter 'P', 'S', 'D', or 'Q'.");
                        Console.ResetColor();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Thank you for using the Product List App. Goodbye!");
        }
    }
}
