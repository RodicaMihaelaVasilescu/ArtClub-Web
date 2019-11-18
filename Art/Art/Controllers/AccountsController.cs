using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Art.Data;
using Microsoft.AspNetCore.Mvc;

namespace Art.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationDbContext db;


        ////// GET: AspNetUsers
        public ActionResult Index(ApplicationDbContext context)
        {
            db = context;
            return View(context.Users);
        }
    }
}