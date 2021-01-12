using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe21.Data
{
    public class Urun
    {
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }

        public override string ToString()
        {
            return string.Format("{0} (₺ {1:n2})", UrunAd, BirimFiyat);
        }
    }
    public class SiparisDetay
    {
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }
        public int Adet { get; set; }
        public string TutarTL { get { return Tutar().ToString("c2"); } }
        public decimal Tutar()
        {
            return Adet * BirimFiyat;
        }
    }
    public class Siparis
    {
        public int MasaNo { get; set; }
        public SiparisDurum Durum { get; set; }
        public decimal OdenecekTutar { get; set; }
        public DateTime? AcilisZamani { get; set; } = DateTime.Now; // biz değer atayıncayaka kadar alacağı değeri girdik.
        public DateTime? KapanisZamani { get; set; }

        public List<SiparisDetay> SiparisDetaylar { get; set; } = new List<SiparisDetay>();
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
    public class KafeVeri
    {
        public int MasaAdet { get; set; } = 20;
        public List<Urun> Urunler { get; set; } = new List<Urun>();
        public List<Siparis> AktifSiparisler { get; set; } = new List<Siparis>();
        public List<Siparis> GecmisSiparisler { get; set; } = new List<Siparis>();
    }
}
