using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CSharpBeltTest.Models;
using System.Linq;

namespace CSharpBeltTest.Controllers
{
    public class ActivitiesController : Controller
    {
        private BeltContext dbContext;
        public ActivitiesController(BeltContext context)
        {
            dbContext = context;
        }
        [HttpGet]
        [Route("activity_center")]
        public IActionResult Activity_Center()
        {
            if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {   HttpContext.Session.SetString("login","False");
                return Redirect("/");
            }
            List<Activity> allActivities=dbContext.Activity.ToList();
            foreach(var a in allActivities)
            {
                if(a.date_time < DateTime.Now.Date)
                {
                    dbContext.Activity.Remove(a);
                    dbContext.SaveChanges();
                }
            }
            ViewBag.fname=HttpContext.Session.GetString("fname");
            ViewBag.allusers=dbContext.Users;
            ViewBag.allActivities=dbContext.Activity.Include(a=>a.Participate).ThenInclude(u => u.User).ToList();
            ViewBag.id=HttpContext.Session.GetInt32("id");
            return View("activity_center");
            }

        [HttpPost]
        [Route("join/{id}")]
        public IActionResult Join(Participant add)
        {
            if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {   HttpContext.Session.SetString("login","False");
                return Redirect("/");
            }
            dbContext.participate.Add(add);
            dbContext.SaveChanges();

            return RedirectToAction("Activity_Center");
        }
        [HttpPost]
        [Route("leave/{id}")]
        public IActionResult Leave(Participant remove, int id)
        {
            if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {   HttpContext.Session.SetString("login","False");
                return Redirect("/");
            }
            var leave=dbContext.participate.Where(a =>a.ActivityId==id && a.UserId==remove.UserId).FirstOrDefault();
            dbContext.participate.Remove(leave);
            dbContext.SaveChanges();

            return RedirectToAction("Activity_Center");
        }
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {   HttpContext.Session.SetString("login","False");
                return Redirect("/");
            }
            var remove=dbContext.Activity.FirstOrDefault(w =>w.ActivityId==id);
            System.Console.WriteLine("");
            dbContext.Activity.Remove(remove);
            dbContext.SaveChanges();
            return RedirectToAction("Activity_Center");
        }

        [HttpGet]
        [Route("activity/add_activity")]
        public IActionResult Add_Activity()
        {
            if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {   HttpContext.Session.SetString("login","False");
                return Redirect("/");
            }
            ViewBag.fname=HttpContext.Session.GetString("fname");
            ViewBag.id=HttpContext.Session.GetInt32("id");
            return View("add_activity");
        }
        [HttpPost]
        [Route("Activity/Add")]
        public IActionResult Add(Activity newactivity)
        {
            if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {   HttpContext.Session.SetString("login","False");
                return Redirect("/");
            }
            if(ModelState.IsValid)
            {
                dbContext.Activity.Add(newactivity);
                dbContext.SaveChanges();
                var a =dbContext.Activity.Last();
                ViewBag.id=HttpContext.Session.GetInt32("id");
                return Redirect($"/activity/{a.ActivityId}");
            }
            return View("add_activity");
        }
        [HttpGet]
        [Route("Activity/{id}")]
        public IActionResult Profile(int id)
        {
            if(HttpContext.Session.GetString("Login")==null || HttpContext.Session.GetString("Login")!="True")
            {   HttpContext.Session.SetString("login","False");
                return Redirect("/");
            }
            ViewBag.fname=HttpContext.Session.GetString("fname");
            ViewBag.id=HttpContext.Session.GetInt32("id");
            ViewBag.activity=dbContext.Activity.Include(a=>a.Participate).ThenInclude(u => u.User).FirstOrDefault(w =>w.ActivityId==id);
            int CreatorId=ViewBag.activity.CreatorID;
            ViewBag.Creator=dbContext.Users.FirstOrDefault(u =>u.UserId==CreatorId);
            ViewBag.Id=HttpContext.Session.GetInt32("id");
            return View("activity_profile");
        }
    }
}
