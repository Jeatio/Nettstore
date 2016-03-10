using Nettbutikk.BLL;
using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class VareController : Controller
    {
        //
        // GET: /Vare/
        //---------------Vare metoder start---------------------------
        public ActionResult ListAlleVarer()
        {
            var vareDB = new VareLogikk();
            List<fremvisningsvarer> alleVarer = vareDB.hentAlleVarer();
            return View(alleVarer);
        }


        public ActionResult SeEnVare(int vareID)
        {
            var vareDB = new VareLogikk();
            displayVarer enVare = vareDB.seVare(vareID);
            return View(enVare);
        }

        public ActionResult SeVarerIKategori(int kat)
        {
            var vareDB = new VareLogikk();
            List<fremvisningsvarer> kategoriVare = vareDB.hentVareIKategori(kat);
            Session["AktivKat"] = kat;
            return View(kategoriVare);
        }

        public ActionResult SeVarerIUnderKategori(int underKat)
        {
            var vareDB = new VareLogikk();
            List<fremvisningsvarer> kategoriVare = vareDB.hentVareIUnderKategori(underKat);
            Session["AktivUnderKat"] = underKat;
            return View(kategoriVare);
        }
        public string Sokbar(string sokord)
        {
            var vareDB = new VareLogikk();
            string sok = vareDB.hentAlleVarer(sokord);
            return sok;
        }

        public ActionResult ListAlleSokVarer(string sok)
        {
            if (sok != null)
            {
                var vareDB = new VareLogikk();
                List<fremvisningsvarer> alleVarer = vareDB.hentSokVarer(sok);
                if (alleVarer == null || alleVarer.Count < 1)
                    ViewBag.Info = "Fant ingen varer med navn: " + sok;
                return View(alleVarer);
            }
            return RedirectToAction("Index","Nettbutikk");
        }

        //------------------Vare metoder slutt-------------------
	}
}