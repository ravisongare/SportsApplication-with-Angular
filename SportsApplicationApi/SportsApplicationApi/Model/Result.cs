using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplicationApi.Model
{
    public class Result
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TestId { get; set; }
        
        public int Distance { get; set; }
        public String Fitness { get; set; }
    }
}
