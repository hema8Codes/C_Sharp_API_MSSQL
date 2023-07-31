using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Models
{
     public class Computer
    {
        public int ComputerId { get; set; }
        public string Motherboard { get; set; }
        public int? CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; }

        public Computer()
        {
            if (VideoCard == null)
            {
                VideoCard = "";
            }
            if (Motherboard == null)
            {
                Motherboard = "";
            }
            if(CPUCores == null)
            {
                CPUCores = 0;
            }
        }
    }
}