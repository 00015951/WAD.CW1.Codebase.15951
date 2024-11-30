using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD.DAL.Models
{
    internal class Activity
    {
        public int Id { get; set; }
        public string Type { get; set; } 
        public double Distance { get; set; }
        public TimeSpan Duration { get; set; } 
        public DateTime Date { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
    }

}
