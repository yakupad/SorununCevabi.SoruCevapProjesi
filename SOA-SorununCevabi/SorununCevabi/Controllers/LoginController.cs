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
    public class LoginController : Controller
    {
        SorununCevabiClient sc = new SorununCevabiClient();

        // GET: Login

        public JsonResult GirisYap(string eposta, string parola)
        {

            if ((eposta == "") || (parola == ""))
            {
                var jsonModel = new
                {
                    basari = -1
                };
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int id = sc.giris(eposta, parola);
                Uye suankiuye = sc.uyebilgileri(id);


                Session.Add("id",suankiuye.UyeID);
                Session["kullaniciadi"] = suankiuye.kuladi;
                Session["adi"] = suankiuye.adi;
                Session["soyadi"] = suankiuye.soyadi;
                Session["eposta"] = suankiuye.eposta;
                Session["kayittarihi"] = suankiuye.kayittarihi;
                Session["kullaniciresmi"] = suankiuye.kullaniciresmi;
                Session["meslek"] = suankiuye.meslek;
                Session["uyebilgileri"] = sc.uyebilgileri((int)Session["id"]);
                Session["kulfavsorusayisi"] = sc.kulFavSoruSayisi((int)Session["id"]);
                Session["kulfavcevapsayisi"] = sc.kulFavCevapSayisi((int)Session["id"]);
                Session["kulsorusayisi"] = sc.kulSoruSayisi((int)Session["id"]);
                Session["kulcevapsayisi"] = sc.kulCevapSayisi((int)Session["id"]);
                var jsonModel = new
                {
                    basari = Session["id"]
                };
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Login()
        {


            return View();
        }
        public JsonResult KayitOl(string ad, string soyad, string kullaniciadi, string eposta, string parola, string parolatekrari, HttpPostedFileWrapper kullaniciresmi, string meslek)
        {
            string resimyolu;
            if (kullaniciresmi.ToString() == "")
            {
                resimyolu = "../Images/yzmbiz.jpg";
            }
            else
            {
                var img = Image.FromStream(kullaniciresmi.InputStream);
                resimyolu = "/Images/" + System.IO.Path.GetRandomFileName() + ".jpg";
                img.Save(Server.MapPath(resimyolu), ImageFormat.Jpeg);
            }





            //unicidüretme


            int id = sc.kayit(kullaniciadi, ad, soyad, parola, eposta, resimyolu, meslek);
            Session["id"] = id;

            var jsonModel = new
            {
                basari = 1
            };
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LockScreen()
        {
            return View();
        }
    }
}