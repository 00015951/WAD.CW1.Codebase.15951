using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD.DAL.Dtos
{
    public class CreateActivityDTO
    {
        public string ActivityName { get; set; }
        public DateTime ActivityDate { get; set; }
        public double Duration { get; set; } 
        public double CaloriesBurned { get; set; } 
        public int UserId { get; set; } 
    }
}
