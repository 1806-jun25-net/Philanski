﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Philanski.Backend.WebAPI.Models;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Philanski.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
  //  [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AccountController : ControllerBase
    {
        private SignInManager<IdentityUser> _signInManager { get; }
        public IRepository Repo { get; }

        public AccountController(SignInManager<IdentityUser> signInManager, IRepository repo)
        {
            _signInManager = signInManager;
            Repo = repo;
        }

        [HttpPost("Login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        [AllowAnonymous]
        public async Task<ActionResult> Login(User input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password,
                isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return StatusCode(403); // Forbidden
            }

            return NoContent();
        }

        [HttpPost("Logout")]
        [ProducesResponseType(204)]
        [AllowAnonymous]
        public async Task<NoContentResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpPost("Register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Register(Register input,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] RoleManager<IdentityRole> roleManager, bool admin = false)
        {
            // with an [ApiController], model state is always automatically checked
            // and return 400 if any errors.

            var user = new IdentityUser(input.Username);
            var libraryEmployee = new Employee();
            var result = await userManager.CreateAsync(user, input.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            if (admin)
            {
                if (!(await roleManager.RoleExistsAsync("Admin")))
                {
                    var adminRole = new IdentityRole("Admin");
                    result = await roleManager.CreateAsync(adminRole);
                    if (!result.Succeeded)
                    {
                        return StatusCode(500, result);
                    }
                }
                result = await userManager.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded)
                {
                    return StatusCode(500, result);
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: true);

            //create employee in our database after account is added to identity
            libraryEmployee.FirstName = input.FirstName;
            libraryEmployee.LastName = input.LastName;
            libraryEmployee.Email = input.Username;
            libraryEmployee.JobTitle = input.JobTitle;
            libraryEmployee.WorksiteId = input.Worksite;
            libraryEmployee.Salary = input.Salary;
            libraryEmployee.HireDate = DateTime.Now;
            Repo.CreateEmployee(libraryEmployee);
            await Repo.Save();
            
                

            
           // return NoContent();
            return CreatedAtRoute("GetEmployee" , new { controller = "Employee", id = input.Username }, input);
        }
    }
}
