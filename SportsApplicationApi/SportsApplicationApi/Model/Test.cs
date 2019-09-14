using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplicationApi.Model
{
    public class Test
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "Date")]
        // public int date { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Participants")]
        public int Count { get; set; } = 0;
        public string UserId { get; set; }
    }
}
