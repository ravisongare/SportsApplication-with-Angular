using Microsoft.AspNetCore.Identity;
using SportsApplicationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public class AddAthlete
    {
        public Result Reesult { get; set; }
        public List<IdentityUser> Athlete { get; set; }
    }
}
