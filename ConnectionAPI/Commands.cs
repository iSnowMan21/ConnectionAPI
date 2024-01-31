using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConnectionAPI;
using MySql.Data.MySqlClient;
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
        public static void Count(ConnDB conn)
        {
            int movieCount = conn.Count();
            Console.WriteLine($"Количество фильмов в базе данных: {movieCount}");
        }

        public static void Info(ConnDB conn)
        {
            Console.WriteLine("Введите год для получения информации: ");
            string? yearMovie = Console.ReadLine();


            conn.InfoYear(yearMovie);
        }
        public static void InfoByTitle(ConnDB conn)
        {
            Console.WriteLine("Введите часть названия для поиска информации: ");
            string? partialTitle = Console.ReadLine();

            conn.InfoByTitle(partialTitle);
        }

    }
   }




