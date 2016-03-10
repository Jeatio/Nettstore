using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nettbutikk.Controllers;
using Nettbutikk.BLL;
using Nettbutikk.Model.PresentasjonsModel;
using Nettbutikk.DAL;
using System.Web.Mvc;
using System.Web;
using MvcContrib.TestHelper;
using System.Collections.Generic;
using System.Web.SessionState;


namespace Enhetstest
{
    [TestClass]
    public class AnsattControllerTest
    {
        [TestMethod]
        public void List_AlleAnsatt_View()
        {
            
            TestControllerBuilder builder = new TestControllerBuilder();
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            builder.InitializeController(controller);
           
            var forventetResultat = new List<ansatt>();
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = true,
                passord = "123"
            };
            forventetResultat.Add(nyansatt);
            forventetResultat.Add(nyansatt);
            forventetResultat.Add(nyansatt);

            controller.Session["ErAdmin"] = nyansatt;
            //Act        
            var resultat = (ViewResult)controller.ListAnsatte();
            var resultatListe = (List<ansatt>)resultat.Model;
            
            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            for(var i = 0; i<resultatListe.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].idnr, resultatListe[i].idnr);
                Assert.AreEqual(forventetResultat[i].fornavn, resultatListe[i].fornavn);
                Assert.AreEqual(forventetResultat[i].etternavn, resultatListe[i].etternavn);
                Assert.AreEqual(forventetResultat[i].brukernavn, resultatListe[i].brukernavn);
                Assert.AreEqual(forventetResultat[i].eradmin, resultatListe[i].eradmin);
                Assert.AreEqual(forventetResultat[i].passord, resultatListe[i].passord);
            }            
        }
       
        [TestMethod]
        public void List_AlleAnsatt_feil_View()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            //Act
            var resultat = (RedirectToRouteResult)controller.ListAnsatte();

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListKunder");
        }
        [TestMethod]
        public void List_AlleAnsatt_feil_admin()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;
            //Act
            var resultat = (RedirectToRouteResult)controller.ListAnsatte();

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListKunder");
        }
        [TestMethod]
        public void OpprettAnsatt_vis_View()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = true,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            //Act
            var result = (ViewResult)controller.OpprettAnsatt();

            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void OpprettAnsatt_feil_vis_View()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            //Act
            var result = (RedirectToRouteResult)controller.OpprettAnsatt();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListKunder");
        }
        [TestMethod]
        public void OpprettAnsatt_feilAdmin_View()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;
            //Act
            var result = (RedirectToRouteResult)controller.OpprettAnsatt();

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListKunder");
        }

        [TestMethod]
        public void OpprettAnsatt_OK_Post()
        {
            
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = true,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innAnsatt = new ansatt()
            {
                
                fornavn = "Hans Magnus",
                etternavn = "Hallaråker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };

            //Act

            var resultat = (RedirectToRouteResult)controller.OpprettAnsatt(innAnsatt);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListAnsatte");
           
        }

        [TestMethod]
        public void OpprettAnsatt_feil_validering_Post()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));

            var innAnsatt = new ansatt();
            controller.ViewData.ModelState.AddModelError("fornavn", "Ikke oppgitt fornavn");
            //Act
            var resultat = (RedirectToRouteResult)controller.OpprettAnsatt(innAnsatt);

            //Assert          
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");
        }
        [TestMethod]
        public void OpprettAnsatt_feil_DB_Post()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innAnsatt = new ansatt();
            innAnsatt.fornavn = "";

            //Act
            var actionResult = (RedirectToRouteResult)controller.OpprettAnsatt(innAnsatt);

            //Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void Slett_feil_en_Ansatt()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innAnsatt = new ansatt();
            innAnsatt.idnr = 0;

            //Act
            var result = (bool)controller.SlettAnsatt(innAnsatt.idnr);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Slett_k_DB_Ansatt()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = true,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innAnsatt = new ansatt();
            innAnsatt.idnr = 0;

            //Act
            var result = (bool)controller.SlettAnsatt(innAnsatt.idnr);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Slett_Ansatt_feilAdmin()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

            var innAnsatt = new ansatt();
            innAnsatt.idnr = 0;

            //Act
            var result = (bool)controller.SlettAnsatt(innAnsatt.idnr);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Slett_en_Ansatt()
        {
            //Arrange
            var controller = new AnsattController(new AnsattLogikk(new AnsattRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = true,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innAnsatt = new ansatt();
            innAnsatt.idnr = 1;

            //Act
            var result = (bool)controller.SlettAnsatt(innAnsatt.idnr);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Slett_Bilde_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            string fil = null;
            //Act
            var result = (string)controller.SlettBilde(fil, "");

            //Assert
            Assert.AreEqual("False", result);
        }
        public void Slett_Bilde()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            string fil = "dsada";
            //Act
            var result = (string)controller.SlettBilde(fil, "");

            //Assert
            Assert.AreEqual("True", result);
        }
        [TestMethod]
        public void Slett_Bilde_Post_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            string fil = "dsada";
            //Act
            var result = (string)controller.SlettBilde(fil, "mappe");
            //Assert
            Assert.AreEqual("mappe", result);
        }
        [TestMethod]
        public void Slett_Bilde_Post_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            string fil = "";
            //Act
            var result = (string)controller.SlettBilde(fil, "");

            //Assert
            Assert.AreEqual("False", result);
        }
        [TestMethod]
        public void UploadBilde_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            string mappe = null;
            HttpPostedFileBase[] dummy = null;
            //Act
            var result = (RedirectToRouteResult)controller.UploadBilde(dummy, mappe);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "visAlleVareBilder");
            
        }
        [TestMethod]
        public void UploadBilde_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            string mappe = "h";
            HttpPostedFileBase[] k = new HttpPostedFileBase[2];
            
            //Act
            var result = (RedirectToRouteResult)controller.UploadBilde( k,mappe);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "visAlleVareBilder");

        }
        [TestMethod]
        public void UploadBilde_OK_Post_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            string mappe = "";
            HttpPostedFileBase[] k = new HttpPostedFileBase[2];

            //Act
            var result = (RedirectToRouteResult)controller.UploadBilde(k, mappe);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "visAlleVareBilder");

        }
        [TestMethod]
        public void LeggInnBilde_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            string[] b = null;
            int vnr = 0;

            //Act
            var result = (string)controller.leggInnBilde(b, vnr);

            //Assert
            Assert.AreEqual("False", result);
        }
        [TestMethod]
        public void LeggInnBilde_PostFeil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            string[] b = new string[2];
            int vnr = 1;

            //Act
            var result = (string)controller.leggInnBilde(b, vnr);

            //Assert
            Assert.AreEqual("False", result);
        }
        [TestMethod]
        public void LeggInnBilde_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            string[] b = new string[2];
            int vnr = 2;

            //Act
            var result = (string)controller.leggInnBilde(b, vnr);

            //Assert
            Assert.AreEqual("True", result);
        }
        [TestMethod]
        public void VisAlleVareBilder_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            var bildeListe = new List<bilderimappe>();
            var filn = new List<string>();
            filn.Add("fil");
            filn.Add("fil");
            var bilde = new bilderimappe()
            {
                filer = filn,
                Mappe = "test"
            };
            bildeListe.Add(bilde);
            bildeListe.Add(bilde);
            bildeListe.Add(bilde);

            //Act        
            var resultat = (ViewResult)controller.visAlleVareBilder();
            var resultatListe = (List<bilderimappe>)resultat.Model;

            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            for (var i = 0; i < resultatListe.Count; i++)
            {
                Assert.AreEqual(bildeListe[i].filer.ToString(), resultatListe[i].filer.ToString());
                Assert.AreEqual(bildeListe[i].Mappe, resultatListe[i].Mappe);

            } 
        }
        [TestMethod]
        public void FjernBildeFraVare_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            int i = 0;
            int j = 0;

            //Act
            var resultat = (string)controller.fjernBildeFraVare(i, j);

            //Assert
            Assert.AreEqual("", resultat);
        }
        [TestMethod]
        public void FjernBildeFraVare_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            int i = 1;
            int j = 0;

            //Act
            var resultat = (string)controller.fjernBildeFraVare(i, j);

            //Assert
            Assert.AreEqual("OK", resultat);
        }

        [TestMethod]
        public void EndreVare_View_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

            var bildeListe = new List<visBilde>();
                var varen = new endreVare()
                {
                    antall = 2,
                    betegnelse = "flott produkt",
                    detaljer = "flott produktflott produktflott produkt",
                    fremvisningsdetaljer = "flott produkt",
                    idnr = 1,
                    katnavn = "PC",
                    pris = 1000,
                    underkatnavn = "Skjermer",
                    katid = 1,
                    underkatid = 1,
                };
                var bilde = new visBilde()
                {
                    id = 1,
                    src = "test"
                };
                bildeListe.Add(bilde);
                bildeListe.Add(bilde);
                varen.bilde = bildeListe;

            //Act
                var resultat = (RedirectToRouteResult)controller.EndreVaren(varen.idnr);

            //Assert
                Assert.AreEqual(resultat.RouteName, "");
                Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");
        }
        [TestMethod]
        public void EndreVare_post_feilId()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var bildeListe = new List<visBilde>();
            var varen = new endreVare()
            {
                antall = 2,
                betegnelse = "flott produkt",
                detaljer = "flott produktflott produktflott produkt",
                fremvisningsdetaljer = "flott produkt",
                idnr = 0,
                katnavn = "PC",
                pris = 1000,
                underkatnavn = "Skjermer",
                katid = 1,
                underkatid = 1,
            };
            var bilde = new visBilde()
            {
                id = 1,
                src = "test"
            };
            bildeListe.Add(bilde);
            bildeListe.Add(bilde);
            varen.bilde = bildeListe;

            //Act
            var resultat = (ViewResult)controller.EndreVaren(varen.idnr);
            var VTilbake = (endreVare)resultat.Model;
            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            Assert.AreEqual(VTilbake.idnr, 0);
        }

        [TestMethod]
        public void EndreVare_View_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var bildeListe = new List<visBilde>();
            var varen = new endreVare()
            {
                antall = 2,
                betegnelse = "flott produkt",
                detaljer = "flott produktflott produktflott produkt",
                fremvisningsdetaljer = "flott produkt",
                idnr = 1,
                katnavn = "PC",
                pris = 1000,
                underkatnavn = "Skjermer",
                katid = 1,
                underkatid = 1,
            };
            var bilde = new visBilde()
            {
                id = 1,
                src = "test"
            };
            bildeListe.Add(bilde);
            bildeListe.Add(bilde);
            varen.bilde = bildeListe;

            //Act
            var resultat = (ViewResult)controller.EndreVaren(varen.idnr);

            //Assert
            Assert.AreEqual(resultat.ViewName, "");

        }
        [TestMethod]
        public void EndreVare_post_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var bildeListe = new List<visBilde>();
            var varen = new endreVare()
            {
                antall = 2,
                betegnelse = "ses",
                detaljer = "flott produktflott produktflott produkt",
                fremvisningsdetaljer = "flott produkt",
                idnr = 0,
                katnavn = "PC",
                pris = 1000,
                underkatnavn = "Skjermer",
                katid = 1,
                underkatid = 1,
            };
            var bilde = new visBilde()
            {
                id = 1,
                src = "test"
            };
            bildeListe.Add(bilde);
            bildeListe.Add(bilde);
            varen.bilde = bildeListe;

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreVaren(varen);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(),"ListVarer");


        }

        [TestMethod]
        public void EndreVare_Validering_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var bildeListe = new List<visBilde>();
            var varen = new endreVare();

            controller.ViewData.ModelState.AddModelError("feil", "tom betegnelse");
            //Act
            var resultat = (RedirectToRouteResult)controller.EndreVaren(varen);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListVarer");
            //Assert.AreEqual(resultat.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "tom betegnelse");
        }
        [TestMethod]
        public void EndreVare_validering_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var bildeListe = new List<visBilde>();
            var varen = new endreVare()
            {
                antall = 2,
                betegnelse = "flott produkt",
                detaljer = "flott produktflott produktflott produkt",
                fremvisningsdetaljer = "flott produkt",
                idnr = 1,
                katnavn = "PC",
                pris = 1000,
                underkatnavn = "Skjermer",
                katid = 1,
                underkatid = 1,
            };
            var bilde = new visBilde()
            {
                id = 1,
                src = "test"
            };
            bildeListe.Add(bilde);
            bildeListe.Add(bilde);
            varen.bilde = bildeListe;

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreVaren(varen);

            //Assert
            Assert.AreEqual(resultat.RouteName,"");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListVarer");
        }

        [TestMethod]
        public void SlettVare_OKFeil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            int v = 0;
            //Act
            var resultat = (RedirectToRouteResult)controller.SlettVare(v);

            //Assert
            Assert.AreEqual(resultat.RouteName,"");
            Assert.AreEqual(resultat.RouteValues.Values.First(),"ListVarer");
        }
        [TestMethod]
        public void SlettVare_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));

            int v = 1;
            //Act
            var resultat = (RedirectToRouteResult)controller.SlettVare(v);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListVarer");
        }
        [TestMethod]
        public void OpprettVare_vis_View()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            //Act
            var resultat = (ViewResult)controller.OpprettVare();

            //Assert
            Assert.AreEqual(resultat.ViewName,"");
        }
        [TestMethod]
        public void OpprettVare_vis_View_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;
            //Act
            var resultat = (RedirectToRouteResult)controller.OpprettVare();

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");
        }
        [TestMethod]
        public void OpprettVare_validering_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            var innVare = new vare();
            innVare.betegnelse = "";
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            //Act
            var resultat = (RedirectToRouteResult)controller.OpprettVare(innVare, 1, 1);

            //Assert
            Assert.AreEqual(resultat.RouteName,"");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListVarer");

        }
        [TestMethod]
        public void OpprettVare_validering_OK_feilSession()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            var innVare = new vare();
            innVare.betegnelse = "OK";
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

            //Act
            var resultat = (RedirectToRouteResult)controller.OpprettVare(innVare, 1, 1);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListVarer");

        }
        [TestMethod]
        public void OpprettVare_Post_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            var innVare = new vare();
            innVare.betegnelse = "hei";
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            //Act
            var resultat = (RedirectToRouteResult)controller.OpprettVare(innVare, 1, 1);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListVarer");
        }
        [TestMethod]
        public void OpprettVare_Post_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            var innVare = new vare();
            innVare.betegnelse = "";
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            //Act
            var resultat = (RedirectToRouteResult)controller.OpprettVare(innVare, 1, 1);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListVarer");
        }
        [TestMethod]
        public void ListVarer_View_feil()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

            //Act
            var resultat = (RedirectToRouteResult)controller.ListVarer();

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");
        }

        [TestMethod]
        public void ListVarer_View_OK()
        {
            //Arrange
            var controller = new AnsattController(new VareLogikk(new VareRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var vareListe = new List<visVarerAdmin>();
            var varen = new visVarerAdmin()
            {
                antall = 2,
                betegnelse = "flott produkt",
                detaljer = "flott produktflott produktflott produkt",
                fremvisningsdetaljer = "flott produkt",
                idnr = 1,
                katnavn = "PC",
                pris = 1000,
                slettet = false,
                underkatnavn = "Skjermer",
                visvarebilde = "../Images/bilde.jpg"
            };
            vareListe.Add(varen);
            vareListe.Add(varen);
            vareListe.Add(varen);

            //Act        
            var resultat = (ViewResult)controller.ListVarer();
            var resultatListe = (List<visVarerAdmin>)resultat.Model;

            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            for (var i = 0; i < resultatListe.Count; i++)
            {
                Assert.AreEqual(vareListe[i].idnr, resultatListe[i].idnr);
                Assert.AreEqual(vareListe[i].antall, resultatListe[i].antall);
                Assert.AreEqual(vareListe[i].betegnelse, resultatListe[i].betegnelse);
                Assert.AreEqual(vareListe[i].detaljer, resultatListe[i].detaljer);
                Assert.AreEqual(vareListe[i].fremvisningsdetaljer, resultatListe[i].fremvisningsdetaljer);
                Assert.AreEqual(vareListe[i].katnavn, resultatListe[i].katnavn);

                Assert.AreEqual(vareListe[i].pris, resultatListe[i].pris);
                Assert.AreEqual(vareListe[i].slettet, resultatListe[i].slettet);
                Assert.AreEqual(vareListe[i].underkatnavn, resultatListe[i].underkatnavn);
                Assert.AreEqual(vareListe[i].visvarebilde, resultatListe[i].visvarebilde);
            }    
        }

        [TestMethod]
        public void Viskvittering_View()
        {
            //Arrange
            var controller = new AnsattController(new OrdreLogikk(new OrdreRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var kvitListe = new List<viskvittering>();
            var kvitt = new viskvittering()
            {
                antall = 2,
                betegnelse = "flott produkt",
                detaljer = "Daniel",
                prisPrEnhet = 1000,
                totpris = 3000,
                dato = new DateTime()
            };
            kvitListe.Add(kvitt);
            kvitListe.Add(kvitt);
            kvitListe.Add(kvitt);

            //Act        
            var resultat = (ViewResult)controller.VisKvittering("hei",1);
            var resultatListe = (List<viskvittering>)resultat.Model;

            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            for (var i = 0; i < resultatListe.Count; i++)
            {
                Assert.AreEqual(kvitListe[i].antall, resultatListe[i].antall);
                Assert.AreEqual(kvitListe[i].betegnelse, resultatListe[i].betegnelse);
                Assert.AreEqual(kvitListe[i].totpris, resultatListe[i].totpris);
                Assert.AreEqual(kvitListe[i].detaljer, resultatListe[i].detaljer);
                Assert.AreEqual(kvitListe[i].prisPrEnhet, resultatListe[i].prisPrEnhet);
                Assert.AreEqual(kvitListe[i].dato, resultatListe[i].dato);
            }
        }
        [TestMethod]
        public void Viskvittering_View_feil()
        {
            //Arrange
            var controller = new AnsattController(new OrdreLogikk(new OrdreRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

            //Act        
            var resultat = (RedirectToRouteResult)controller.VisKvittering("", 1);


            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");
        }
        [TestMethod]
        public void Listkvittering_View_OK()
        {
            //Arrange
            var controller = new AnsattController(new OrdreLogikk(new OrdreRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var ordreListe = new List<visOrdreAdmin>();
            var ordre = new visOrdreAdmin()
            {
                id = 2,
                pris = 1233,
                fornavn = "Daniel",
                etternavn = "Rein",
                epostadresse = "Dan@hot.com",
                dato = new DateTime()
            };
            ordreListe.Add(ordre);
            ordreListe.Add(ordre);
            ordreListe.Add(ordre);

            //Act        
            var resultat = (ViewResult)controller.ListKvitteringer();
            var resultatListe = (List<visOrdreAdmin>)resultat.Model;

            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            for (var i = 0; i < resultatListe.Count; i++)
            {
                Assert.AreEqual(ordreListe[i].id, resultatListe[i].id);
                Assert.AreEqual(ordreListe[i].pris, resultatListe[i].pris);
                Assert.AreEqual(ordreListe[i].fornavn, resultatListe[i].fornavn);
                Assert.AreEqual(ordreListe[i].etternavn, resultatListe[i].etternavn);
                Assert.AreEqual(ordreListe[i].epostadresse, resultatListe[i].epostadresse);
                Assert.AreEqual(ordreListe[i].dato, resultatListe[i].dato);
            }
        }
        [TestMethod]
        public void Listkvittering_View_feil()
        {
            //Arrange
            var controller = new AnsattController(new OrdreLogikk(new OrdreRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;


            //Act        
            var resultat = (RedirectToRouteResult)controller.ListKvitteringer();

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");

        }
        [TestMethod]
        public void SlettKunde_feil()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

            //Act
            var resultat = (bool)controller.Slett("");

            //Assert
            Assert.IsFalse(resultat);
        }
        [TestMethod]
        public void SlettKunde_OKfeil()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            string email = "";
            //Act
            var resultat = (bool)controller.Slett(email);

            //Assert
            Assert.IsFalse(resultat);
        }
        [TestMethod]
        public void SlettKunde_OK()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            string email = "ses";
            //Act
            var resultat = (bool)controller.Slett(email);

            //Assert
            Assert.IsTrue(resultat);
        }

        [TestMethod]
        public void DetaljerView_feil()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;


            //Act
            var resultat = (RedirectToRouteResult)controller.Detaljer("");

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");
        }
        [TestMethod]
        public void DetaljerView_feilmail()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;


            //Act
            var resultat = (ViewResult)controller.Detaljer("");
            var tTilbake = (minProfil)resultat.Model;

            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            Assert.AreEqual(tTilbake.epostadresse,"");
        }
        [TestMethod]
        public void DetaljerView_OK()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;
            var fkunde = new minProfil()
            {

                fornavn = "Per",
                etternavn = "Olsen",
                adresse = "Osloveien 82",
                postnr = "1234",
                poststeder = "Oslo",
                telefonnummer = "12345678",
                epostadresse = "riktig"
            };

            //Act
            var resultat = (ViewResult)controller.Detaljer("s");
            var tkunde = (minProfil)resultat.Model;
            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            Assert.AreEqual(fkunde.epostadresse, tkunde.epostadresse);
            Assert.AreEqual(fkunde.telefonnummer, tkunde.telefonnummer);
            Assert.AreEqual(fkunde.fornavn, tkunde.fornavn);
            Assert.AreEqual(fkunde.etternavn, tkunde.etternavn);
            Assert.AreEqual(fkunde.adresse, tkunde.adresse);
            Assert.AreEqual(fkunde.postnr, tkunde.postnr);
            Assert.AreEqual(fkunde.poststeder, tkunde.poststeder);
        }

        [TestMethod]
        public void ListKunder_View()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var kundeListe = new List<minProfil>();
            var k = new minProfil()
            {
                fornavn = "Per",
                etternavn = "Olsen",
                epostadresse = "per@ya.no",
                adresse = "Osloveien 82",
                postnr = "1087",
                poststeder = "Oslo",
                telefonnummer = "12345678"
            };
            kundeListe.Add(k);
            kundeListe.Add(k);
            kundeListe.Add(k);

            //Act        
            var resultat = (ViewResult)controller.ListKunder();
            var resultatListe = (List<minProfil>)resultat.Model;

            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            for (var i = 0; i < resultatListe.Count; i++)
            {
                Assert.AreEqual(kundeListe[i].postnr, resultatListe[i].postnr);
                Assert.AreEqual(kundeListe[i].adresse, resultatListe[i].adresse);
                Assert.AreEqual(kundeListe[i].fornavn, resultatListe[i].fornavn);
                Assert.AreEqual(kundeListe[i].etternavn, resultatListe[i].etternavn);
                Assert.AreEqual(kundeListe[i].epostadresse, resultatListe[i].epostadresse);
                Assert.AreEqual(kundeListe[i].poststeder, resultatListe[i].poststeder);
                Assert.AreEqual(kundeListe[i].telefonnummer, resultatListe[i].telefonnummer);
            }
        }
        [TestMethod]
        public void ListKunder_View_feil()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

           

            //Act        
            var resultat = (RedirectToRouteResult)controller.ListKunder();


            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");

        }
        [TestMethod]
        public void EndreKunde_View_feil()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreKunde("");

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");
        }
        [TestMethod]
        public void EndreKunde_View_feilKunde()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            //Act
            var resultat = (ViewResult)controller.EndreKunde("");
            var tTilbake = (endreKunde)resultat.Model;
            //Assert
            Assert.AreEqual(resultat.ViewName, "");
            Assert.AreEqual(tTilbake.fornavn,"");
        }
        [TestMethod]
        public void EndreKunde_View()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var fkunde = new endreKunde()
            {

                fornavn = "Per",
                etternavn = "Olsen",
                adresse = "Osloveien 82",
                postnr = "1234",
                poststeder = "Oslo",
                telefonnummer = "12345678",
                passord = "123"
            };
            //Act
            var resultat = (ViewResult)controller.EndreKunde("ses");
            var tkunde = (endreKunde)resultat.Model;
            //Assert
            Assert.AreEqual(resultat.ViewName, "");


            Assert.AreEqual(fkunde.telefonnummer, tkunde.telefonnummer);
            Assert.AreEqual(fkunde.fornavn, tkunde.fornavn);
            Assert.AreEqual(fkunde.etternavn, tkunde.etternavn);
            Assert.AreEqual(fkunde.adresse, tkunde.adresse);
            Assert.AreEqual(fkunde.postnr, tkunde.postnr);
            Assert.AreEqual(fkunde.poststeder, tkunde.poststeder);
            Assert.AreEqual(fkunde.passord, tkunde.passord);
        }
        [TestMethod]
        public void EndreKunde_validering_feil()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innKunde = new endreKunde();
            string email = "";

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreKunde(email, innKunde);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");

        }
        [TestMethod]
        public void EndreKunde_feilAdmin()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));

            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = null;

            var innKunde = new endreKunde()
                {
                    fornavn = "Per",
                    etternavn = "Olsen",
                    adresse = "Osloveien 82",
                    postnr = "1234",
                    poststeder = "Oslo",
                    telefonnummer = "12345678",
                    passord = "123"
                };
            string email = "s";

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreKunde(email, innKunde);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");

        }

        [TestMethod]
        public void EndreKunde_feilAdminPassord()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));

            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                passord = "123"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innKunde = new endreKunde()
            {
                fornavn = "Per",
                etternavn = "Olsen",
                adresse = "Osloveien 82",
                postnr = "1234",
                poststeder = "Oslo",
                telefonnummer = "12345678",
                passord = "OK"
            };
            string email = "s";

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreKunde(email, innKunde);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");

        }
        [TestMethod]
        public void EndreKunde_Post_OK()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));

            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,
                
                passord = "OK"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innKunde = new endreKunde()
            {
                fornavn = "Per",
                etternavn = "Olsen",
                adresse = "Osloveien 82",
                postnr = "1234",
                poststeder = "Oslo",
                telefonnummer = "12345678",
                passord = "OK"
            };
            string email = "hm";

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreKunde(email, innKunde);
           
            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "ListKunder");

        }
        [TestMethod]
        public void EndreKunde_Post_feil()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));

            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,

                passord = "OK"
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innKunde = new endreKunde()
            {
                fornavn = "Per",
                etternavn = "Olsen",
                adresse = "Osloveien 82",
                postnr = "1234",
                poststeder = "Oslo",
                telefonnummer = "12345678",
                passord = "OK"
            };
            string email = "";

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreKunde(email, innKunde);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");

        }

        [TestMethod]
        public void EndreKunde_Post_feilp()
        {
            //Arrange
            var controller = new AnsattController(new KundeLogikk(new KundeRepositoryStub()));

            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);
            var nyansatt = new ansatt()
            {
                idnr = 2,
                fornavn = "Hans Magnus",
                etternavn = "Hallaraker",
                brukernavn = "hm@hd.no",
                eradmin = false,

                passord = ""
            };
            controller.Session["ErAdmin"] = nyansatt;

            var innKunde = new endreKunde()
            {
                fornavn = "Per",
                etternavn = "Olsen",
                adresse = "Osloveien 82",
                postnr = "1234",
                poststeder = "Oslo",
                telefonnummer = "12345678",
                passord = ""
            };
            string email = "";

            //Act
            var resultat = (RedirectToRouteResult)controller.EndreKunde(email, innKunde);

            //Assert
            Assert.AreEqual(resultat.RouteName, "");
            Assert.AreEqual(resultat.RouteValues.Values.First(), "Index");

        }
        
    }
}
