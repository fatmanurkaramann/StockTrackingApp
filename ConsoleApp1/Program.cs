using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {

        static async Task Main(string[] args)
        {
            string apiUrl = "https://localhost:7085/api/Users/Login";

            // Kullanıcı adı ve şifre bilgilerini buraya girin
            Console.WriteLine("Kullanıcı adı giriniz");
            string username = Console.ReadLine();
            Console.WriteLine("Şifre giriniz");
            string password = Console.ReadLine();

            // Giriş yapmak için gerekli verileri oluşturun
            var loginData = new
            {
                UsernameOrEmail = username,
                Password = password
            };

            // HttpClient ile istek gönderin
            using (var httpClient = new HttpClient())
            {
                // Serialize edilmiş JSON verisini oluşturun
                var jsonData = System.Text.Json.JsonSerializer.Serialize(loginData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // POST isteğiyle giriş yapın
                var response = await httpClient.PostAsync(apiUrl, content);

                // İstek yanıtını işleyin
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginUserCommandResponse>(responseBody);
                    string accessToken = result?.Token?.AccessToken;
                    
                    Console.WriteLine("Giriş başarılı. Access Token: " + accessToken);

                    while (true)
                    {
                        Console.WriteLine("Enter your choice:");
                        Console.WriteLine("1. Ürün Ekle");
                        Console.WriteLine("2. Ürün listele");
                        Console.WriteLine("3. Ürün güncelle");

                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                await Product.Create();
                                break;
                            case 2:
                                await Product.ListProducts();
                                break;
                            case 3:
                                await Product.Update();
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }

                    }

                }
                else
                {
                    Console.WriteLine("Giriş başarısız. Status Code: " + response.StatusCode);
                }
            }
        }
      
    }
  
    public  class Product
    {
        public static async Task Update()
        {
            Api apiUrl = new Api();

            Console.WriteLine("Güncellenecek Ürünün ID'sini Girin: ");
            string productId = Console.ReadLine();


            using (var httpClient = new HttpClient())
            {

                // Kullanıcıdan ürün özelliklerini alın
                Console.WriteLine("Yeni Ürün Adı: ");
                string name = Console.ReadLine();

                Console.WriteLine("Yeni Ürün Fiyatı: ");
                float price;
                while (!float.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("Geçersiz fiyat. Tekrar girin: ");
                }

                Console.WriteLine("Yeni Ürün Stoğu: ");
                int stock = Int16.Parse(Console.ReadLine());

                // Verileri oluşturun
                var productData = new
                {
                    Id = productId,
                    Name = name,
                    Price = price,
                    Stock = stock
                };

                // Serialize edilmiş JSON verisini oluşturun
                var jsonData = System.Text.Json.JsonSerializer.Serialize(productData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // PUT isteğiyle ürünü güncelle
                var response = await httpClient.PutAsync(apiUrl.apiUrl, content);

                // İstek yanıtını işleyin
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Ürün başarıyla güncellendi.");
                }
                else
                {
                    Console.WriteLine($"Ürün güncelleme başarısız oldu. Status Code: {response.StatusCode}");
                }
            }
        }
        public static async Task Create() {

            Api apiUrl = new Api();

            using (var httpClient = new HttpClient())
            {
                
                // Kullanıcıdan ürün özelliklerini alın
                Console.WriteLine("Ürün Adı: ");
                string name = Console.ReadLine();

                Console.WriteLine("Ürün Fiyatı: ");
                float price;
                while (!float.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("Geçersiz fiyat. Tekrar girin: ");
                }

                Console.WriteLine("Ürün Stoğu: ");
                int stock = Int16.Parse(Console.ReadLine());

                var productData = new
                {
                    Name = name,
                    Price = price,
                    Stock = stock
                };

                // Serialize edilmiş JSON verisini oluşturun
                var jsonData = System.Text.Json.JsonSerializer.Serialize(productData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


                // POST isteğini gönderin
                var response = await httpClient.PostAsync(apiUrl.apiUrl, content);

                // İstek yanıtını işleyin
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Ürün başarıyla oluşturuldu.");
                }
                else
                {
                    Console.WriteLine($"Ürün oluşturma başarısız oldu. Status Code: {response.StatusCode}");
                }
            }

        }

        public static async Task ListProducts()
        {
            Api apiUrl = new Api();

            using (var httpClient = new HttpClient())
            {

                try
                {
                    var response = await httpClient.GetAsync(apiUrl.apiUrl); // ProductsController'ınızdaki endpoint URL'sini buraya girin
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    // JSON veriyi JArray olarak parse ediyoruz
                    JObject jsonData = JObject.Parse(responseBody);

                    // "products" dizisini alıyoruz
                    JArray productsArray = (JArray)jsonData["products"];

                    // Her bir ürün için "name" ve "stock" alanlarını yazdırıyoruz
                    foreach (JObject product in productsArray)
                    {
                        Console.WriteLine("Name: " + product["name"]);
                        Console.WriteLine("Stock: " + product["stock"]);
                        Console.WriteLine("------------------------");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"API isteği başarısız oldu: {ex.Message}");
                }
            }

        }
    }
    public class Api
    {
        public string apiUrl { get; set; } = "https://localhost:7085/api/Products";
    }
    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
        public string Message { get; set; }
    }
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
