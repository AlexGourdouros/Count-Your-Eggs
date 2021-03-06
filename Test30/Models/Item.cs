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

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.InvoiceItems = new HashSet<InvoiceItem>();
            this.Spoilages = new HashSet<Spoilage>();
            this.UsedItems = new HashSet<UsedItem>();
        }
        [Display(Name = "Item")]

        public int ItemID { get; set; }
        [Display(Name = "Name")]

        public string Name { get; set; }

        // public int Quantity { get; set; }
        // Stack Overflow exception thrown, maybe needs try / catch?
        /* public int _Quantity
         {
             get
             {
                 return
                  (Spoilages.Sum(b => (int?)b.Quantity)) ?? 0;

             }
             set { }
         }
         */
        //  public Nullable<int> _Quantity { get  {return Spoilages.Sum(b => (int?)b.Quantity) ?? 0; } set { } }
        /* public int _Quantity
         {
             get
             {
                 return
                  (Spoilages.Sum(b => (int?)b.Quantity)) ?? 0;

             }
             set {  }
         }
         */

       /* private int _Quantity
        {
            get
            {
                return
                 (Spoilages.Sum(b => b.Quantity));

            }
            set { }
        }
        */
        [Display(Name = "Quantity")]
        // public int? Quantity { get { return Quantity; } set { Quantity = Quantity - _Quantity; } }

        // public int Quantity { get; set; }
        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity - (Spoilages.Sum(b => (int?)b.Quantity)) - (UsedItems.Sum(b => (int?)b.Quantity)) ?? 0;
            }
            set
            {
                if (_quantity < 0)
                {
                    _quantity = 0;
                }
                else
                {
                   _quantity = value ;
                }
            }
        }

        [Display(Name = "User")]

        public int UserID { get; set; }
        [Column(TypeName = "money")]
        [Display(Name = "Retail Price")]
        public decimal Price { get; set; }
        [Column(TypeName = "money")]
        [Display(Name = "Total")]
        public decimal Total { get { return Quantity * Price; }set { } }
        // public decimal Total { get { return Quantity * Price; } set { } }
        /*  public Nullable<decimal> Total
          {
              get
              {
                  return
                   (Price * Quantity - Spoilages.Sum(b => (int?)b.Quantity) - (UsedItems.Sum(b => (int?)b.Quantity))) ?? 0;

              }
              set { }
          }
          */
        [Display(Name = "Category")]

        public int CategoryID { get; set; }
        // public int statusID { get; set; }
        [Display(Name = "Status")]

        public int statusID
        {
            get
            {
                if (Quantity <= 0)
                {
                    return statusID = 2;
                }
                else { return 1; }

            }

            set { }

        }

        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Spoilage> Spoilages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsedItem> UsedItems { get; set; }
        public virtual User User { get; set; }
        public virtual Status Status { get; set; }
    }
}
