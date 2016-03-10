using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.DAL
{
    public class SkrivTilFil
    {
        public static void skrivFeilTilFil(string melding)
        {
            try
            {
                
                DirectoryInfo dir = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Logg/"));

                foreach(var f in dir.GetFiles())
                {
                    if(f.Name.Equals("feil.txt"))
                    {
                        using(StreamWriter sw = f.AppendText())
                        {
                            sw.WriteLine(melding);
                        }
                    }
                }

            }
            catch (Exception feil)
            {
                System.IO.File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Logg/feil.txt"),melding);
            }
        }

        public static void skrivEndringerTilFil(string melding)
        {
            try
            {
                var db = new NettbutikkDB();
                var l = new DBLogg() {
                    Dato = DateTime.Now,
                    Logg = melding                   
                };
                db.DBLogg.Add(l);
                db.SaveChanges();
                
                DirectoryInfo dir = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Logg/"));

                foreach (var f in dir.GetFiles())
                {
                    if (f.Name.Equals("dbLogg.txt"))
                    {
                        using (StreamWriter sw = f.AppendText())
                        {
                            sw.WriteLine(melding);

                            
                            
                        }
                    }
                }

            }catch(Exception feil)
            {
                skrivFeilTilFil(feil + "");
            }
        }

    }
}
