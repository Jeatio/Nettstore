using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.DAL
{
    public class AnsattRepository : DAL.IAnsattRepository
    {
        public bool settInnAnsatt(ansatt innAnsatt)
        {
            try
            {
                var db = new NettbutikkDB();
                
                var nyAnsatt = new Ansatt()
                {
                    Brukernavn = innAnsatt.brukernavn,
                    ErAdmin = false,
                    Fornavn = innAnsatt.fornavn,
                    Etternavn = innAnsatt.etternavn
                };
                var kundeDB = new KundeRepository();
                nyAnsatt.Passord = kundeDB.hashPassord(innAnsatt.passord);

                db.Ansatte.Add(nyAnsatt);
                db.SaveChanges();
                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + "/En Ansatt med brukernavn " + nyAnsatt.Brukernavn + " er blitt opprettet");

                return true;
                
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }
        public bool slettAnsatt(int id)
        {
            try
            {
                var db = new NettbutikkDB();
                var enAnsatt = db.Ansatte.Find(id);
               
                if (enAnsatt == null)
                    return false;
                var navn = enAnsatt.Brukernavn;
                db.Ansatte.Remove(enAnsatt);
                db.SaveChanges();
                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + "/En Ansatt med brukernavn " + navn + " er blitt slettet");
                return true;

            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }
        public List<ansatt> listAnsatte()
        {
            try
            {
                var db = new NettbutikkDB();
                List<ansatt> alleAnsatte = new List<ansatt>();
                foreach(Ansatt a in db.Ansatte)
                {
                    if(!a.ErAdmin)
                    {
                       var utAnsatt = new ansatt()
                         {
                             idnr = a.Id,
                            brukernavn = a.Brukernavn,
                            fornavn = a.Fornavn,
                            etternavn = a.Etternavn
                         };
                       alleAnsatte.Add(utAnsatt);
                    }
                }

                return alleAnsatte;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
    }
}
