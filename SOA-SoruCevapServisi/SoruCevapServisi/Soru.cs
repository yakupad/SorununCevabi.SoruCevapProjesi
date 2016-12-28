using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoruCevapServisi
{
    public class Soru
    {
        public int soruID { get; set; }
        public string soruBasligi { get; set; }
        public string soruIcerigi { get; set; }
        public string soruSahibi { get; set; }
        public DateTime soruTarihi { get; set; }
        public int soruSahibiID { get; set; }
        public string soruSahibiResim { get; set; }
        public bool favori { get; set; }

    }
}