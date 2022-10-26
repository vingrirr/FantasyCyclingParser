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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ConfigAdmin()
        {
            List<FantasyYearConfig> configs = Repository.FantasyYearConfigGetAll();
            return View();
        }

        public ActionResult RiderAdmin()
        {
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);
            season.UpdateRiderPoints(); 

            return View(season);
        }

        public ActionResult AssignRiderPhoto(string riderPhotoURL, string pdcRiderID, string pcs_riderURL)        
        {
            
                
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);

            Rider r = season.Riders.Where(x => x.PDC_RiderID == pdcRiderID).First();

            RiderPhoto photo = new RiderPhoto();
            photo.Name = r.Name;
            photo.Image = Parser.GetImage(riderPhotoURL);
            photo.PCS_RiderURL = pcs_riderURL;

            r.Photo = photo;

            Repository.PDCSeasonUpdate(season);

            return View(season);
        }
    }
}