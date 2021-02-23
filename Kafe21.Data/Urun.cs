using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe21.Data
{
    [Table("Urunler")]
    public class Urun
    {
        public int Id { get; set; }

        [Required,MaxLength(100)]
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }

        public override string ToString()
        {
            return string.Format("{0} (₺ {1:n2})", UrunAd, BirimFiyat);
        }
        public virtual ICollection<SiparisDetay> SiparisDetaylar { get; set;}
    }

    [Table("SiparisDetaylar")]
    public class SiparisDetay
    {
        public int Id { get; set; }

        [Required,MaxLength(100)]
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }
        public int Adet { get; set; }

        [NotMapped] // veritabanında gözükmesin
        public string TutarTL { get { return Tutar().ToString("c2"); } }
        public int SiparisId { get; set; }
        public virtual Siparis Siparis { get; set; }

        public int UrunId { get; set; }

        public virtual Urun Urun { get; set; }
        public decimal Tutar()
        {
            return Adet * BirimFiyat;
        }
    }

    [Table("Siparisler")]
    public class Siparis
    {
        public int Id { get; set; }
        public int MasaNo { get; set; }
        public SiparisDurum Durum { get; set; }
        public decimal OdenecekTutar { get; set; }
        public DateTime? AcilisZamani { get; set; } = DateTime.Now; // biz değer atayıncayaka kadar alacağı değeri girdik.
        public DateTime? KapanisZamani { get; set; }

        public virtual ICollection<SiparisDetay> SiparisDetaylar { get; set; } = new HashSet<SiparisDetay>(); //HashSet tekrar edilemez
        public string ToplamTutarTL => ToplamTutar().ToString("c2");
        public decimal ToplamTutar()
        {
            return SiparisDetaylar.Sum(x => x.Tutar());
            //decimal toplam=0;
            //foreach (SiparisDetay detay in SiparisDetay)
            //{
            //    toplam += detay.Tutar();
            //}
            //return toplam;
        }
    }
    public class KafeVeri:DbContext
    {
        public KafeVeri(): base("name=KafeVeri")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiparisDetay>()//Siparis detay entity sinin birden 
                .HasRequired(x => x.Urun)  //Zorunlu olarak bir urun'u vardır
                .WithMany(x => x.SiparisDetaylar).// ki bu urun birden çok siparis detay'da bulunabilir
                HasForeignKey(x => x.UrunId) //
                .WillCascadeOnDelete(false);
        }
        public int MasaAdet { get; set; } = 20;
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SiparisDetay> SiparisDetaylar { get; set; }
    }
}
