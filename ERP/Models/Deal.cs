//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Deal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Deal()
        {
            this.Books = new HashSet<Book>();
        }
    
        public int Id { get; set; }
        public string Status { get; set; }
        public Nullable<int> Cost { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Profit { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public Nullable<System.DateTime> Updatetime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}