using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD.DAL.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Type { get; set; } 
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double CaloriesBurned { get; set; }
        public DateTime Date { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
    }

}
