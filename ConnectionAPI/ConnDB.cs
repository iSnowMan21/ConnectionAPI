using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace ConnectionAPI
{
    internal class ConnDB
    {
        private MySqlConnection? connection;
        private string? server;
        private string? database;
        private string? uid;
        private string? password;

        //Constructor
        public ConnDB()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "imdb";
            uid = "root";
            password = "WBZc4Apkqm04NAHZC0d6";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
       /* public void Insert(Answer ans)
        {
            string query = "INSERT INTO film_info (IMDB_ID, Type, Title, Year, Poster) VALUES('1', 'movie', 'Lost', 2000, 'Lost in Island')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
*/
        internal void Insert(Answer ans)
        {

            //open connection
            if (this.OpenConnection() == true)
            {
                foreach (var movie in ans.search)
                {
                    string query = $"INSERT INTO film_info (IMDB_ID, Type, Title, Year, Poster) VALUES('{movie.imdbID}', '{movie.Type}', \"{movie.Title}\", '{movie.year}', '{movie.Poster}');";
                    MySqlCommand cmd = new (query, connection);
                    
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(movie.Poster);

                }
                this.CloseConnection();

            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE film_info SET IMDB_ID = 3,Type = 'serios', Title = 'fwfw', Year = 2004, Poster = 'wdfwjudhqw' WHERE Title='Lost'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string titleToDelete)
        {
            
                string query = $"DELETE FROM film_info WHERE Title= '{titleToDelete}'";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                
            }
        }

        //Select statement
        /*public List<string>[] Select()
        {
        }*/

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM film_info";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

      
    }
    
}
