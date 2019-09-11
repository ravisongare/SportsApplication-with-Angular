using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplicationApi.Model
{
    public class Result
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string test_id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int distance { get; set; }
        public String Fitness { get; set; }
    }
}
