
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CreateDB
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*
                 * string path1 = Directory.GetCurrentDirectory() + @"\Scripts\CreateAndFillDB.sql";
                Console.WriteLine(path1);

                string path2 = System.Reflection.Assembly.GetEntryAssembly().Location;
                Console.WriteLine(path2);

                string path3 = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine(path3);

                string path4 = Assembly.GetEntryAssembly().Location;
                Console.WriteLine(path4);

                string path5 = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                Console.WriteLine(path5);
                */

                if (!CheckDataBaseExists())
                {
                    GenerateDatBase();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " +e.ToString());
                Console.ReadLine();
            } 
        }

        private static void GenerateDatBase()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Scripts\CreateAndFillDB.sql";
            Console.WriteLine("Looking in: "+path);
            string fileContent = File.ReadAllText(path);
            IEnumerable<string> commandStrings = Regex.Split(fileContent, @"^\s*GO\s*$",
                           RegexOptions.Multiline | RegexOptions.IgnoreCase);

            SqlCommand command = new SqlCommand();
            command.Connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");
            command.CommandType = System.Data.CommandType.Text;
            command.Connection.Open();

            foreach (var item in commandStrings)
            {
                if (item.Trim() != "")
                {
                    Console.WriteLine(item);
                    command.CommandText = item;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static bool CheckDataBaseExists()
        {
            SqlConnection Connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_shopagent;Integrated Security=True");
            try
            {
                Connection.Open();
                Connection.Close();
                Console.WriteLine("DB exists");
                return true;
            }
            catch (System.Exception)
            {
                Console.WriteLine("DB does not exist");
                return false;
            }
        }
    }
}
