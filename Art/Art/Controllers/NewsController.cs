using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Art.Controllers
{
    public class NewsController : Controller
    {
        // GET: /News/
        public IActionResult Index()
        {
            return View();
        }
        // 

        // GET: /News/
        //public string Index()
        //{
        //    return "This is my default action...";
        //}

        // 
        // GET: /News/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
