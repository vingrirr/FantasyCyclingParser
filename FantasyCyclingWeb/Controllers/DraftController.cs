using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyCyclingParser;
using FantasyCyclingWeb.Models;
using Common;


namespace FantasyCyclingWeb.Controllers
{
    public class DraftController : Controller
    {
        // GET: Draft
        public ActionResult Index()
        {
            RiderPhoto vm = Repository.RiderPhotoGetAll().First(); 
            return View(vm);
        }
    }
}