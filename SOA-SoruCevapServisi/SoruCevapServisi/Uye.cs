using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoruCevapServisi
{
    public class Uye
    {
        public int UyeID { get; set; }
        public string kuladi { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string sifre { get; set; }
        public string eposta { get; set; }
        public DateTime kayittarihi { get; set; }
        public string kullaniciresmi { get; set; }
        public string meslek { get; set; }
        public string egitim { get; set; }
        public string konum { get; set; }
        public string yetenek { get; set; }
        public string not { get; set; }

    }

}