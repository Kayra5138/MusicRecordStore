using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;



namespace MusicRecordStore.Pages
{
    public class IndexModel : PageModel
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
                FirebaseResponse response = client.Get("/records");

                if (response != null && !string.IsNullOrEmpty(response.Body) && response.Body != "null")
                {
                    List<Product> products = JsonConvert.DeserializeObject<List<Product>>(response.Body);

                    if (products != null && products.Any())
                    {
                        List<Product> nonNullProducts = products.Where(p => p != null).ToList();
                        List<Product> sortedProducts = nonNullProducts.OrderByDescending(p => p.Reviews).ToList();

                        if (sortedProducts != null && sortedProducts.Any())
                        {
                            List<Product> top3Products = sortedProducts.Take(3).ToList();
                            Products = top3Products;
                        }
                        else
                        {
                            // sortedProducts boş ise gerekli işlemleri yapabilirsiniz.
                        }
                    }
                    else
                    {
                        // Ürün listesi boş ise gerekli işlemleri yapabilirsiniz.
                    }
                }
                else
                {
                    // response.Body boş veya "null" ise gerekli işlemleri yapabilirsiniz.
                }
            }
            else
            {
                // client, basePath veya authSecret null ise gerekli işlemleri yapabilirsiniz.
            }
        }
    }
}