using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;

public class CartModel : PageModel
{
    public List<Product> Products { get; set; }

    public void OnGet()
    {
        string authSecret = "Fx4jJPKTgBAHWfA5cUAtGka5On4AVwtCoZQywj1Z";
        string basePath = "https://musicrecordstore-default-rtdb.europe-west1.firebasedatabase.app/";

        IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = authSecret,
            BasePath = basePath,
        };

        client = new FireSharp.FirebaseClient(config);

        if (client != null && !string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(authSecret))
        {
            var cartItems = GetCartItemsFromSession(HttpContext.Session.GetString("cart"));
            if (cartItems.Any())
            {
                var response = client.Get("/records");
                if (response != null && !string.IsNullOrEmpty(response.Body) && response.Body != "null")
                {
                    List<Product> allProducts = JsonConvert.DeserializeObject<List<Product>>(response.Body);

                    if (allProducts != null && allProducts.Any() && cartItems != null)
                    {
                        Products = allProducts
                            .Where(p => p != null && cartItems.Contains(p.Id))
                            .ToList();
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



    private List<int> GetCartItemsFromSession(string inputString)
    {
        List<int> cartItems = new List<int>();

        if (inputString == null) { return cartItems; }

        foreach (char character in inputString)
        {
            if (char.IsDigit(character))
            {
                int number = int.Parse(character.ToString());
                cartItems.Add(number);
            }
        }

        return cartItems;
    }
}
