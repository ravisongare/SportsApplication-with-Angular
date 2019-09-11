using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsApplication.Models;
using SportsApplication.Models.Repository;
using SportsApplicationApi.Model;

namespace SportsApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AthleteController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        // GET: Test
        // api/athlete/test
        [HttpGet, Authorize(AuthenticationSchemes = "Bearer"), Route("test")]
        public ActionResult<AthlteDetail> Athlete()
        {
            var currentUser = HttpContext.User;
            var id = currentUser.Claims.FirstOrDefault(c => c.Type == "id").Value;

            var result = unitOfWork.data.getAthleteDetail(id).ToList();
            return Ok(result);
        }
        // GET: Test
        // api/athlete/test/detail
        [HttpGet, Authorize(AuthenticationSchemes = "Bearer" ), Route("test/{resultid}/detail")]
        public ActionResult<Result> AthleteTestDetail([FromRoute]int resultid)
        {

            var result = unitOfWork.data.getResultById(resultid);
            
            return Ok(result);
        }

    }
}