using System.ComponentModel.DataAnnotations;

namespace AppIT___InterviewTask.Models
{
    public class Partner
    {
        public Partner()
        {
        }
        public Partner(Partner record)
        {
            this.PartnerID = record.PartnerID;
            this.PartnerName = record.PartnerName;
            this.ParentPartner = record.ParentPartner;
            this.FeePercent = record.FeePercent;
        }

        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public int ParentPartner { get; set; }
        [Range(0, 20)]
        public decimal FeePercent { get; set; }
    }
}
