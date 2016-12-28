using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoruCevapServisi
{
    public class Yorum
    {
        public int cevapID { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string yorumSahibi { get; set; }
        public DateTime yorumTarihi { get; set; }
        public string yorumcuresmi { get; set; }
        public string yorumcuadi { get; set; }
        public string yorumcusoyadi { get; set; }
        public int yorumSahibiID { get; set; }

    }
}