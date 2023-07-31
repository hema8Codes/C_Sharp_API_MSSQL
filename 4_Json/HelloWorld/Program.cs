namespace HelloWorld;
using System.Data;
using Dapper;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Data.SqlClient;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard                
            // ) VALUES ('"+ myComputer.Motherboard 
            // + "','" + myComputer.HasWifi
            // + "','" + myComputer.HasLTE
            // + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
            // + "','" + myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
            // + "','" + myComputer.VideoCard
            // +"')";

            // Console.WriteLine(sql);

            // //File.WriteAllText("log.txt", "\n" + sql + "\n");

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine("\n" + sql + "\n");

            // openFile.Close();

            string ComputersJson = File.ReadAllText("Computers.json");

            // Console.WriteLine(ComputersJson);

            JsonSerializerOptions options = new JsonSerializerOptions()  // for System.Text.Json
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            //Deserialization
            IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(ComputersJson);

            IEnumerable<Computer>? computersSytem = JsonConvert.DeserializeObject<IEnumerable<Computer>>(ComputersJson);


            if(computersNewtonSoft != null)
            {
                foreach(Computer computer in computersNewtonSoft)
                {
                        string sql = @"INSERT INTO TutorialAppSchema.Computer (
                            Motherboard,
                            HasWifi,
                            HasLTE,
                            ReleaseDate,
                            Price,
                            VideoCard                
                        ) VALUES ('"+ EscapeSingleQoute(computer.Motherboard) 
                        + "','" + computer.HasWifi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate?.ToString("yyyy-MM-dd")
                        + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                        + "','" + EscapeSingleQoute(computer.VideoCard)
                        +"')";

                        dapper.ExecuteSql(sql);
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings()  // for Newtonsoft.Json
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // Serialization
            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            File.WriteAllText("computerCopyNewtonsoft.txt", computersCopyNewtonsoft);

            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSytem, options);

            File.WriteAllText("computersCopySystem.txt", computersCopySystem);
           

        }

        static string EscapeSingleQoute(string input)
        {
            string output = input.Replace("'","''");


            return output;

        }

    }

