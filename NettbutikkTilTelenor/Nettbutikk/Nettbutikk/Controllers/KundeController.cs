using Nettbutikk.BLL;
using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class KundeController : Controller
    {
        //
        // GET: /Kunde/

        //------------------Kunde Metoder start -hfg-------------------
        public ActionResult OpprettKunde()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Opprettkunde(kunde innKunde)
        {
            if (ModelState.IsValid)
            {
                var kundeDB = new KundeLogikk();
                if (kundeDB.finnKunde(innKunde.epostadresse) != null)
                    if (innKunde.epostadresse == kundeDB.finnKunde(innKunde.epostadresse).epostadresse)
                    {
                        ModelState.AddModelError("epostadresse", "E-post er funnet fra før");
                        return RedirectToAction("Index");
                    }
                bool OK = kundeDB.settInnKunde(innKunde);
                if (OK)
                {
                    return RedirectToAction("Index","Nettbutikk");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult EndreKunde()
        {
            if (Session["LoggetInn"] != null)
            {
                string epost = ((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"]).epostadresse;
                var kundeDB = new KundeLogikk();
                endreKunde enKunde = kundeDB.finnKundeEndre(epost);
                return View(enKunde);
            }
            return RedirectToAction("Index","Nettbutikk");

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EndreKunde(endreKunde endreKunde)
        {

            if (ModelState.IsValid)
            {
                var kundeDB = new KundeLogikk();
                string epost = ((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"]).epostadresse;
                if (kundeDB.hashPassord(endreKunde.passord) != kundeDB.finnKunde(epost).passord)
                {
                    ModelState.AddModelError("passord", "Passord stemmer ikke");
                    Session["LagringOK"] = false;
                    return RedirectToAction("MinProfil");
                }
                bool endringOK = kundeDB.endreKunde(epost, endreKunde);
                if (endringOK)
                {
                    return RedirectToAction("MinProfil");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult EndreKundePassord()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EndreKundePassord(endrePassord endrePass)
        {
            var kundeDB = new KundeLogikk();
            var epost = "";
            if (Session["LoggetInn"] != null)
            {
                epost = ((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"]).epostadresse;
                if (kundeDB.endrePassord(epost, endrePass))
                {
                    return RedirectToAction("MinProfil");
                }
                else
                {
                    Session["LagringOK"] = false;
                    return RedirectToAction("MinProfil");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult MinProfil()
        {
            if (Session["LagringOK"] != null)
                if (!(bool)Session["LagringOK"])
                {
                    Session["LagringOK"] = null;
                    ViewBag.LagringOK = false;
                }
            if (Session["LoggetInn"] != null)
            {
                string epost = (((Nettbutikk.Model.PresentasjonsModel.kunde)Session["LoggetInn"])).epostadresse;
                var kundeDB = new KundeLogikk();
                minProfil enKunde = kundeDB.finnKundeProfil(epost);
                return View(enKunde);
            }
            return RedirectToAction("Index");
        }

        public bool SjekkKunde(string email)
        {
            var kundeDB = new KundeLogikk();
            if (kundeDB.finnKunde(email) == null)
                return false;
            if (email == kundeDB.finnKunde(email).epostadresse)
            {
                //E-post er finnes fra før;
                return true;
            }
            return false;
        }
        //---------------Kunde metoder slutt--------------------------
	}
}