using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.DAL
{
    public class AnsattRepositoryStub : DAL.IAnsattRepository
    {
        public List<ansatt> listAnsatte()
        {
            var ansattListe = new List<ansatt>();
            var ansatt = new ansatt()
            {
                idnr=2,
                fornavn ="Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = true,
                passord = "123"
            };
            ansattListe.Add(ansatt);
            ansattListe.Add(ansatt);
            ansattListe.Add(ansatt);
            return ansattListe;
        }
        public bool settInnAnsatt(ansatt innAnsatt)
        {
            if(innAnsatt.fornavn == "")
            {
                return false;
            }else
            {
                return true;
            }
        }
        public bool slettAnsatt(int id)
        {
            if(id == 0)
            {
                return false;
            }else
            {
                return true;
            }
        }
    }
}
