
using ConnectionAPI;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace ConnectionAPI
{
    public class Program
    {


        public static void Main(string[] args)
        {
            ConnDB conn = new ConnDB();
            
            while (false)
            {
                Console.WriteLine("Доступные команды:");
                Console.WriteLine("1. Add film");
                Console.WriteLine("2. Exit");
                Console.WriteLine("Введите номер команды:");
                string usInput = Console.ReadLine();
            }
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.omdbapi.com/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            // Get data response
            var response = client.GetAsync("?apikey=d554bc03&s=lord&page=2").Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                Answer? ans = JsonConvert.DeserializeObject<Answer>(dataObjects);
                //Console.WriteLine(dataObjects);
                if (ans != null)
                {
                    //Console.WriteLine(ans.search[0].Poster);
                    conn.Insert(ans);
                }
                if (ans != null)
                {
                    foreach (var item in ans.search)
                    {
                        Console.WriteLine(item.Title + "\t" + item.year);
                    }
                }

                //request -> server -> respone -> end-user//2xx 3xx 4xx 5xx
                //get request
                //post
                //delete
                //update
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                              response.ReasonPhrase);
            }

        }
    }
}