using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CSharpBeltTest.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace CSharpBeltTest.Controllers

{
    public class UsersController : Controller
    {
        private BeltContext dbContext;

        public UsersController(BeltContext context)
        {
            dbContext = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Login","False");

            return View();
        }

        [HttpPost]
        [Route ("register")]
        public IActionResult Register(User used )
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u=> u.email == used.email))
                {
                    ModelState.AddModelError("email", "Email already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                used.password = Hasher.HashPassword(used, used.password);
                dbContext.Add(used);
                HttpContext.Session.SetString("Login", "True");
                User user = dbContext.Users.FirstOrDefault(u => u.email == used.email);
                HttpContext.Session.SetInt32("id",user.UserId);
                HttpContext.Session.SetString("fname",user.fname);
                return RedirectToAction("Success");
            }
            return View("Index");
        }

        [HttpPost]
        [Route ("login")] 
        public IActionResult Login(LoginUser userSubmission)
        {
            if(ModelState.IsValid) 
                {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.email == userSubmission.Email);
                if(userInDb == null)
                    {
                        ModelState.AddModelError("Email", "Invalid Email/Password");
                        return View("Index");
                    }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.password, userSubmission.Password);
                
                if(result == 0)
                {   
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                User user = dbContext.Users.FirstOrDefault(u => u.email == userSubmission.Email);
                HttpContext.Session.SetInt32("id",user.UserId);
                HttpContext.Session.SetString("fname",user.fname);
                int id=(int)HttpContext.Session.GetInt32("id");
                HttpContext.Session.SetString("Login", "True");
                return RedirectToAction("Success");
                }
            return View("Index");
        }

        [HttpGet]
        [Route("edit")] 
        public IActionResult Edit(User user)
        {   if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {
                return RedirectToAction("Index");
            }
            
            int id=(int)HttpContext.Session.GetInt32("id");
            user.UserId=id;
            System.Console.WriteLine(user.UserId);

            return View();
        }

        [HttpPost]
        [Route("update")] 
        public IActionResult Update(User update)
        
        { 
            int id=(int)HttpContext.Session.GetInt32("id");
            System.Console.WriteLine(id);
            update.UserId=id;
            dbContext.Users.Update(update);
            dbContext.SaveChanges(); 
            
            return RedirectToAction("Success"); 
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("success")]
        public IActionResult Success(int id)
        { 
            if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {   HttpContext.Session.SetString("login","False");
                return RedirectToAction("Index");
            }
                
            return Redirect("/activity_center");
        }
    }
}
