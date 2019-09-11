using Microsoft.AspNetCore.Identity;
using SportsApplicationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SportsApplicationApiDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IData _data;
        public UnitOfWork(SportsApplicationApiDbContext db, UserManager<IdentityUser> userManager,
                                    SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IData data
        {
            get
            {
              if (_data == null)
                {
                    _data = new SqlData(db, userManager, signInManager, roleManager);
                }
                return _data;
            }
            set
            { }
        }

        public void commit()
        {
            db.SaveChanges();
        }
    }
}
