using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsApplication.Models;
using SportsApplication.Models.Repository;
using SportsApplicationApi.Model;

namespace SportsApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        // GET: Test
        // api/admin/test/list
        [HttpGet, Authorize(AuthenticationSchemes = "Bearer"), Route("test/list")]
         public List<Test> TestList()
        {
            var currentUser = HttpContext.User;     
            var id = currentUser.Claims.FirstOrDefault(c => c.Type== "id").Value;
            return unitOfWork.data.GetAllTestById(id);
        }


        // GET: api/admin/test/5/detail
        [HttpGet, Authorize(AuthenticationSchemes = "Bearer"), Route("test/{id}/detail")]
        public async Task<ActionResult<List<TestDetailModel>>> TestDetails([FromRoute]string id)
        {
            TestDetailModel testDetail;
            var test = unitOfWork.data.GetTestByid(id);
            if (test == null)
                return NotFound();
     
           var testWithAthlete = unitOfWork.data.GetDeatail(id);
            var t = testWithAthlete.ToList();

            if (testWithAthlete == null)
            {
                return NotFound();
            }
            testDetail = new TestDetailModel { Test = test, Detail = testWithAthlete };
            return Ok(testDetail);
        }

        // GET: api/admin/test/5/edit
        [HttpGet, Authorize(AuthenticationSchemes = "Bearer"), Route("test/{id}/edit")]
        public ActionResult<EditAthleteResult> Edit(int id)
       {
            EditAthleteResult tmp = new EditAthleteResult();
            tmp.athletes = unitOfWork.data.GetAllAthlete();
            tmp.result = unitOfWork.data.getResultById(id);


            if (tmp == null)
            {
                return NotFound();
            }
            return Ok(tmp);
        }

        // Post: api/admin/test/edit
        [HttpPut, Authorize(AuthenticationSchemes = "Bearer"), Route("test/edit")]
        public IActionResult Edit([FromBody] Result result)
        {
            var tmp = unitOfWork.data.GetResultsById(result.TestId);
            foreach (var temp in tmp)
            {
                if (result.UserId == temp.UserId && result.Id != temp.Id)
                {

                    var message = "Athlete already exist";
                    EditAthleteResult tmp1 = new EditAthleteResult();
                    tmp1.athletes = unitOfWork.data.GetAllAthlete();
                    tmp1.result = result;
                    return BadRequest(message);
                }
            }
            if (ModelState.IsValid)
            {

                //var temp = data.GetAthlete(result.user_id);
                //  temp.name = Name;
                // data.UpdateAthlete(temp);
                // data.commit();
                unitOfWork.data.UpdateResult(result);
                unitOfWork.commit();
                // return RedirectToAction("Details", "Test", new { id = result.test_id });
                return Ok();
            }
            return Ok();
        }


        // get: api/admin/test/result/5/delete
        [HttpGet, Authorize(AuthenticationSchemes = "Bearer"), Route("test/result/{resultid}/delete")]
        public ActionResult<Result> Delete([FromRoute]int resultid )
        {
            var result = unitOfWork.data.getResultById(resultid);


            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // Post: api/admin/test/result/5/delete
        [HttpDelete, Authorize(AuthenticationSchemes = "Bearer"), Route("test/result/{resultid}/delete")]
        public IActionResult DeleteConfirmed([FromRoute]int resultid)
        {
            var result = unitOfWork.data.getResultById(resultid);
            unitOfWork.data.DeleteResult(result);
            unitOfWork.data.RemoveParticipant(result.TestId);
            unitOfWork.commit();
            return Ok();
            //return RedirectToAction("Details", "Test", new { id = result.test_id });
        }


        // Get: api/admin/athletes
        [HttpGet, Authorize(AuthenticationSchemes = "Bearer"), Route("athletes")]
        public ActionResult<List<Athlete>> AddAthlete()
        {

           // AddAthlete addAthlete = new AddAthlete();
           // addAthlete.result = new Result();
            //addAthlete.result.test_id = id;
            return unitOfWork.data.GetAllAthlete();
            
        }

        // Post: api/admin/athletes
        [HttpPost, Authorize(AuthenticationSchemes = "Bearer"), Route("athletes")]
        public IActionResult AddAthlete([FromBody] Result result)
        {
            // var currentUser = HttpContext.User;
            var tmp1 = unitOfWork.data.GetResultsById(result.TestId);
            foreach (var tmp in tmp1)
            {
                if (result.UserId == tmp.UserId)
                {
                  var message = "This Athlete Already Exist";
                    return BadRequest(message); 

                }
            }
            if (result.Distance <= 1000)
                result.Fitness = "Below Average";
            if (result.Distance > 1000 && result.Distance <= 2000)
                result.Fitness = "Average";
            if (result.Distance > 2000 && result.Distance <= 3500)
                result.Fitness = "Good";
            if (result.Distance > 3500)
                result.Fitness = "Very Good";

            unitOfWork.data.AddResult(result);
            unitOfWork.commit();
       //     unitOfWork.data.AddParticipant(result.TestId);
            unitOfWork.commit();
            return Ok();
            //return RedirectToAction("Details", "Test", new { id = result.test_id });
        }

        // Get: api/admin/test/5
        [HttpGet, Authorize(AuthenticationSchemes = "Bearer"), Route("test/{testid}")]
     //   [Authorize(Roles = "Admin")]
        public ActionResult<Test> Delete([FromRoute]string testid)
        {


            var test = unitOfWork.data.GetTestByid(testid);
            if (test == null)
            {
                return NotFound();
            }

            return test;
        }

        // Post: api/admin/test/5/delete
        [HttpDelete, Authorize(AuthenticationSchemes = "Bearer"), Route("test/{testid}/delete")]
        public  IActionResult DeleteConfirmed([FromRoute]string testid)
        {
            var test = unitOfWork.data.GetTestByid(testid);
            unitOfWork.data.DeleteTest(test);
            unitOfWork.commit();
            var results = unitOfWork.data.GetResultsById(testid);
            if (results != null)
                foreach (var temp in results)
                {
                    unitOfWork.data.DeleteResult(temp);

                }
            unitOfWork.commit();
            return Ok();
           // return RedirectToAction("Index");
        }

        // Post: api/admin/test/create
        //[HttpDelete, Authorize(AuthenticationSchemes = "Bearer"), Route("test/create")]
        //public IActionResult Create()
        //{

        //    Test test = new Test();
        //    test.User_id = unitOfWork.data.GetUserAsync(HttpContext.User).Result.Id;
        //    return test;
        //}

        // Post: api/admin/test/5/
        [HttpPost, Authorize(AuthenticationSchemes = "Bearer"), Route("test/create")]
        public IActionResult Create([FromBody]Test test)
        {
            var currentUser = HttpContext.User;
            var id = currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value;
            test.UserId = id;
            unitOfWork.data.Addtest(test);
            unitOfWork.commit();
            return Ok();
            //return RedirectToAction(nameof(Index));
        }

    }
}