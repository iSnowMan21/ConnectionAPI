using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConnectionAPI;
using Newtonsoft.Json;

namespace ConnectionAPI
{
    internal class Commands
    {
        ConnDB conn = new ConnDB();
        public static void addFilm(ConnDB conn)
        {
            Console.WriteLine("Введите название фильма: ");
            string title = Console.ReadLine();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.omdbapi.com/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
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
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }

            }
        }
    }
    }




