using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.DAL
{
   public class FAQRepository
    {
       // atm ikke i bruk
       public List<faqPresentasjon> FrontSporsmal()
       {
           try
           {
               var db = new NettbutikkDB();

               List<faqPresentasjon> utfaq = new List<faqPresentasjon>();
               foreach(var faq in db.FAQ)
               {
                   if(faq.Fremvisning)
                   {
                       var fv = new faqPresentasjon()
                       {
                           sporsmal = faq.Sporsmal,
                           svar = faq.Svar
                       };
                       utfaq.Add(fv);
                   }
               }
               return utfaq;
           }catch(Exception feil)
           {
               SkrivTilFil.skrivFeilTilFil(feil + "");
               return null;
           }
       }
       public List<faqPresentasjon> AlleSporsmal()
       {
           try
           {
               var db = new NettbutikkDB();
               List<faqPresentasjon> allefaq = db.FAQ.Select(fq => new faqPresentasjon()
               {
                   id = fq.Id,
                   kategori= fq.Kategori.KategoriNavn,
                   sporsmal = fq.Sporsmal,
                   svar = fq.Svar,
                   fremvisning = fq.Fremvisning,
                   antallKlikk = fq.AntallKlikk
               }).ToList();
               return allefaq;
           }catch(Exception feil)
           {
               SkrivTilFil.skrivFeilTilFil(feil + "");
               return null;
           }
       }
       public List<faqPresentasjon> SporsmalIKategori (int id)
       {
           try
           {
               var db = new NettbutikkDB();
               List<faqPresentasjon> utlist = new List<faqPresentasjon>();
               var eksistKat = db.FAQKategorier.Find(id);
               if(eksistKat != null)
               {               
                   foreach(var k in eksistKat.Innhold)
                   {
                       var utS = new faqPresentasjon()
                       {
                           id = k.Id,
                           sporsmal = k.Sporsmal,
                           svar = k.Svar,
                           kategori = k.Kategori.KategoriNavn,
                           fremvisning = k.Fremvisning,
                           antallKlikk = k.AntallKlikk

                       };
                       utlist.Add(utS);
                   }
               }
               return utlist;
           }catch(Exception feil)
           {
               SkrivTilFil.skrivFeilTilFil(feil + "");
               return null;
           }
       }
       public List<faqPresentasjon> ListKategorier ()
       {
           try
           {
               var db = new NettbutikkDB();
               List<faqPresentasjon> utliste = db.FAQKategorier.Select(k => new faqPresentasjon()
               {
                   id = k.Id,
                   kategori = k.KategoriNavn,
                   bilde = k.Bilde
               }).ToList();
               return utliste;
           }catch(Exception feil)
           {
               SkrivTilFil.skrivFeilTilFil(feil + "");
               return null;
           }
       }
       public bool settInnSporsmal(faqSettInn innFAQ)
       {
           try
           {
               var db = new NettbutikkDB();

               var nyFAQ = new FAQ()
               {
                  
                   Sporsmal = innFAQ.sporsmal,
                   Svar = "Ikke besvart ennå. Sendt inn av " + innFAQ.fornavn
                  
               };
               var kat = "Innsendte Spørsmål";
               var eksistKategori = db.FAQKategorier.FirstOrDefault(fk => fk.KategoriNavn.Equals(kat));

               if(eksistKategori != null)
               {
                   nyFAQ.Kategori = eksistKategori;
                   eksistKategori.Innhold.Add(nyFAQ);
               }
               else
               {
                   var nyKat = new FAQKategori()
                   {
                       KategoriNavn = kat
                   };
                   nyKat.Innhold.Add(nyFAQ);
               }
               db.SaveChanges();
               return true;
           }catch(Exception feil)
           {
               SkrivTilFil.skrivFeilTilFil(feil + "");
               return false;
           }
       }
       public bool oppdaterAntallKlikk (int id, faqPresentasjon innFAQ)
       {
           try
           {
               var db = new NettbutikkDB();

               FAQ funnetFAQ = db.FAQ.Find(id);

               if(funnetFAQ == null)
               {
                   return false;
               }
               funnetFAQ.AntallKlikk = innFAQ.antallKlikk;

               db.SaveChanges();
               return true;

           }catch(Exception feil)
           {
               SkrivTilFil.skrivFeilTilFil(feil + "");
               return false;
           }
       }
    }
}
