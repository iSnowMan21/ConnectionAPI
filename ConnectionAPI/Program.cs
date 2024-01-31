﻿
using ConnectionAPI;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace ConnectionAPI
{
    public class Program
    {
        //clr

        public static void Main(string[] args)
        {
            ConnDB conn = new ConnDB();
            
            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Доступные команды:");
                Console.WriteLine("1. Add film");
                Console.WriteLine("2. Delete movie");
                Console.WriteLine("3. Info");
                Console.WriteLine("Введите номер команды:");
                Console.WriteLine("\n");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Commands.addFilm(conn);
                        break;
                    case "2":
                        Commands.DeleteMovie(conn);
                        break;
                    case "3":
                        Commands.Info(conn);
                        break;
                    default:
                        Console.WriteLine("Некорректная команда");
                        break;
                }
            }
            

                //request -> server -> respone -> end-user//2xx 3xx 4xx 5xx
                //get request
                //post
                //delete
                //update
            }
          

        }
    }
