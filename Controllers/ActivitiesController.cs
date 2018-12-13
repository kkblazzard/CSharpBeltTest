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
        // here we can "inject" our context service into the constructor
        public ActivitiesController(BeltContext context)
        {
            dbContext = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("activities")]
        public IActionResult Activities()
        {
            return View("Activities");
        }
    }
}
