namespace HelloWorld;
using System.Data;
using Dapper;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Data.SqlClient;
using System.Globalization;
using Microsoft.Extensions.Configuration;

internal class Program
    {
        static void Main(string[] args)
        {


            Computer myComputer = new Computer() 
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };


            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard                
            ) VALUES ('"+ myComputer.Motherboard 
            + "','" + myComputer.HasWifi
            + "','" + myComputer.HasLTE
            + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
            + "','" + myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            + "','" + myComputer.VideoCard
            +"')";

            Console.WriteLine(sql);

            //File.WriteAllText("log.txt", "\n" + sql + "\n");

            using StreamWriter openFile = new("log.txt", append: true);

            openFile.WriteLine("\n" + sql + "\n");

            openFile.Close();

            string fileText = File.ReadAllText("log.txt");

            Console.WriteLine(fileText);
           

        }

    }

