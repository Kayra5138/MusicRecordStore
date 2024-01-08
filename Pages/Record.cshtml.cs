using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;

public class RecordModel : PageModel
{
    public Product ProductDetail { get; set; }

    public void OnGet(string id)
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
            FirebaseResponse response = client.Get($"/records/{id}");

            if (response.Body != "null")
            {
                ProductDetail = JsonConvert.DeserializeObject<Product>(response.Body);
            }
        }
    }
    public IActionResult OnGetCart(string cartItems)
    {
        HttpContext.Session.SetString("cart", cartItems);
        return new JsonResult(HttpContext.Session.GetString("cart"));
    }
}