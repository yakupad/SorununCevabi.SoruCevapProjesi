using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SorununCevabi.ServiceReference;
using System.Drawing;
using System.Drawing.Imaging;

namespace SorununCevabi.Controllers
{
    public class HomeController : Controller
    {
        SorununCevabiClient sc = new SorununCevabiClient();
       

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sorular()
        {
            
                ViewBag.tumsorular = sc.tumsorularıcek(Convert.ToInt32(Session["id"])).ToList();
           

            
            //ViewBag.tumsorular = sc.tumsorularıcek();
           

            

            return View();
        }

        public ActionResult SoruEkle()
        {
            return View();
        }
        public JsonResult CevapEkle(string cevapbasligi, string cevapicerigi, int cevapsahibi, int soruno, int yorumid)
        {
            bool durum = sc.cevapekle(cevapbasligi, cevapicerigi, cevapsahibi, soruno, yorumid);
            var jsonModel = new
            {
                basari = durum
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult YeniSoruEkle(string sorubasligi, string soruicerigi)
        {
            var kid = Session["id"];
            bool durum = sc.soruekle(sorubasligi, soruicerigi, Convert.ToInt32(kid), DateTime.Now, 0);
            var jsonModel = new
            {
                basari = durum
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SoruOku(int id)
        {
            ViewBag.kullaniciid = Session["id"];
            ViewBag.secilisoru = sc.teksorucek(id, (int)Session["id"]);
            ViewBag.secilicevap = sc.cevapcek(id, (int)Session["id"]);
            return View();
        }

        public ActionResult Profil()
        {
            ViewBag.tumuyeler = sc.tumuyelericek();
            ViewBag.uyeBilgileri = sc.uyebilgileri((int)Session["id"]);
            ViewBag.kulfavorisorus = sc.kulFavSoruSayisi((int)Session["id"]);
            ViewBag.kulfavoricevaps = sc.kulFavCevapSayisi((int)Session["id"]);
            ViewBag.kulsorus = sc.kulSoruSayisi((int)Session["id"]);
            ViewBag.kulcevaps = sc.kulCevapSayisi((int)Session["id"]);
            return View();
        }
        public JsonResult SoruSil(int soruID)
        {
            bool durum = sc.sorusil(soruID);
            var jsonModel = new
            {
                basari = durum
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SoruGuncelle()
        {
            return View();
        }
        public JsonResult SoruGuncelleme(int soruID, string sorubaslik, string soruicerik)
        {
            Session["guncelleneceksoruID"] = soruID;
            Session["guncelleneceksorubaslik"] = sorubaslik;
            Session["guncelleneceksoruicerik"] = soruicerik;
            var jsonModel = new
            {
                basari = true
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SoruyuGuncelle(int soruID, string yenisorubaslik, string yenisoruicerik)
        {
            var durum = sc.soruguncelle(soruID, yenisorubaslik, yenisoruicerik);
            var jsonModel = new
            {
                basari = durum
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CevapSil(int cevapID)
        {
            var durum = sc.cevapsil(cevapID);
            var jsonModel = new
            {
                basari = durum
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SoruFavGuncelle(int soruID)
        {
            var durum = sc.sorufavguncelle(Convert.ToInt32(Session["id"]), soruID);


            var jsonModel = new
            {
                basari = durum
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CevapFavGuncelle(int cevapID)
        {
            var durum = sc.cevapfavguncelle(Convert.ToInt32(Session["id"]), cevapID);


            var jsonModel = new
            {
                basari = durum
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult YararliCevap(int cevapID)
        {
            var durum = sc.yararlicevap(cevapID);


            var jsonModel = new
            {
                basari = durum
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FavoriCevaplar()
        {
            ViewBag.tumfavoricevaplar = sc.tumfavoricevaplaricek((int)Session["id"]);
            return View();
        }
        public ActionResult FavoriSorular()
        {
            ViewBag.tumfavorisorular = sc.tumfavorisorularıcek((int)Session["id"]);
            return View();
        }

        public JsonResult ProfilGuncelle(string ad, string soyad, string kullaniciadi, string eposta, string sifre, string meslek, string egitim, string konum, string yetenekler, HttpPostedFileWrapper kullaniciresmi, string not)
        {


            string resimyolu;
            if (kullaniciresmi == null)
            {
                resimyolu = Session["kullaniciresmi"].ToString();
            }
            else
            {
                var img = Image.FromStream(kullaniciresmi.InputStream);
                resimyolu = "/Images/" + System.IO.Path.GetRandomFileName() + ".jpg";
                img.Save(Server.MapPath(resimyolu), ImageFormat.Jpeg);
            }


            Uye uye = new Uye();
            uye.UyeID = Convert.ToInt32(Session["id"]);
            uye.adi = ad;
            uye.soyadi = soyad;
            uye.kuladi = kullaniciadi;
            uye.eposta = eposta;
            uye.sifre = sifre;
            uye.meslek = meslek;
            uye.egitim = egitim;
            uye.konum = konum;
            uye.kullaniciresmi = resimyolu;
            uye.not = not;
            uye.yetenek = yetenekler;



            sc.profilGuncelle(uye);

            var jsonModel = new
            {
                basari = true
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }


    }
}