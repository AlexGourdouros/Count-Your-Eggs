using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test30.Models
{
    public partial class Invoice
    {
       
        public void UpdateTotalPaid()
        {
           TotalPaid = (PayBridges.Sum(b => (decimal?)b.Payment)) ?? 0;
        }
        public void UpdateTotalDue()
        {
            TotalDue = (InvoiceItems.Sum(b => (decimal?)b.ExtendedCost) - TotalPaid) ?? 0;

        }
        public void UpdateTotal()
        {
            Total = (InvoiceItems.Sum(b => (decimal?)b.ExtendedCost)) ?? 0;
        }
        public void SetStatusID()
        {
            if (TotalDue < 0)
            {
                statusID = 2;
            }
            else if (TotalDue > 0)
            {
                statusID = 1;
            }
            else
            {
                statusID = 1;
            }
           

        }

    }
  
}