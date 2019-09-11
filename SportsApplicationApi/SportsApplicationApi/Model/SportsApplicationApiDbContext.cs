using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplicationApi.Model
{
    public class SportsApplicationApiDbContext: IdentityDbContext
    {
        public SportsApplicationApiDbContext(DbContextOptions<SportsApplicationApiDbContext> options) : base(options)
        {
        }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
