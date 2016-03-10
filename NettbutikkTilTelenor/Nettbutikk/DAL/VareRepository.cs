using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Nettbutikk.Model.DomeneModel;
using Nettbutikk.Model.PresentasjonsModel;
using System.IO;
namespace Nettbutikk.DAL
{
    public class VareRepository : DAL.IVareRepository
    {
        public bool OpprettVare(vare innVare, int underkategori, int kategori)
        {
           try
            {   
               var nyVare = new Vare()
                  {
                       Betegnelse = innVare.betegnelse,
                       Antall = innVare.antall,
                       Pris = innVare.pris,
                       Detaljer = innVare.detaljer,
                       FremVisningsDetaljer = innVare.fremvisningsdetaljer,
                       Slettet= false
                   };
           
               var db = new NettbutikkDB();

               UnderKategori eksistUnderKat = db.UnderKategorier.Find(underkategori);
               if (eksistUnderKat == null)
               {
                   eksistUnderKat = new UnderKategori()
                   {
                       Navn = innVare.underkatnavn
                   };
                   nyVare.UnderKategori = eksistUnderKat;
                   db.UnderKategorier.Add(eksistUnderKat);
               }
               else 
               {
                   nyVare.UnderKategori = eksistUnderKat;
               }
               

                Kategori eksiKat = db.Kategorier.Find(kategori);
                if (eksiKat == null)
                {
                    eksiKat = new Kategori()
                    {
                        Navn = innVare.katnavn
                    };
                    eksistUnderKat.Kategori = eksiKat;
                    db.Kategorier.Add(eksiKat);
                }
                else
                {
                    eksistUnderKat.Kategori = eksiKat;
                }

                db.Varer.Add(nyVare);
                db.SaveChanges();
                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + "/En Vare med betegnelse " + nyVare.Betegnelse + " er blitt opprettet");

                return true;
            }
            catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }
        public bool slettVare (int id)
        {
            try
            {
                var db = new NettbutikkDB();
                var vare = db.Varer.Find(id);
                vare.Slettet = true;
                db.SaveChanges();
                oppdaterKat();
                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + "/En Vare med betegnelse " + vare.Betegnelse + " er blitt slettet");

                return true;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }

        public bool oppdaterVare(endreVare innVare)
        {
            try
            {
                var db = new NettbutikkDB();
                Vare endreEnVare = db.Varer.Find(innVare.idnr);
                if (endreEnVare == null)
                    return false;

                endreEnVare.Betegnelse = innVare.betegnelse;
                if (innVare.antall < 0)
                    endreEnVare.Antall = 0;
                
                endreEnVare.Antall = innVare.antall;
                endreEnVare.Pris = innVare.pris;
                endreEnVare.Detaljer = innVare.detaljer;
                endreEnVare.FremVisningsDetaljer = innVare.fremvisningsdetaljer;

                if (innVare.underkatid != endreEnVare.UnderKategori.Id || innVare.katid != endreEnVare.UnderKategori.Kategori.KatNr)
                {
                    if (endreEnVare.Slettet)
                        endreEnVare.Slettet = false;

                    UnderKategori eksistUnderKat = db.UnderKategorier.Find(innVare.underkatid);
                    if (eksistUnderKat == null)
                    {
                        eksistUnderKat = new UnderKategori()
                        {
                            Navn = innVare.underkatnavn
                        };
                        endreEnVare.UnderKategori = eksistUnderKat;
                        db.UnderKategorier.Add(eksistUnderKat);
                    }
                    else
                    {
                        endreEnVare.UnderKategori = eksistUnderKat;
                    }
                
                    Kategori eksiKat = db.Kategorier.Find(innVare.katid);
                    if (eksiKat == null)
                    {
                        eksiKat = new Kategori()
                        {
                            Navn = innVare.katnavn
                        };
                        eksistUnderKat.Kategori = eksiKat;
                        db.Kategorier.Add(eksiKat);
                    }
                    else
                    {
                        eksistUnderKat.Kategori = eksiKat;
                    }
                }
            
                db.SaveChanges();
                string melding = " /Vare med betegnelse " + endreEnVare.Betegnelse + " har endret: ";
                if (endreEnVare.Betegnelse != innVare.betegnelse)
                    melding += "betegnelse, ";
                if (endreEnVare.Antall != innVare.antall)
                    melding += "antall, ";
                if (endreEnVare.Detaljer != innVare.detaljer)
                    melding += "detaler, ";
                if (endreEnVare.FremVisningsDetaljer != innVare.fremvisningsdetaljer)
                    melding += "fremvisningsdetaljer, ";
                if (endreEnVare.Pris != innVare.pris)
                    melding += "pris, ";
                if (endreEnVare.UnderKategori.Navn != innVare.underkatnavn)
                    melding += "underkategori, ";
                if (endreEnVare.UnderKategori.Kategori.Navn != innVare.katnavn)
                    melding += "kategori, ";

                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + melding);

                return true;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }
        public List<visVarerAdmin> listAlleVarer()
        {
            try
            {
                var db = new NettbutikkDB();
                List<Vare> alleVarene = (from k in db.Varer select k).ToList();
                List<visVarerAdmin> liste = new List<visVarerAdmin>();
                foreach (var vare in alleVarene)
                {

                    var varene = new visVarerAdmin()
                    {
                        idnr = vare.VNr,
                        betegnelse = vare.Betegnelse,
                        pris = vare.Pris,
                        detaljer = vare.Detaljer,
                        katnavn = vare.UnderKategori.Kategori.Navn,
                        underkatnavn = vare.UnderKategori.Navn,
                        antall = vare.Antall,
                        fremvisningsdetaljer = vare.FremVisningsDetaljer,
                        slettet = vare.Slettet
                    };

                    if (vare.Bilde.Count != 0)
                        varene.visvarebilde = vare.Bilde.First().src;

                    liste.Add(varene);
                    
                }
                return liste;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
        public List<fremvisningsvarer> hentAlleVarer()
        {
            try
            {
                var db = new NettbutikkDB();
                List<Vare> alleVarene = (from k in db.Varer select k).ToList();
                List<fremvisningsvarer> liste = new List<fremvisningsvarer>();
                foreach(var vare in alleVarene  )
                {
                    if(!vare.Slettet)
                    {    
                    var varene = new fremvisningsvarer()
                    {
                        idnr = vare.VNr,
                        betegnelse = vare.Betegnelse,
                        pris = vare.Pris,
                        antall = vare.Antall,
                        fremvisningsdetaljer = vare.FremVisningsDetaljer
                    };
                   
                    if (vare.Bilde.Count != 0)
                        varene.visvarebilde = vare.Bilde.First().src;
                    
                    liste.Add(varene);
                }
                }
                return liste;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
        public string hentAlleVarer(string sokord)
        {
            try
            {
            var db = new NettbutikkDB();
            var varer = (from v in db.Varer select v).ToList();
            string utView = "";
            if (sokord != null)
            {
                foreach (var vare in varer)
                {
                    if (!vare.Slettet)
                    {
                        if (Regex.IsMatch(vare.Betegnelse.ToUpper(), sokord.ToUpper() + ".*"))
                        {
                            utView += "<option value=\"" + vare.Betegnelse + "\" />;";
                        }
                    }
                }
            }
            return utView;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return sokord;
            }
        }

        public List<fremvisningsvarer> hentSokVarer(string sokord)
        {
            try
            {
                var db = new NettbutikkDB();
                List<Vare> alleVarene = (from k in db.Varer select k).ToList();
                List<fremvisningsvarer> liste = new List<fremvisningsvarer>();
                foreach (var vare in alleVarene)
                {
                    if (Regex.IsMatch(vare.Betegnelse.ToUpper(), sokord.ToUpper() + ".*"))
                    {
                        if(!vare.Slettet)
                        { 
                        var varene = new fremvisningsvarer()
                        {
                            idnr = vare.VNr,
                            betegnelse = vare.Betegnelse,
                            pris = vare.Pris,
                            antall = vare.Antall,
                            fremvisningsdetaljer = vare.FremVisningsDetaljer
                        };

                        if (vare.Bilde.Count != 0)
                            varene.visvarebilde = vare.Bilde.First().src;

                        liste.Add(varene);
                    }
                 }
                 }
                   return liste;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
         }

        public List<kategori> hentAlleKategorier()
        {
            try
            {
                var db = new NettbutikkDB();
                List<Kategori> alleKat = (from k in db.Kategorier select k).ToList(); 
                var alleKatTilView = new List<kategori>();
               
                foreach(var kat in alleKat)
                {
                    if (kat.Navn != "Slettet")
                    {
                        var enKat = new kategori();
                        List<underKategori> underkategoriN = new List<underKategori>();
                        List<visVareInfo> visVareI = new List<visVareInfo>();
                        enKat.kategorinavn = kat.Navn;
                        enKat.kategorinr = kat.KatNr;
                        foreach (UnderKategori underK in kat.UnderKategori)
                        {
                            if (underK.Navn != null)
                            {
                                var un = new underKategori();
                                un.id = underK.Id;
                                un.navn = underK.Navn;
                                foreach (var vare in underK.Vare)
                                {
                                    if (!vare.Slettet)
                                    {
                                        var visV = new visVareInfo()
                                        {
                                            underkatnavn = vare.UnderKategori.Navn,
                                            underkategori = vare.UnderKategori.Id,
                                            betegnelse = vare.Betegnelse,
                                            antall = vare.Antall,
                                            detaljer = vare.Detaljer,
                                            fremvisningsdetaljer = vare.FremVisningsDetaljer,
                                            pris = vare.Pris
                                        };
                                        visVareI.Add(visV);
                                    }
                                }
                                un.varer = visVareI;
                                underkategoriN.Add(un);

                            }
                        }
                        enKat.underkat = underkategoriN;
                        alleKatTilView.Add(enKat);
                    }
                }
                return alleKatTilView;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }

        public endreVare finnVareEndre(int id)
        {
            try
            {
                var db = new NettbutikkDB();
                var enVare = db.Varer.Find(id);
                List<visBilde> vb = new List<visBilde>();
                if (enVare == null)
                {
                    return null;
                }
                else
                {
                    var utVare = new endreVare()
                    {
                         betegnelse = enVare.Betegnelse,
                         antall = enVare.Antall,
                         fremvisningsdetaljer = enVare.FremVisningsDetaljer,
                         detaljer = enVare.Detaljer,
                         katnavn = enVare.UnderKategori.Kategori.Navn,
                         katid = enVare.UnderKategori.Kategori.KatNr,
                         underkatnavn = enVare.UnderKategori.Navn,
                         underkatid = enVare.UnderKategori.Id,
                         pris = enVare.Pris,
                         idnr = enVare.VNr
                    };

                    foreach (var b in enVare.Bilde)
                    {
                        var nyB = new visBilde();
                        nyB.id = b.Id;
                        nyB.src = b.src;
                        vb.Add(nyB);
                    }
                    utVare.bilde = vb;
                    return utVare;
                }
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }

        public string slettBildeTilVare(int vareID, int bildeID)
        {
            try
            {
                var db = new NettbutikkDB();
                var enVare = db.Varer.Find(vareID);
                if(enVare == null)
                {
                    return "False";
                }
                foreach(var b in enVare.Bilde)
                {
                    if(b.Id == bildeID)
                    {
                        enVare.Bilde.Remove(b);
                        db.SaveChanges();
                        string melding = " /Bilde: " + b.src + " har blitt fjernet fra vare med id: " + vareID;
                        SkrivTilFil.skrivEndringerTilFil(DateTime.Now + melding);
                        return "True";
                    }
                }

                return "False";
            }
            catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return "False";
            }
        }

        public displayVarer seVare(int vareID)
        {
            try
            {
                var db = new NettbutikkDB();
                var enVare = db.Varer.Find(vareID);
                List<visBilde> vb = new List<visBilde>();

                if(enVare == null)
                {
                    return null;
                }
                
                var varen = new displayVarer()
                {
                    idnr=enVare.VNr,
                    betegnelse = enVare.Betegnelse,
                    antall = enVare.Antall,
                    pris = enVare.Pris,
                    detaljer = enVare.Detaljer
                };
                foreach(var b in enVare.Bilde)
                {
                    var nyB = new visBilde();
                    nyB.id = b.Id;
                    nyB.src = b.src;
                    vb.Add(nyB);
                }
                varen.bilde = vb;
                return varen; 
            
            }
            catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }

        public List<fremvisningsvarer> hentVareIKategori(int katnummer)
        {
            try
            {
                var db = new NettbutikkDB();
                Kategori kat = db.Kategorier.Find(katnummer);
                List<fremvisningsvarer> vareIKategori = new List<fremvisningsvarer>();
                if(kat == null)
                    return null;
                foreach(UnderKategori underKat in kat.UnderKategori)
                {
                    foreach(var underKatVare in underKat.Vare)
                    {
                        if (!underKatVare.Slettet)
                        {
                        fremvisningsvarer varer = new fremvisningsvarer()
                        {
                            idnr = underKatVare.VNr,
                            betegnelse = underKatVare.Betegnelse,
                            pris = underKatVare.Pris,
                            antall = underKatVare.Antall,
                            fremvisningsdetaljer = underKatVare.FremVisningsDetaljer
                        };
                        if (underKatVare.Bilde.Count != 0)
                            varer.visvarebilde = underKatVare.Bilde.First().src;
                        
                        vareIKategori.Add(varer);
                    }
                }
                }
                
                return vareIKategori;

            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }

        public List<fremvisningsvarer> hentVareIUnderKategori(int Underkatnummer)
        {
            try
            {
                var db = new NettbutikkDB();
                UnderKategori kat = db.UnderKategorier.Find(Underkatnummer);
                List<fremvisningsvarer> vareIUnderKat = new List<fremvisningsvarer>();
                if (kat == null)
                    return null;

                    foreach (var underKatVare in kat.Vare)
                    {
                        if(!underKatVare.Slettet)
                        {
                        var varer = new fremvisningsvarer()
                        {
                            idnr = underKatVare.VNr,
                            betegnelse = underKatVare.Betegnelse,
                            pris = underKatVare.Pris,
                            antall = underKatVare.Antall,
                            fremvisningsdetaljer = underKatVare.FremVisningsDetaljer,
                        };
                        if (underKatVare.Bilde.Count != 0)
                            varer.visvarebilde = underKatVare.Bilde.First().src;

                        vareIUnderKat.Add(varer);
                    }
                    }
                    return vareIUnderKat;
                }
                catch (Exception feil)
                {
                    SkrivTilFil.skrivFeilTilFil(feil + "");
                    return null;
                }
          }
        
        public bool OpprettBildeTilVare(string[] bilder, int vareNr)
        {
            try
            {
                string melding = "";
                var db = new NettbutikkDB();
                Vare eksistVare = db.Varer.Find(vareNr);
                if (eksistVare == null)
                {
                    return false;
                }
                foreach(var b in bilder)
                {
                    if(b == "")
                    {
                        //Gjør ingenting
                    }
                    else
                    {
                        var nyttBilde = new Bilde()
                        {
                            src = b,
                            Vare = eksistVare
                        };
                        melding += " /Bilde: " + nyttBilde.src + " Har blitt knyttet til vare med id: " + eksistVare.VNr;
                        db.Bilder.Add(nyttBilde);
                        eksistVare.Bilde.Add(nyttBilde);
                    }
                }
                db.SaveChanges();
                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + melding);
                return true;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }

        public List<bilderimappe> ImagesInnhold()
        {
            try
            {
                List<bilderimappe> mapper = new List<bilderimappe>();
                DirectoryInfo dir = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/"));
                DirectoryInfo[] underDir = dir.GetDirectories();
                foreach(var map in underDir)
                {
                    List<string> filSti = new List<string>();
                    String sti = "../Images/"+map.Name;
                    var mappeBilder = new bilderimappe();
                    mappeBilder.Mappe = map.Name;
                    foreach(var mapFil in map.GetFiles())
                    {
                        string fil = "/" + mapFil.Name;
                        filSti.Add(sti + fil);
                    }
                    mappeBilder.filer = filSti;
                    mapper.Add(mappeBilder);
                }
                return mapper;
             }
             catch (Exception feil)
             {
                 SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
             }
        }

        public bool lastOppBilder(HttpPostedFileBase[] file, string mappe)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/"));
                DirectoryInfo[] underDir = dir.GetDirectories();
                foreach(var map in underDir)
                {
                    if(map.Name == mappe)
                    {
                      foreach(var img in file)
                      {
                          img.SaveAs(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/"+map.Name),img.FileName));
                      }
                      return true;
                    }
                }
                DirectoryInfo ny = dir.CreateSubdirectory(mappe);
                foreach (var img in file)
                {
                   img.SaveAs(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/" + ny.Name), img.FileName));
                }
                return true;
            }
            catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }

        public bool SlettBilder(string filnavn, string mappenavn)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/"));
                DirectoryInfo[] underDir = dir.GetDirectories();

                foreach (var map in underDir)
                {
                    if (map.Name == mappenavn)
                    {
                        foreach (var img in map.GetFiles())
                        {
                            if (Regex.IsMatch(filnavn, ".*" + img.Name + ".*"))
                           {
                               img.Delete();
                               return true;
                           }
                        }
                    }
                }
                return false;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }

        public void oppdaterKat()
        {
            try
            {
                var db = new NettbutikkDB();
                string melding = "";
                List<Kategori> alleKat = (from k in db.Kategorier select k).ToList();
                foreach (var kat in alleKat.Reverse<Kategori>())
                {
                    foreach (UnderKategori underK in kat.UnderKategori.Reverse<UnderKategori>())
                    {
                        foreach (Vare vare in underK.Vare.Reverse<Vare>())
                        {
                           if(vare.Slettet)
                           {
                               UnderKategori eksistUnderKat = db.UnderKategorier.FirstOrDefault(s => s.Navn == "SlettetVarer");
                               if (eksistUnderKat == null)
                               {
                                    eksistUnderKat = new UnderKategori()
                                    {
                                      Navn = "SlettetVarer"
                                    };
                                    vare.UnderKategori = eksistUnderKat;
                                    db.UnderKategorier.Add(eksistUnderKat);
                                    Kategori eksiKat = new Kategori()
                                    {
                                        Navn = "Slettet"
                                    };
                                    eksistUnderKat.Kategori = eksiKat;
                                    db.Kategorier.Add(eksiKat);
                                    melding += " /Vare med id: " + vare.VNr + " ble flyttet til slettet kategori ";
                               }
                               else
                               {
                                  vare.UnderKategori = eksistUnderKat;
                                  melding += " /Vare med id: " + vare.VNr + " ble flyttet til slettet kategori "; 
                               }                                
                           }
                        }
                    }
                }
                db.SaveChanges();
                foreach (Kategori kat in alleKat.Reverse<Kategori>())
                {
                    foreach (UnderKategori underK in kat.UnderKategori.Reverse<UnderKategori>())
                    {
                        if (underK.Vare.Count == 0 && underK.Navn != "SlettetVarer")
                         {
                             kat.UnderKategori.Remove(underK);
                             db.UnderKategorier.Remove(underK);
                             melding += "Underkategori: " + underK.Navn + " ble fjernet fordi den ikke inneholdt varer. ";
                         }
                    }
                    if(kat.UnderKategori.Count == 0)
                    {
                        db.Kategorier.Remove(kat);
                        melding += "Kategori: " + kat.Navn + " ble slettet fordi den ikke hadde underkategorier ";
                    }
                }
                db.SaveChanges();
                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + melding);
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
               
            }
        }
    }
}