using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
namespace Nettbutikk.DAL
{
    public interface IKundeRepository
    {
        bool endreKunde(string email,endreKunde innKunde);
        bool endrePassord(string email,endrePassord pass);
        ansatt erAdmin(string navn, string passord);
        kunde finnKunde(string email);
        endreKunde finnKundeEndre(string email);
        minProfil finnKundeProfil(string email);
        string hashPassord(string funktohash);
        List<minProfil> hentAlle();
        kunde loggInnOK(string email, string passord);
        bool settInnKunde(kunde innKunde);
        bool slettKunde(string email);
    }
}
