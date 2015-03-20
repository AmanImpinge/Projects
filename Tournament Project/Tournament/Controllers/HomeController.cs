using MyTournament.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tournament;

namespace MyTournament.Controllers
{

    public class HomeController : Controller
    {
        TournamentDataContext _this = new TournamentDataContext();    
        public ActionResult Index()
        {
            ViewBag.Message = "";
            ViewBag.Skills = _this.tblSkillsMasters.Where(c => c.ID != 0);
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblplayer model)
        {
          //  ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.Skills = _this.tblSkillsMasters.Where(c => c.ID != 0);
            if (_this.Tb_Emps.Any(c => c.EMPID == model.EmpID))
            {
                if (!_this.tblplayers.Any(c => c.EmpID == model.EmpID))
                {
                    //tblplayer _model = new tblplayer();
                    //_model = model;
                    _this.tblplayers.InsertOnSubmit(model);
                     _this.SubmitChanges();
                     return RedirectToAction("Thanks");
                }
                else
                {
                    ViewBag.Message = "Already Registered, Please contact to admin department.";
                }
            }
            else
            {
                ViewBag.Message = "Invalid Employee ID, Please check and submit again.";
            }
            return View();
        }


        [AllowAnonymous]
        public ActionResult Thanks()
        {
            return View();
        }



        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
