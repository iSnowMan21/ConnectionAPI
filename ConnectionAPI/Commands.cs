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
        public static void addFilm(ConnDB conn)
        {


            Console.WriteLine("Введите ключевое слово для поиска фильмов: ");
            string keyword = Console.ReadLine();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.omdbapi.com/");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync($"?apikey=d554bc03&s={keyword}&page=1").Result;
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
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }

            }

        }
        public static void DeleteMovie(ConnDB conn)
        {
            Console.WriteLine("Введите название фильма для удаления: ");
            string titleToDelete = Console.ReadLine();

           
            conn.Delete(titleToDelete);
            Console.WriteLine($"Фильм с названием {titleToDelete} удален");
        }
    }
   }




