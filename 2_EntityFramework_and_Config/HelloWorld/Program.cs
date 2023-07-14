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
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF entityFramework = new DataContextEF(config);

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

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

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
                Computer.ComputerId,
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
                    Console.WriteLine(singleComputer.ComputerId
            + "','" + singleComputer.Motherboard 
            + "','" + singleComputer.HasWifi
            + "','" + singleComputer.HasLTE
            + "','" + singleComputer.ReleaseDate.ToString("yyyy-MM-dd")
            + "','" + singleComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            + "','" + singleComputer.VideoCard);
            }

            IEnumerable<Computer>? computerEf = entityFramework.Computer?.ToList<Computer>();

            if(computerEf != null){
            foreach(Computer singleComputer in computerEf)
            {
                    Console.WriteLine(singleComputer.ComputerId
            + "','" + singleComputer.Motherboard 
            + "','" + singleComputer.HasWifi
            + "','" + singleComputer.HasLTE
            + "','" + singleComputer.ReleaseDate.ToString("yyyy-MM-dd")
            + "','" + singleComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            + "','" + singleComputer.VideoCard);
            }
            }
            // myComputer.HasWifi = false;
            // Console.WriteLine(myComputer.Motherboard);
            // Console.WriteLine(myComputer.HasWifi);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.VideoCard);
        }

    }

