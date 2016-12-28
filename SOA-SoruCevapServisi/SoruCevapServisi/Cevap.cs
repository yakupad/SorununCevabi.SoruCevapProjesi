using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoruCevapServisi
{
    public class Cevap
    {
        public int cevapID { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string cevapSahibi { get; set; }
        public int soruNo { get; set; }
        public DateTime cevapTarihi { get; set; }
        public string cevapciresmi { get; set; }
        public List<Yorum> yorumlar = new List<Yorum>();
        public bool faydalimi { get; set; }
        public int cevapSahibiID { get; set; }
        public bool favori { get; set; }
    }
}