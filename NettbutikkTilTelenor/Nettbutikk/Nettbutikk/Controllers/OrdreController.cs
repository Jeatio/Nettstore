using Nettbutikk.BLL;
using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class OrdreController : Controller
    {
        //
        // GET: /Ordre/
        //-----------------Ordre bestillingsmetoder start---------------
        public ActionResult SlettOrdrer(int ordrenummer)
        {
            var ordreDB = new OrdreLogikk();
            string kundeEpost = "";
            if (Session["LoggetInn"] != null)
            {
                kundeEpost = (((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"])).epostadresse;
            }
            bool ok = ordreDB.slettOrdre(ordrenummer, kundeEpost);
            if (ok)
            {
                return RedirectToAction("VisHandlevogn", new { vareid = 0 });
            }
            return RedirectToAction("Index","Nettbutikk");
        }

        public ActionResult VisHandlevogn(int vareid)
        {
            var ordreDB = new OrdreLogikk();
            string kundeEpost = "";
            if (Session["LoggetInn"] != null)
            {
                kundeEpost = (((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"])).epostadresse;
            }
            else
            {
                return RedirectToAction("Index","Nettbutikk");
            }
            if (vareid == 0)
            {

                List<visordre> ordreliste = ordreDB.visHandlevogn(kundeEpost);
                ViewBag.TotalPris = ordreDB.RegnPris(kundeEpost);
                return View(ordreliste);
            }
            else
            {
                bool OK = ordreDB.leggIHandlevogn(vareid, kundeEpost);
                if (OK)
                {
                    List<visordre> ordreliste = ordreDB.visHandlevogn(kundeEpost);
                    ViewBag.TotalPris = ordreDB.RegnPris(kundeEpost);
                    return View(ordreliste);
                }
            }
            return RedirectToAction("Index","Nettbutikk");

        }

        public ActionResult EndreAntallVarer(int ant, int ordrenummer)
        {
            var ordreDB = new OrdreLogikk();
            string kundeEpost = "";
            if (Session["LoggetInn"] != null)
            {
                kundeEpost = (((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"])).epostadresse;
            }

            if (ordreDB.endreAntallVarer(ant, ordrenummer, kundeEpost))
                return RedirectToAction("VisHandlevogn", new { vareid = 0 });
            else
                return RedirectToAction("Index","Nettbutikk");
        }

        public ActionResult HandleVarer()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult HandleVarer(betaling innInfo)
        {
            var ordreDB = new OrdreLogikk();
            if (Session["LoggetInn"] != null)
            {
                var kundeEpost = (((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"])).epostadresse;
                bool OK = ordreDB.KjøpVarer(kundeEpost);
                if (OK)
                {
                    Session["betalt"] = true;
                    return RedirectToAction("VisAlleKvitteringer");
                }
                return RedirectToAction("Index","Nettbutikk");
            }
            else
            {
                return RedirectToAction("Index","Nettbutikk");
            }
        }
        public ActionResult VisAlleKvitteringer()
        {
            var ordreDB = new OrdreLogikk();
            if (Session["betalt"] != null)
                if ((bool)Session["betalt"])
                {
                    Session["betalt"] = false;
                    ViewBag.betalt = true;
                }
            if (Session["LoggetInn"] != null)
            {
                var kundeEpost = (((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"])).epostadresse;
                List<visallekvitteringer> visKvitteringer = ordreDB.visAlleKvitteringer(kundeEpost);
                return View(visKvitteringer);
            }
            return RedirectToAction("Index","Nettbutikk");

        }

        public ActionResult VisKvittering(int kvitteringsID)
        {
            var ordreDB = new OrdreLogikk();
            if (Session["LoggetInn"] != null)
            {
                var kundeEpost = (((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"])).epostadresse;
                List<viskvittering> ordreliste = ordreDB.visKvittering(kundeEpost, kvitteringsID);
                return View(ordreliste);
            }
            return RedirectToAction("VisAlleKvitteringer");
        }
	}
}