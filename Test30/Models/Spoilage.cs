//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test30.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Spoilage
    {
        public int SpoilageID { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Spoiled")]
        public System.DateTime DateSpoiled { get; set; }
        [Display(Name = "Reason")]

        public string Reason { get; set; }
        [Display(Name = "Quantity")]

        public int Quantity { get; set; }
        [Display(Name = "Item")]

        public int ItemID { get; set; }
        [Display(Name = "User")]

        public int UserID { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual User User { get; set; }
    }
}