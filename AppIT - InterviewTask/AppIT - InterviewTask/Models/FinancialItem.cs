using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppIT___InterviewTask.Models
{
    public class FinancialItem
    {
        public FinancialItem()
        {
        }

        public FinancialItem(FinancialItem record)
        {
            this.PartnerID = record.PartnerID;
            this.Date = record.Date;
            this.Amount = record.Amount;
        }

        public int PartnerID { get; set; }
        public DateTime Date{ get; set; }
        public decimal Amount { get; set; }
    }
}
