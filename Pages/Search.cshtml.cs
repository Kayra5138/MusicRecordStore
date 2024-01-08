using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MusicRecordStore.Pages
{
    public class SearchModel : PageModel
    {
        private readonly string authSecret = "Fx4jJPKTgBAHWfA5cUAtGka5On4AVwtCoZQywj1Z";
        private readonly string basePath = "https://musicrecordstore-default-rtdb.europe-west1.firebasedatabase.app/";
        private IFirebaseClient client;
        public List<Product> Products { get; set; }
        public string Query { get; set; }

        public void OnGet(string query, string sortBy, string sortOrder)
        {
            Query = HttpUtility.UrlDecode(query);
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath,
            };
            client = new FireSharp.FirebaseClient(config);

            if (client != null && !string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(authSecret))
            {
                FirebaseResponse response = client.Get("/records");

                if (response.Body != null && response.Body != "null")
                {
                    if (!string.IsNullOrEmpty(response.Body))
                    {
                        Products = JsonConvert.DeserializeObject<List<Product>>(response.Body);
                        if (Products == null)
                        {
                            Products = new List<Product>();
                        }
                        if (!string.IsNullOrEmpty(query))
                        {
                            Products = Products?
                                .Where(p =>
                                    p != null &&
                                    p.Name != null && p.Artist != null &&
                                    (ReplaceTurkishCharacters(p.Name).Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                    ReplaceTurkishCharacters(p.Artist).Contains(query, StringComparison.OrdinalIgnoreCase)))
                                .ToList() ?? new List<Product>();
                        }
                        SortProducts(sortBy, sortOrder);
                    }
                    else
                    {
                        Products = new List<Product>();
                    }
                }
                else
                {
                    Products = new List<Product>();
                }
            }
        }

        private void SortProducts(string sortBy, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy) || string.IsNullOrEmpty(sortOrder))
            {
                return;
            }

            switch (sortBy.ToLower())
            {
                case "artist":
                    Products = sortOrder.ToLower() == "ascending" ?
                        Products.OrderBy(p => p.Artist).ToList() :
                        Products.OrderByDescending(p => p.Artist).ToList();
                    break;
                case "name":
                    Products = sortOrder.ToLower() == "ascending" ?
                        Products.OrderBy(p => p.Name).ToList() :
                        Products.OrderByDescending(p => p.Name).ToList();
                    break;
                case "price":
                    Products = sortOrder.ToLower() == "ascending" ?
                        Products.OrderBy(p => p.Price).ToList() :
                        Products.OrderByDescending(p => p.Price).ToList();
                    break;
                case "rating":
                    Products = sortOrder.ToLower() == "ascending" ?
                        Products.OrderBy(p => p.Rating).ToList() :
                        Products.OrderByDescending(p => p.Rating).ToList();
                    break;
                case "reviews":
                    Products = sortOrder.ToLower() == "ascending" ?
                        Products.OrderBy(p => p.Reviews).ToList() :
                        Products.OrderByDescending(p => p.Reviews).ToList();
                    break;
            }
        }

        static string ReplaceTurkishCharacters(string inputString)
        {
            Dictionary<char, char> turkishChars = new Dictionary<char, char>
        {
            {'ı', 'i'},
            {'İ', 'I'},
            {'ü', 'u'},
            {'Ü', 'U'},
            {'ö', 'o'},
            {'Ö', 'O'},
            {'ş', 's'},
            {'Ş', 'S'},
            {'ç', 'c'},
            {'Ç', 'C'},
            {'ğ', 'g'},
            {'Ğ', 'G'}
        };

            StringBuilder result = new StringBuilder();

            foreach (char c in inputString)
            {
                if (turkishChars.ContainsKey(c))
                {
                    result.Append(turkishChars[c]);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}