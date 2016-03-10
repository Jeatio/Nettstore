using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nettbutikk.BLL;
using Nettbutikk.Model;
using Nettbutikk.Model.PresentasjonsModel;
using Nettbutikk.Model.DomeneModel;
namespace Nettbutikk.Controllers
{
    public class NettbutikkController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult MenyViser()
        {
            var vareDB = new VareLogikk();

            List<kategori> listeKat = vareDB.hentAlleKategorier();
            return View(listeKat);
        }

        //------------------Logg inn metoder start-------------------
        public ActionResult LoggInn()
        {
            return View();
        }
        public string LoggInnSjekk(string email, string passord)
        {
            Session["LoggetInn"] = null;
            Session["ErAdmin"] = null;
            var kundeDB = new KundeLogikk();
            kunde innloggingOk = kundeDB.loggInnOK(email,passord);
            if (innloggingOk != null)
            { 
                Session["LoggetInn"] = innloggingOk;
                return "True";          
            }
            ansatt erAdmin = kundeDB.erAdmin(email, passord);
            if(erAdmin != null)
            {
                Session["ErAdmin"] = erAdmin;
                return "True";
            }
            return "False verdi";  
        }
        public ActionResult LoggUt()
        {
            Session["LoggetInn"] = null;
            Session["ErAdmin"] = null;
            return RedirectToAction("Index","Nettbutikk");
        }
        //-----------------Logg ut metoder slutt--------------------

        public ActionResult FAQ()
        {
            return View();
        }

    }
}