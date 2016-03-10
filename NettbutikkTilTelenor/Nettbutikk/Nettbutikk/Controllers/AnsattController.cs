using Nettbutikk.BLL;
using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Nettbutikk.Controllers
{
    public class AnsattController : Controller
    {
        //
        // GET: /Admin/
        private IAnsattLogikk _ansattBLL;
        private IKundeLogikk _kundeBLL;
        private IVareLogikk _vareBLL;
        private IOrdreLogikk _ordreBLL;
        public AnsattController ()
        {
            _ansattBLL = new AnsattLogikk();
            _kundeBLL = new KundeLogikk();
            _vareBLL = new VareLogikk();
            _ordreBLL = new OrdreLogikk();
        }
        public AnsattController(IAnsattLogikk stub)
        {
            _ansattBLL = stub;
        }
        public AnsattController(IKundeLogikk stub)
        {
            _kundeBLL = stub;
        }
        public AnsattController(IVareLogikk stub)
        {
            _vareBLL = stub;
        }
        public AnsattController (IOrdreLogikk stub)
        {
            _ordreBLL = stub;
        }
        

        //----------------Admin metoder--------------
        public ActionResult EndreKunde (string email)
        {   
            ViewBag.epost = email;
            if (Session["ErAdmin"] != null)
            {               
                
                endreKunde enKunde = _kundeBLL.finnKundeEndre(email);
                
                return View(enKunde);
            }
            return RedirectToAction("Index", "Nettbutikk");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EndreKunde (string email,endreKunde innKunde)
        {

            if(ModelState.IsValid)
            {
                if((Nettbutikk.Model.PresentasjonsModel.ansatt)Session["ErAdmin"] != null)
                {  
                    //innKunde.passord er passordet admin skriver inn i view.
                    if (((Nettbutikk.Model.PresentasjonsModel.ansatt)Session["ErAdmin"]).passord == _kundeBLL.hashPassord(innKunde.passord))
                    {
                        bool endringOK = _kundeBLL.endreKunde(email, innKunde);
                        if (endringOK)
                             return RedirectToAction("ListKunder", "Ansatt");
                    }
                }
                             
            }
            return RedirectToAction("Index", "Nettbutikk");
        }

        public ActionResult ListKunder()
        {
            if (Session["ErAdmin"] != null)
                {
                    
                    List<minProfil> alleKunder = _kundeBLL.hentAlle();
                    return View(alleKunder);
                }
            return RedirectToAction("Index","Nettbutikk");
        }

        public ActionResult Detaljer(string email)
        {
            if(Session["ErAdmin"] != null)
            {
                
                minProfil enKunde = _kundeBLL.finnKundeProfil(email);
                return View(enKunde);
            }
            return RedirectToAction("Index", "Nettbutikk");           
        }
        public bool Slett(string email)
        {
            
            if(Session["ErAdmin"] != null)
            {
              
                bool slettOK = _kundeBLL.slettKunde(email);
                return slettOK;
            }
            return false;
        }

        public ActionResult ListKvitteringer ()
        {
            
            if(Session["ErAdmin"] != null)
            {
               
                List<visOrdreAdmin> alleKvitteringer = _ordreBLL.visAlleKvitteringer();
                return View(alleKvitteringer);
            }
            return RedirectToAction("Index", "Nettbutikk");
        }

        public ActionResult VisKvittering(string email,int kvitteringsID)
        {
            
            if (Session["ErAdmin"] != null)
            {     
               
                List<viskvittering> ordreliste = _ordreBLL.visKvittering(email, kvitteringsID);
                return View(ordreliste);
            }
            return RedirectToAction("Index","Ansatt");
        }

        public ActionResult ListVarer()
        {           
            if(Session["ErAdmin"] != null)
            {
               
                List<visVarerAdmin> vareliste = _vareBLL.listAlleVarer();
                return View(vareliste);
            }
            return RedirectToAction("Index","Nettbutikk");
        }

        public ActionResult OpprettVare()
        {
            if (Session["ErAdmin"] != null)
            {
                ViewBag.Kategorier = _vareBLL.hentAlleKategorier(); 
                return View();
            }
            return RedirectToAction("Index", "Nettbutikk");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpprettVare(vare innVare, int underkategori, int kategori)
        {
            if (ModelState.IsValid)
            {
                if(Session["ErAdmin"] != null)
                {
                    bool ok = _vareBLL.OpprettVare(innVare, underkategori, kategori);
                }
            }
            return RedirectToAction("ListVarer", "Ansatt");
        }

        public ActionResult SlettVare(int vareNr)
        {

            bool ok = _vareBLL.slettVare(vareNr); 

            return RedirectToAction("ListVarer", "Ansatt");
        }

        public ActionResult EndreVaren(int varen)
        {
            if (Session["ErAdmin"] != null)
            {

                endreVare enVare = _vareBLL.finnVareEndre(varen);
                ViewBag.Kategorier = _vareBLL.hentAlleKategorier();
                return View(enVare);
            }
            return RedirectToAction("Index", "Nettbutikk");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EndreVaren(endreVare innVare)
        {
            if (ModelState.IsValid)
            {

                bool ok = _vareBLL.oppdaterVare(innVare);    
            }
            return RedirectToAction("ListVarer", "Ansatt");
        }

        public string fjernBildeFraVare(int idVare, int idBilde)
        {

            var ok = _vareBLL.slettBildeTilVare(idVare, idBilde);
            
            return ok;
        }

        public ActionResult visAlleVareBilder()
        {

            List<bilderimappe> mapper = _vareBLL.ImagesInnhold();
            List<fremvisningsvarer> alleVarer = _vareBLL.hentAlleVarer();
            ViewBag.Varer = alleVarer;
            return View(mapper);
        }

        public string leggInnBilde(string[] bildene, int vareNr)
        {
            if (bildene == null || vareNr == 0)
                return "False";

            bool OK = _vareBLL.OpprettBildeTilVare(bildene, vareNr);
            if (OK)
            {
                return "True";
            }
            return "False";
        }

        [HttpPost]
        public ActionResult UploadBilde(HttpPostedFileBase[] file, string mappe)
        {
            if(file == null || mappe == null)
                return RedirectToAction("visAlleVareBilder");

            bool ok = _vareBLL.lastOppBilder(file, mappe);
            if(ok)
               return RedirectToAction("visAlleVareBilder");
            
            return RedirectToAction("visAlleVareBilder");
        }

        public string SlettBilde(string fil, string mappe)
        {
            if (fil == null || mappe == null)
                return "False";

            bool ok = _vareBLL.SlettBilder(fil, mappe);
            if (ok)
                return mappe;
            
            return "False";
        }

        public ActionResult ListAnsatte()
        {
            if(((Nettbutikk.Model.PresentasjonsModel.ansatt)Session["ErAdmin"]) != null && ((Nettbutikk.Model.PresentasjonsModel.ansatt)Session["ErAdmin"]).eradmin)
            {
                
                List<ansatt> alleAnsatte = _ansattBLL.listAnsatte();
                return View(alleAnsatte);

            }
            return RedirectToAction("ListKunder", "Ansatt");
        }
        public ActionResult OpprettAnsatt()
        {
            if (((Nettbutikk.Model.PresentasjonsModel.ansatt)Session["ErAdmin"]) != null && ((Nettbutikk.Model.PresentasjonsModel.ansatt)Session["ErAdmin"]).eradmin)
            {
                return View();
            }
            return RedirectToAction("ListKunder", "Ansatt");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpprettAnsatt(ansatt innAnsatt)
        {
            if(ModelState.IsValid)
            {
               
                bool ok = _ansattBLL.settInnAnsatt(innAnsatt);

                if(ok)
                {
                    return RedirectToAction("ListAnsatte", "Ansatt");
                }
            }
            return RedirectToAction("Index","Nettbutikk");
        }
        public bool SlettAnsatt(int id)
        {
            if (((Nettbutikk.Model.PresentasjonsModel.ansatt)Session["ErAdmin"]) != null && ((Nettbutikk.Model.PresentasjonsModel.ansatt)Session["ErAdmin"]).eradmin)
            {
               
                bool ok = _ansattBLL.slettAnsatt(id);
                return ok;
            }
            return false;
        }

       

        //----------------Admin metoder slutt-----------
	}
}