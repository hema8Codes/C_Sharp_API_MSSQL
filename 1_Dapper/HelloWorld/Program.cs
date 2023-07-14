namespace HelloWorld;
using System.Data;
using Dapper;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Data.SqlClient;
using System.Globalization;

internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();

            string sqlCommand = "SELECT GETDATE()";

            //dbConnection.Query<DateTime>(sqlCommand);  // it's gonna return an array of result or row
            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);  // it's gonna return a single row

            Console.WriteLine(rightNow.ToString());

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

            //int result = dapper.ExecuteSqlWithRowCount(sql);
            bool result = dapper.ExecuteSql(sql);

            //Console.WriteLine(result);

            string sqlSelect = @"
            SELECT  
                Computer.Motherboard, 
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard    
            FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            foreach(Computer singleComputer in computers)
            {
                    Console.WriteLine(myComputer.Motherboard 
            + "','" + myComputer.HasWifi
            + "','" + myComputer.HasLTE
            + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
            + "','" + myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            + "','" + myComputer.VideoCard);
            }
            // myComputer.HasWifi = false;
            // Console.WriteLine(myComputer.Motherboard);
            // Console.WriteLine(myComputer.HasWifi);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.VideoCard);
        }

    }

