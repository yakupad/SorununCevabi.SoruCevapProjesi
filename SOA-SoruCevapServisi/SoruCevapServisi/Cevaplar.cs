//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoruCevapServisi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cevaplar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cevaplar()
        {
            this.Cevaplar1 = new HashSet<Cevaplar>();
            this.FavoriCevap = new HashSet<FavoriCevap>();
        }
    
        public int cevapID { get; set; }
        public string cevapBasligi { get; set; }
        public string cevapIcerigi { get; set; }
        public Nullable<int> cevapSahibi { get; set; }
        public int soruNo { get; set; }
        public Nullable<System.DateTime> cevapTarihi { get; set; }
        public Nullable<int> yorumID { get; set; }
        public Nullable<int> faydali { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cevaplar> Cevaplar1 { get; set; }
        public virtual Cevaplar Cevaplar2 { get; set; }
        public virtual Sorular Sorular { get; set; }
        public virtual Uyeler Uyeler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FavoriCevap> FavoriCevap { get; set; }
    }
}
