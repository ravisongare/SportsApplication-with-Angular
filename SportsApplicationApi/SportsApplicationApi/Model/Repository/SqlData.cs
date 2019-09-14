using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsApplicationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public class SqlData:IData
    {
        private readonly SportsApplicationApiDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SqlData(SportsApplicationApiDbContext db, UserManager<IdentityUser> userManager, 
                         SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;

            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.roleManager = roleManager;
        }
        
        public void AddParticipant(string id)
        {
            var temp = db.Tests.Single(r => r.Id .Equals(id));
            temp.Count = temp.Count + 1;
            
        }

        public void AddResult(Result result)
        {
            db.Results.Add(result); 
        }

        public void Addtest(Test test)
        {
            db.Tests.Add(test);
        }

        public void DeleteTest(Test test)
        {
            db.Tests.Remove(test);
        }

        public List<Athlete> GetAllAthlete()
        {

            //  return db.Users.ToList();
             var t=from a in db.Users
                   select new Athlete
                   {
                       Id = a.Id,
                       Name = a.Email

                   };
            return t.ToList();
        }

        public List<Test> GetAllTestById(string id)
        {
            var test= db.Tests.Where(t => t.UserId.Equals(id)).ToList();
            foreach (var temp in test)
            {
                var count = db.Results.Where(r => r.TestId == temp.Id).Count();
                temp.Count = count;
            }
            
            return test;
            
        }

        public Athlete GetAthlete(string id)
        {
            return db.Athletes.SingleOrDefault(m=>m.Id.Equals(id));
        }
        public IQueryable<AthlteDetail> getAthleteDetail(string id)
        {

            return from t in db.Tests
                   join r in db.Results
                   on t.Id equals r.TestId
                   where r.UserId.Equals(id)
                   select new AthlteDetail
                   { Type = t.Type, Date = t.Date, ResultId = r.Id };
        }

        public List<Detail> GetDeatail(string id)
        {
             var r=from a in db.Users
                   join r1 in db.Results
                   on a.Id equals r1.UserId
                   where r1.TestId.Equals( id)
                   orderby r1.Distance descending
                   select new Detail
                   { Name = a.UserName, Distance = r1.Distance, Fitness = r1.Fitness,AthleteId=a.Id,ResultId=r1.Id };
            return r.ToList();
        }

        public Result getResult(string testId, string UserId)
        {
            return db.Results.FirstOrDefault(t => t.TestId.Equals(testId) && t.UserId.Equals(UserId));
        }

    
        public Test GetTestByid(string id)
        {
            return  db.Tests.FirstOrDefault(m=>m.Id==id);
        }

     
        public void UpdateAthlete(Athlete athlete)
        {
            db.Athletes.Update(athlete);
        }

        public IEnumerable<Result> GetResultsById(string id)
        {
            return db.Results.Where(t => t.TestId.Equals(id));
        }
        public void UpdateResult(Result result)
        {
            var query = (from r in db.Results
                         where r.Id.Equals(result.Id)
                         select r).Single();
            query.UserId = result.UserId;
            query.TestId = result.TestId;
            query.Distance = result.Distance;
            query.Fitness = result.Fitness;
            //db.Results.Update(result);
        }
        public Result getResultById(int id)
        {
            return db.Results.SingleOrDefault(m => m.Id == id);
        }

        public void DeleteResult(Result result)
        {
            db.Results.Remove(result);
        }

        public void RemoveParticipant(string id)
        {
            var temp = db.Tests.Single(r => r.Id.Equals(id));
            temp.Count = temp.Count - 1;
        }

       public string getRole(IdentityUser current)
        {
            return userManager.GetRolesAsync(current).Result.FirstOrDefault();
        }

        public async Task<IdentityUser> GetUserAsync(ClaimsPrincipal httpuser)
        {
            var user = await userManager.GetUserAsync(httpuser);
            return user;
        }
       public async Task<IdentityResult> RegisterUser(RegisterViewModel model, IdentityUser user)
        {
            return await userManager.CreateAsync(user, model.Password);
        }

        public async Task AddRole(RegisterViewModel model, IdentityUser user)
        {
   
            await userManager.AddToRoleAsync(user, model.Role);
        }

        public bool IsSignedIn(ClaimsPrincipal User)
        {

            return signInManager.IsSignedIn(User);
        }
        public async Task SignIn(IdentityUser user)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task<SignInResult> Login(LoginModel model)
        {

            return await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
        }

        public async Task Signout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> CreateRole(IdentityRole identityRole)
        {
            return await roleManager.CreateAsync(identityRole);
        }
        //public IdentityUser CurrentUser()
        //{
        //    return userManager.GetUserAsync(HttpContext.User).Result;
        //}
    }
}
