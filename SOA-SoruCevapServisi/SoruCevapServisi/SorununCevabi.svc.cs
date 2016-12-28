using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SoruCevapServisi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SorununCevabi" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SorununCevabi.svc or SorununCevabi.svc.cs at the Solution Explorer and start debugging.
    public class SorununCevabi : ISorununCevabi
    {

        sorununcevabiEntitiesDB db = new sorununcevabiEntitiesDB();

        public List<Cevap> cevapcek(int soruID, int uyeID)
        {
            List<Cevap> cvplist = new List<Cevap>();

            foreach (Cevaplar item in db.Cevaplar.Where(x => x.soruNo == soruID && x.yorumID == null))
            {
                Cevap cvp = new Cevap();
                cvp.Baslik = item.cevapBasligi;
                cvp.cevapID = item.cevapID;
                cvp.cevapSahibi = db.Uyeler.FirstOrDefault(x => x.uyeID == item.Uyeler.uyeID).kuladi;
                cvp.cevapTarihi = Convert.ToDateTime(item.cevapTarihi);
                cvp.faydalimi = item.faydali == 1 ? true : false;
                cvp.Icerik = item.cevapIcerigi;
                cvp.soruNo = item.soruNo;
                cvp.cevapciresmi = db.Uyeler.FirstOrDefault(x => x.uyeID == item.Uyeler.uyeID).kullaniciresmi;
                cvp.favori = db.FavoriCevap.Any(x => x.uyeID == uyeID && x.cevapID == item.cevapID);
                cvp.cevapSahibiID = db.Uyeler.FirstOrDefault(x => x.uyeID == item.Uyeler.uyeID).uyeID;

                foreach (Cevaplar it in db.Cevaplar.Where(x => x.yorumID == item.cevapID))
                {
                    Yorum yrm = new Yorum();
                    yrm.Baslik = it.cevapBasligi;
                    yrm.cevapID = it.cevapID;
                    yrm.Icerik = it.cevapIcerigi;
                    yrm.yorumSahibi = db.Uyeler.FirstOrDefault(x => x.uyeID == it.Uyeler.uyeID).kuladi;
                    yrm.yorumTarihi = Convert.ToDateTime(it.cevapTarihi);
                    yrm.yorumcuresmi = db.Uyeler.FirstOrDefault(x => x.uyeID == it.Uyeler.uyeID).kullaniciresmi;
                    yrm.yorumcuadi = db.Uyeler.FirstOrDefault(x => x.uyeID == it.Uyeler.uyeID).adi;
                    yrm.yorumcusoyadi = db.Uyeler.FirstOrDefault(x => x.uyeID == it.Uyeler.uyeID).soyadi;
                    yrm.yorumSahibiID = db.Uyeler.FirstOrDefault(x => x.uyeID == it.Uyeler.uyeID).uyeID;
                    cvp.yorumlar.Add(yrm);
                }
                cvplist.Add(cvp);

            }
            return cvplist;
        }

        public bool cevapekle(string cevapBasligi, string cevapİcerigi, int cevapSahibi, int soruNo, int yorumID)
        {

            Cevaplar cevaplar = new Cevaplar();
            cevaplar.cevapBasligi = cevapBasligi;
            cevaplar.cevapIcerigi = cevapİcerigi;
            cevaplar.cevapSahibi = cevapSahibi;
            cevaplar.cevapTarihi = DateTime.Now;

            cevaplar.soruNo = soruNo;
            cevaplar.faydali = 0;
            if (yorumID == 0)
                cevaplar.yorumID = null;
            else
                cevaplar.yorumID = yorumID;

            db.Cevaplar.Add(cevaplar);
            db.SaveChanges();

            return true;
        }

        public bool cevapfavekle(int uyeID, int cevapID)
        {
            FavoriCevap fc = new FavoriCevap();
            fc.uyeID = uyeID;
            fc.cevapID = cevapID;

            db.FavoriCevap.Add(fc);
            db.SaveChanges();

            return true;
        }

        public int giris(string kullaniciadi, string sifre)
        {
            if (db.Uyeler.Any(x => ((x.kuladi == kullaniciadi) || (x.eposta == kullaniciadi)) && (x.sifre == sifre)))
                return db.Uyeler.FirstOrDefault(x => ((x.kuladi == kullaniciadi) || (x.eposta == kullaniciadi)) && x.sifre == sifre).uyeID;
            else return -1;
        }

        public int kayit(string kullaniciadi, string ad, string soyad, string sifre, string eposta, string kullaniciresmi, string meslek)
        {
            Uyeler u = new Uyeler();
            u.kuladi = kullaniciadi;
            u.adi = ad;
            u.soyadi = soyad;
            u.sifre = sifre;
            u.eposta = eposta;
            u.kayittarihi = DateTime.Now;
            u.kullaniciresmi = kullaniciresmi;
            u.Meslek = meslek;

            db.Uyeler.Add(u);
            db.SaveChanges();
            return u.uyeID;
        }

        public bool soruekle(string soruBasligi, string soruIcerigi, int soruSahibi, DateTime soruTarihi, int favori)
        {
            Sorular soru = new Sorular();
            soru.soruBasligi = soruBasligi;
            soru.soruIcerigi = soruIcerigi;
            soru.soruSahibi = soruSahibi;
            soru.soruTarihi = DateTime.Now;
            soru.favori = favori;

            db.Sorular.Add(soru);
            db.SaveChanges();
            return true;
        }


        public Soru teksorucek(int soruID, int uyeId)
        {
            Sorular teksoru;
            teksoru = db.Sorular.FirstOrDefault(x => x.soruID == soruID);
            Soru sr = new Soru();

            sr.soruBasligi = teksoru.soruBasligi;
            sr.soruIcerigi = teksoru.soruIcerigi;
            sr.soruSahibi = db.Uyeler.FirstOrDefault(x => x.uyeID == teksoru.Uyeler.uyeID).kuladi;
            sr.soruSahibiID = db.Uyeler.FirstOrDefault(x => x.uyeID == teksoru.Uyeler.uyeID).uyeID;
            sr.soruTarihi = Convert.ToDateTime(teksoru.soruTarihi);
            sr.soruID = teksoru.soruID;
            sr.favori = db.FavoriSoru.Any(x => x.uyeID == uyeId && x.soruID == soruID);


            return sr;
        }



        public List<Soru> tumsorularıcek(int uyeID)
        {
            List<Soru> tsorular = new List<Soru>();

            foreach (Sorular item in db.Sorular)
            {
                Soru soru = new Soru();
                soru.soruBasligi = item.soruBasligi;
                soru.soruIcerigi = item.soruIcerigi;
                soru.soruSahibi = db.Uyeler.FirstOrDefault(x => x.uyeID == item.Uyeler.uyeID).kuladi;
                soru.soruSahibiID = db.Uyeler.FirstOrDefault(x => x.uyeID == item.Uyeler.uyeID).uyeID;
                soru.soruSahibiResim = db.Uyeler.FirstOrDefault(x => x.uyeID == item.Uyeler.uyeID).kullaniciresmi;
                soru.soruTarihi = Convert.ToDateTime(item.soruTarihi);
                soru.soruID = item.soruID;
                soru.favori = db.FavoriSoru.Any(x => x.uyeID == uyeID && x.soruID == item.soruID);
                tsorular.Add(soru);
            }        
            return tsorular.ToList();
        }
        public Uye uyebilgileri(int uyeID)
        {
            List<string> yeti = new List<string>();
            Uyeler uyebilgisi = db.Uyeler.FirstOrDefault(x => x.uyeID == uyeID);
            Uye uye = new Uye();
            uye.adi = uyebilgisi.adi;
            uye.eposta = uyebilgisi.eposta;
            uye.kayittarihi = Convert.ToDateTime(uyebilgisi.kayittarihi);
            uye.kuladi = uyebilgisi.kuladi;
            uye.kullaniciresmi = uyebilgisi.kullaniciresmi;
            uye.sifre = uyebilgisi.sifre;
            uye.soyadi = uyebilgisi.soyadi;
            uye.UyeID = uyebilgisi.uyeID;
            uye.meslek = uyebilgisi.Meslek;
            uye.egitim = uyebilgisi.Egitim;
            uye.yetenek = uyebilgisi.Yetenekler;
            uye.konum = uyebilgisi.Konum;
            uye.not = uyebilgisi.Notu;

            return uye;
        }

        public bool sorusil(int soruid)
        {
            var silineceksoru = db.Sorular.Where(x => x.soruID == soruid).FirstOrDefault();
            db.Sorular.Remove(silineceksoru);
            db.SaveChanges();
            return true;
        }

        public bool soruguncelle(int soruid, string sorubaslik, string soruicerik)
        {
            var guncelleniceksoru = db.Sorular.Where(x => x.soruID == soruid).FirstOrDefault();
            guncelleniceksoru.soruBasligi = sorubaslik;
            guncelleniceksoru.soruIcerigi = soruicerik;
            guncelleniceksoru.soruTarihi = Convert.ToDateTime(DateTime.Now);
            db.SaveChanges();
            return true;
        }
        public bool sorufavguncelle(int uyeID, int soruID)
        {
            if (db.FavoriSoru.Any(x => x.uyeID == uyeID && x.soruID == soruID))
            {
                FavoriSoru fs = db.FavoriSoru.FirstOrDefault(x => x.uyeID == uyeID && x.soruID == soruID);
                db.FavoriSoru.Remove(fs);
                db.SaveChanges();
                return false;
            }
            else
            {
                FavoriSoru fs = new FavoriSoru();
                fs.soruID = soruID;
                fs.uyeID = uyeID;
                db.FavoriSoru.Add(fs);
                db.SaveChanges();

                return true;
            }
        }


        public bool cevapsil(int cevapid)
        {
            var silinecekcevap = db.Cevaplar.Where(x => x.cevapID == cevapid).FirstOrDefault();
            db.Cevaplar.Remove(silinecekcevap);
            db.SaveChanges();
            return true;
        }

        public bool cevapfavguncelle(int uyeID, int cevapID)
        {

            if (db.FavoriCevap.Any(x => x.uyeID == uyeID && x.cevapID == cevapID))
            {
                FavoriCevap fc = db.FavoriCevap.FirstOrDefault(x => x.uyeID == uyeID && x.cevapID == cevapID);
                db.FavoriCevap.Remove(fc);

                db.SaveChanges();
                return false;
            }
            else
            {
                FavoriCevap fc = new FavoriCevap();
                fc.cevapID = cevapID;
                fc.uyeID = uyeID;
                db.FavoriCevap.Add(fc);
                db.SaveChanges();

                return true;
            }
        }
        public bool yararlicevap(int cevapID)
        {
            Cevaplar ycf = db.Cevaplar.FirstOrDefault(x => x.cevapID == cevapID);
            if (db.Cevaplar.FirstOrDefault(x => x.cevapID == cevapID).faydali == 0)
            {
                foreach (var item in db.Cevaplar.Where(x => x.soruNo == ycf.soruNo))
                {
                    item.faydali = 0;
                }
                db.Cevaplar.FirstOrDefault(x => x.cevapID == cevapID).faydali = 1;


                db.SaveChanges();
                return false;

            }
            else
            {
                foreach (var item in db.Cevaplar.Where(x => x.soruNo == ycf.soruNo))
                {
                    item.faydali = 0;
                }
                db.SaveChanges();
                return true;
            }
        }
        public List<Soru> tumfavorisorularıcek(int uyeID)
        {
            List<Soru> tfsorular = new List<Soru>();

            foreach (FavoriSoru item in db.FavoriSoru.Where(x => x.uyeID == uyeID))
            {

                Soru soru = new Soru();
                Sorular s = db.Sorular.FirstOrDefault(x => x.soruID == item.soruID && uyeID == item.uyeID);
                soru.soruBasligi = db.Sorular.FirstOrDefault(x => x.soruID == item.soruID && uyeID == item.uyeID).soruBasligi;
                soru.soruIcerigi = db.Sorular.FirstOrDefault(x => x.soruID == item.soruID && uyeID == item.uyeID).soruIcerigi;
                soru.soruSahibi = db.Uyeler.FirstOrDefault(x => x.uyeID == s.Uyeler.uyeID).kuladi;
                soru.soruSahibiID = db.Uyeler.FirstOrDefault(x => x.uyeID == s.Uyeler.uyeID).uyeID;
                soru.soruSahibiResim = db.Uyeler.FirstOrDefault(x => x.uyeID == s.Uyeler.uyeID).kullaniciresmi;
                soru.soruTarihi = Convert.ToDateTime(db.Sorular.FirstOrDefault(x => x.soruID == item.soruID && uyeID == item.uyeID).soruTarihi);
                soru.soruID = db.Sorular.FirstOrDefault(x => x.soruID == item.soruID && uyeID == item.uyeID).soruID;
                tfsorular.Add(soru);
            }
            return tfsorular.ToList();
        }
        public List<Cevap> tumfavoricevaplaricek(int uyeID)
        {
            List<Cevap> tfcevaplar = new List<Cevap>();

            foreach (FavoriCevap item in db.FavoriCevap.Where(x => x.uyeID == uyeID))
            {

                Cevap cevap = new Cevap();
                Cevaplar c = item.Cevaplar;
                cevap.Icerik = c.cevapIcerigi;
                cevap.cevapSahibi = c.Uyeler.kuladi;
                cevap.cevapSahibiID = c.Uyeler.uyeID;
                cevap.cevapciresmi = c.Uyeler.kullaniciresmi;
                cevap.cevapTarihi = c.cevapTarihi.Value;
                cevap.soruNo = c.soruNo;
                tfcevaplar.Add(cevap);
            }
            return tfcevaplar.ToList();
        }
        public int kulSoruSayisi(int uyeID)
        {
            var kulSoruSayisi = db.Sorular.Count(x => x.soruSahibi == uyeID);
            return kulSoruSayisi;
        }

        public int kulCevapSayisi(int uyeID)
        {
            var kulCevapSayisi = db.Cevaplar.Count(x => x.cevapSahibi == uyeID);
            return kulCevapSayisi;
        }

        public int kulFavSoruSayisi(int uyeID)
        {
            var kulFavSoruSayisi = db.FavoriSoru.Count(x => x.uyeID == uyeID);
            return kulFavSoruSayisi;
        }
        public int kulFavCevapSayisi(int uyeID)
        {
            var kulFavCevapSayisi = db.FavoriCevap.Count(x => x.uyeID == uyeID);
            return kulFavCevapSayisi;
        }

        public bool profilGuncelle(Uye uye)
        {
            //KullaniciYetenek ky = new KullaniciYetenek();
            //Yetenekler yy = new Yetenekler();
            //List<string> yet = uye.yetenek;
            var guncellenicekuye = db.Uyeler.Where(x => x.uyeID == uye.UyeID).FirstOrDefault();

            if (uye.adi != "") { guncellenicekuye.adi = uye.adi; };
            if (uye.egitim != "") { guncellenicekuye.Egitim = uye.egitim; };
            if (uye.eposta != "") { guncellenicekuye.eposta = uye.eposta; };
            if (uye.konum != "") { guncellenicekuye.Konum = uye.konum; };
            if (uye.kuladi != "") { guncellenicekuye.kuladi = uye.kuladi; };
            if (uye.kullaniciresmi != "") { guncellenicekuye.kullaniciresmi = uye.kullaniciresmi; };
            if (uye.meslek != "") { guncellenicekuye.Meslek = uye.meslek; };
            if (uye.not != "") { guncellenicekuye.Notu = uye.not; };
            if (uye.sifre != "") { guncellenicekuye.sifre = uye.sifre; };
            if (uye.soyadi != "") { guncellenicekuye.soyadi = uye.soyadi; };
            if (uye.yetenek != "") { guncellenicekuye.Yetenekler = uye.yetenek; };
            db.SaveChanges();
            return true;
        }

        public List<Uye> tumuyelericek()
        {
            List<Uye> tuyeler = new List<Uye>();

            foreach (Uyeler item in db.Uyeler)
            {
                Uye uye = new Uye();
                uye.adi = item.adi;
                uye.egitim = item.Egitim;
                uye.eposta = item.eposta;
                uye.kayittarihi = Convert.ToDateTime(item.kayittarihi);
                uye.konum = item.Konum;
                uye.kuladi = item.kuladi;
                uye.kullaniciresmi = item.kullaniciresmi;
                uye.meslek= item.Meslek;
                uye.not = item.Notu;
                uye.soyadi = item.soyadi;
                uye.UyeID = item.uyeID;
                uye.yetenek = item.Yetenekler;

                tuyeler.Add(uye);
            }
            return tuyeler.ToList();
        }

    }
}