using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Web;
namespace Nettbutikk.BLL
{
    public interface IVareLogikk
    {
        endreVare finnVareEndre(int id);
        List<kategori> hentAlleKategorier();
        List<fremvisningsvarer> hentAlleVarer();
        string hentAlleVarer(string sokord);
        List<fremvisningsvarer> hentSokVarer(string sokord);
        List<fremvisningsvarer> hentVareIKategori(int katnummer);
        List<fremvisningsvarer> hentVareIUnderKategori(int Underkatnummer);
        List<bilderimappe> ImagesInnhold();
        bool lastOppBilder(HttpPostedFileBase[] file, string mappe);
        List<visVarerAdmin> listAlleVarer();
        bool oppdaterVare(endreVare innVare);
        bool OpprettBildeTilVare(string[] innbilde, int vareNr);
        bool OpprettVare(vare innVare, int underkategori, int kategori);
        displayVarer seVare(int vareID);
        bool SlettBilder(string filnavn, string mappenavn);
        string slettBildeTilVare(int vareID, int bildeID);
        bool slettVare(int id);
    }
}
