//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPlanningAPI.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Couple
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Couple()
        {
            this.Wedding = new HashSet<Wedding>();
        }
    
        public int CoupleId { get; set; }
        public string SharedEmail { get; set; }
        public string Usr { get; set; }
        public string Password { get; set; }
        public int WPlannerId { get; set; }
        public string Address { get; set; }
        public string LastNameIdentifier { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
    
        public virtual WPlanner WPlanner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wedding> Wedding { get; set; }
    }
}
