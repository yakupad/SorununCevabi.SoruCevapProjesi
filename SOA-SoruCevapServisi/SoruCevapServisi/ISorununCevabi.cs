using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SoruCevapServisi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISorununCevabi" in both code and config file together.
    [ServiceContract]
    public interface ISorununCevabi
    {
        [OperationContract]
        int giris(string kullaniciadi, string sifre);

        [OperationContract]
        int kayit(string kullaniciadi, string ad, string soyad, string sifre, string eposta, string kullaniciresmi, string meslek);

        [OperationContract]
        bool soruekle(string soruBasligi, string soruIcerigi, int soruSahibi, DateTime soruTarihi, int favori);

        [OperationContract]
        bool cevapekle(string cevapBasligi, string cevapİcerigi, int cevapSahibi, int soruNo, int yorumID);

        [OperationContract]
        bool cevapfavekle(int uyeID, int cevapID);

        [OperationContract]
        List<Cevap> cevapcek(int soruID, int uyeID);

        [OperationContract]
        Soru teksorucek(int soruID, int uyeId);

        [OperationContract]
        List<Soru> tumsorularıcek(int uyeID);

        [OperationContract]
        Uye uyebilgileri(int uyeID);

        [OperationContract]
        bool sorusil(int soruid);

        [OperationContract]
        bool soruguncelle(int soruid, string sorubaslik, string soruicerik);

        [OperationContract]
        bool cevapsil(int cevapid);

        [OperationContract]
        bool sorufavguncelle(int uyeID, int soruID);

        [OperationContract]
        bool cevapfavguncelle(int uyeID, int soruID);

        [OperationContract]
        bool yararlicevap(int cevapID);

        [OperationContract]
        List<Soru> tumfavorisorularıcek(int uyeID);

        [OperationContract]
        List<Cevap> tumfavoricevaplaricek(int uyeID);

        [OperationContract]
        int kulSoruSayisi(int uyeID);

        [OperationContract]
        int kulCevapSayisi(int uyeID);

        [OperationContract]
        int kulFavSoruSayisi(int uyeID);

        [OperationContract]
        int kulFavCevapSayisi(int uyeID);

        [OperationContract]
        bool profilGuncelle(Uye uye);

        [OperationContract]
        List<Uye> tumuyelericek();


    }
}
