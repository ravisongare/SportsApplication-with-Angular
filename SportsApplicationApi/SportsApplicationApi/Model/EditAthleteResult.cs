using Microsoft.AspNetCore.Identity;
using SportsApplicationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public class EditAthleteResult
    {
        public List<Athlete> athletes { get; set; }
        public Result result { get; set; }
        public string Name { get; set; }
    }
}
