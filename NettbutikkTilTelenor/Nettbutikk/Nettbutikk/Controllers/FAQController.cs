using Nettbutikk.BLL;
using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Nettbutikk.Scripts.App
{
    public class FAQController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var FAQDb = new FAQBLL();

            List<faqPresentasjon> AlleKategorier = FAQDb.ListKategorier();

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(AlleKategorier);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
        public HttpResponseMessage Get(int id)
        {
            var FAQDb = new FAQBLL();

            List<faqPresentasjon> SporsmalIKat = FAQDb.SporsmalIKategori(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(SporsmalIKat);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString,Encoding.UTF8,"application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public HttpResponseMessage Post(faqSettInn innFAQ)
        {
            if(ModelState.IsValid)
            {
                var FAQDb = new FAQBLL();

                bool OK = FAQDb.settInnSporsmal(innFAQ);

                if(OK)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Kunne ikke sette inn spørsmål i DB")
            };
        }
        public HttpResponseMessage Put(int id, [FromBody] faqPresentasjon innFAQ)
        {
            if(ModelState.IsValid)
            {
                var FAQDb = new FAQBLL();

                bool ok = FAQDb.oppdaterAntallKlikk(id, innFAQ);
                if(ok)
                {
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Kunne ikke endre antallKlikk i db")
            };
        }

       
      


    }
}
