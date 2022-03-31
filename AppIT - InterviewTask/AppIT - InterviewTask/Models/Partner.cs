using System.ComponentModel.DataAnnotations;

namespace AppIT___InterviewTask.Models
{
    public class Partner
    {
        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public int ParentPartner { get; set; }
        [Range(0, 20)]
        public decimal FeePercent { get; set; }
    }
}
