using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonDemo
{
    public class ApiHandler
    {
        private readonly HttpClient _client;

        public ApiHandler()
        {
            _client = new HttpClient();
        }

        public async Task StartProgram()
        {
            var products = await GetProductsFromApi();

            if (AskForEnterKey())
            {
                foreach (var product in products.OrderBy(p => p.Id).Take(5))
                {
                    Console.WriteLine($"Product Id: {product.Id}");
                    Console.WriteLine($"Product title: {product.Title}");
                    Console.WriteLine($"Product price: {product.Price:C2}");
                    Console.WriteLine($"Product category: {product.Category}");
                    Console.WriteLine($"Product description: {product.Description}");
                    Console.WriteLine("------------------------------");
                }
            }
        }

        private async Task<List<Product>> GetProductsFromApi()
        {
            var response = await _client.GetStringAsync(
                "https://fakestoreapi.com/products"
            );
             return JsonConvert.DeserializeObject<List<Product>>(response);
        }


        /// <summary>
        /// Asks the user to press Enter och Esc. Keeps asking until Enter och Esc is pressed
        /// </summary>
        /// <returns>If enter is pressed, it returns true, if esc it returns false</returns>
        private static bool AskForEnterKey()
        {
            Console.WriteLine("Press <Enter> to view a list of products.");
            var input = Console.ReadKey();
            while (input.Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\n *If you want to exit, press <ESC>. Otherwise press <Enter> to view a list of products");
                input = Console.ReadKey();
                if (input.Key == ConsoleKey.Escape) return false;
            }

            return input.Key == ConsoleKey.Enter;
        }
    }
}
