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
    
    public partial class FavoriCevap
    {
        public int id { get; set; }
        public Nullable<int> uyeID { get; set; }
        public Nullable<int> cevapID { get; set; }
    
        public virtual Cevaplar Cevaplar { get; set; }
        public virtual Uyeler Uyeler { get; set; }
    }
}
