namespace Nettbutikk.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Drawing;
    using System.Linq;

    public class Ansatt
    {
        [Key]
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public bool ErAdmin { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Passord { get; set; }
        
    }
    public class Kunde
    {
        //public int KNr { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }
        public string TelefonNummer { get; set; }
        [Key]
        public string EPostAdresse { get; set; }
        public string Postnr { get; set; }
        public virtual Poststed Poststed { get; set; }
        public virtual List<Ordre> Ordre { get; set; }
        public virtual List<Kvittering> Kvittering { get; set; }
        public string Passord { get; set; }
        
    }

    public class Poststed
    {
        [Key]
        public string Postnr { get; set; }
        public string PersPoststed { get; set; }
        public virtual List<Kunde> Kunde { get; set; }
    }
 
    public class Ordre
    {
        [Key]
        public int OrdreNr { get; set; }
        public DateTime OrdreDato { get; set; }
        public virtual Kunde Kunde { get; set; }
        public virtual Vare Vare { get; set; }
        public virtual Kvittering Kvittering { get; set; }
        public int Antall { get; set; }
        public int PrisPrEnhet { get; set; }
        public bool ErLevert { get; set; }
        public bool ErBetalt { get; set; }
        public string detaljer { get;set; } //inneholder detaljer om ordren
    }

    public class Vare
    {
        [Key]
        public int VNr { get; set; }
        public string Betegnelse { get; set; }
        public int Pris { get; set; }
        public int Antall { get; set; }
        public string FremVisningsDetaljer { get; set; }
        public string Detaljer { get; set; }
        public bool Slettet { get; set; }
        public virtual UnderKategori UnderKategori { get; set; }
        public virtual List<Ordre> Ordre { get; set; }
        public virtual List<Bilde> Bilde { get; set; }

    }

    public class Kategori
    {
        [Key]
        public int KatNr { get; set; }
        public string Navn { get; set; }
        public virtual List<UnderKategori> UnderKategori { get; set; }
    }
    public class UnderKategori
    {
        [Key]
        public int Id { get; set; }
        public string Navn { get; set; }
        public virtual Kategori Kategori { get; set; }
        public virtual List<Vare> Vare { get; set; }
    }
    public class DBLogg 
    {
        [Key]
        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public string Logg { get; set; }

    }
    public class Kvittering
    {
        [Key]
        public int Id { get; set; }
        public DateTime Dato { get; set; }
        public int Pris { get; set; }
        public virtual List<Ordre> Ordre { get; set; }
        public Kunde Kunde { get; set; }
    }

    public class Bilde
    {
        [Key]
        public int Id { get; set; }
        public string src { get; set; }
        public virtual Vare Vare {get; set;}
    }

    public class FAQ
    {
        [Key]
        public int Id { get; set; }
        public string Sporsmal { get; set; }
        public string Svar { get; set; }
        public bool Fremvisning { get; set; }
        public int AntallKlikk { get; set; }
        public FAQKategori Kategori { get; set; }

    }
    public class FAQKategori
    {
        [Key]
        public int Id { get; set; }
        public string KategoriNavn { get; set; }
        public string Bilde { get; set; }
        public virtual List<FAQ> Innhold { get; set; }
    }

    public class NettbutikkDB : DbContext
    {
      
        public NettbutikkDB()
            : base("name=NettbutikkDB")
        {
           //Database.SetInitializer<NettbutikkDB>(new DropCreateDatabaseIfModelChanges<NettbutikkDB>());
            Database.CreateIfNotExists();
        }

        public DbSet<FAQ> FAQ { get;set; }
        public DbSet<FAQKategori> FAQKategorier { get; set; }
        public DbSet<Kvittering> Kvitteringer { get; set; }
        public DbSet<DBLogg> DBLogg { get; set; }
        public DbSet<Ordre> Ordrer { get; set; }  
        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Poststed> Poststeder { get; set; }
        public DbSet<Ansatt> Ansatte { get;set; }
        public DbSet<Vare> Varer { get; set; }
        public DbSet<Kategori> Kategorier { get; set; }
        public DbSet<Bilde> Bilder { get; set; }
        public DbSet<UnderKategori> UnderKategorier { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poststed>().HasKey(p => p.Postnr);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
        }

    }
}