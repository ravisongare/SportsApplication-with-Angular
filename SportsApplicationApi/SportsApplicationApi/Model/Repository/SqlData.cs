using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            var temp = db.Tests.Single(r => r.id .Equals(id));
            temp.count = temp.count + 1;
            
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
                       id = a.Id,
                       name = a.Email

                   };
            return t.ToList();
        }

        public List<Test> GetAllTestById(string id)
        {
            return db.Tests.Where(t => t.User_id.Equals(id)).ToList();
            
        }

        public Athlete GetAthlete(string id)
        {
            return db.Athletes.SingleOrDefault(m=>m.id.Equals(id));
        }
        public IQueryable<AthlteDetail> getAthleteDetail(string id)
        {

            return from t in db.Tests
                   join r in db.Results
                   on t.id equals r.test_id
                   where r.user_id.Equals(id)
                   select new AthlteDetail
                   { type = t.type, Date = t.date, resultId = r.id };
        }

        public List<Detail> GetDeatail(string id)
        {
             var r=from a in db.Users
                   join r1 in db.Results
                   on a.Id equals r1.user_id
                   where r1.test_id.Equals( id)
                   orderby r1.distance descending
                   select new Detail
                   { name = a.UserName, distance = r1.distance, fitness = r1.Fitness,athleteId=a.Id,resultId=r1.id };
            return r.ToList();
        }

        public Result getResult(string testId, string UserId)
        {
            return db.Results.FirstOrDefault(t => t.test_id.Equals(testId) && t.user_id.Equals(UserId));
        }

    
        public Test GetTestByid(string id)
        {
            return db.Tests.SingleOrDefault(m=>m.id.Equals(id));
        }

     
        public void UpdateAthlete(Athlete athlete)
        {
            db.Athletes.Update(athlete);
        }

        public IEnumerable<Result> GetResultsById(string id)
        {
            return db.Results.Where(t => t.test_id.Equals(id));
        }
        public void UpdateResult(Result result)
        {
            var query = (from r in db.Results
                         where r.id.Equals(result.id)
                         select r).Single();
            query.user_id = result.user_id;
            query.test_id = result.test_id;
            query.distance = result.distance;
            query.Fitness = result.Fitness;
            //db.Results.Update(result);
        }
        public Result getResultById(int id)
        {
            return db.Results.SingleOrDefault(m => m.id == id);
        }

        public void DeleteResult(Result result)
        {
            db.Results.Remove(result);
        }

        public void RemoveParticipant(string id)
        {
            var temp = db.Tests.Single(r => r.id.Equals(id));
            temp.count = temp.count - 1;
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
