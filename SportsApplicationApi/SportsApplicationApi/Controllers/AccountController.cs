
using System;

using System.Collections.Generic;

using System.IdentityModel.Tokens.Jwt;

using System.Linq;

using System.Security.Claims;

using System.Text;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using SportsApplication.Models;
using SportsApplication.Models.Repository;
using SportsApplicationApi.Model;

namespace SportsApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {

            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }


        // post api/login
        [HttpPost, Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
           Microsoft.AspNetCore.Identity.SignInResult result = await unitOfWork.data.Login(user);
            if(result.Succeeded)
            {
                var currentUser = user.UserName;
                var users = await userManager.FindByEmailAsync(currentUser);
                var id = users.Id;
                var rolename = unitOfWork.data.getRole(users);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                var claims = new List<Claim>{
                                          new Claim(ClaimTypes.Name,user.UserName),
                                          new Claim("Role", rolename),
                                          new Claim("id",id )
                                          };
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost,Route("register")]
        
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
              
                IdentityResult result = await unitOfWork.data.RegisterUser(model, user);
             
                if (result.Succeeded)
                {
                    await unitOfWork.data.AddRole(model, user);
                    //   await unitOfWork.data.SignIn(user);

                    return Ok(new{id=user.Id });
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

            }
            return BadRequest(ModelState);
        }










    }
}