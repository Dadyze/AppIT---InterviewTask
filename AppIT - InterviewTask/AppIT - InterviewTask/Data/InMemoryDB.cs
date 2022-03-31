using AppIT___InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppIT___InterviewTask
{
    static class InMemoryDB
    {
        public static List<FinancialItem> _list;
        public static List<Partner> _partnerList;

        static InMemoryDB()
        {
            _list = new List<FinancialItem>();
            _list.Add(new FinancialItem { PartnerID = 1, Date = DateTime.Now, Amount = 12321 });
            _list.Add(new FinancialItem { PartnerID = 2, Date = DateTime.Now, Amount = 123122 });
            _list.Add(new FinancialItem { PartnerID = 3, Date = DateTime.Now, Amount = 123123 });
            _list.Add(new FinancialItem { PartnerID = 4, Date = DateTime.Now, Amount = 4234234 });
            _list.Add(new FinancialItem { PartnerID = 5, Date = DateTime.Now, Amount = 1241231 });
            _list.Add(new FinancialItem { PartnerID = 6, Date = DateTime.Now, Amount = 123123123 });

            _partnerList = new List<Partner>();
            _partnerList.Add(new Partner { PartnerID = 1, ParentPartner = 10, PartnerName = "John", FeePercent = 10 });
            _partnerList.Add(new Partner { PartnerID = 2, ParentPartner = 10, PartnerName = "Jack", FeePercent = 11 });
            _partnerList.Add(new Partner { PartnerID = 3, ParentPartner = 10, PartnerName = "Milton", FeePercent = 12 });
            _partnerList.Add(new Partner { PartnerID = 4, ParentPartner = 9, PartnerName = "Howard", FeePercent = 13 });
            _partnerList.Add(new Partner { PartnerID = 5, ParentPartner = 9, PartnerName = "Andrew", FeePercent = 14 });
            _partnerList.Add(new Partner { PartnerID = 6, ParentPartner = 9, PartnerName = "Steve", FeePercent = 15 });
            _partnerList.Add(new Partner { PartnerID = 7, ParentPartner = 8, PartnerName = "David", FeePercent = 16 });
            _partnerList.Add(new Partner { PartnerID = 8, ParentPartner = 9, PartnerName = "Bill", FeePercent = 17 });
            _partnerList.Add(new Partner { PartnerID = 9, ParentPartner = 10, PartnerName = "Donald", FeePercent = 18 });
            _partnerList.Add(new Partner { PartnerID = 10, ParentPartner = 0, PartnerName = "Dwayne", FeePercent = 19 });
        }

    }
    
}
