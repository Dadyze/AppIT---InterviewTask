using AppIT___InterviewTask.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AppIT___InterviewTask.Controllers
{
    public class ListController : Controller
    {

        

        private List<CalculationDTO> GenerateStats()
        {

            List<CalculationDTO> lista = new List<CalculationDTO>();
            var query = (from LL in InMemoryDB._list.ToList()
                         join PL in InMemoryDB._partnerList.ToList()
                             on LL.PartnerID equals PL.PartnerID
                         select new { LL.Amount, PL.PartnerName, PL.FeePercent, PL.ParentPartner });

            foreach (var item in query)
            {
                CalculationDTO novi = new CalculationDTO();
                novi.Amount = item.Amount;
                novi.PartnerName = item.PartnerName;
                novi.FeePercent = item.FeePercent;
                if(item.ParentPartner == 0)
                {
                    novi.ParentPartner = "glavni";
                } else
                {
                    novi.ParentPartner = InMemoryDB._partnerList.FirstOrDefault(x => x.PartnerID == item.ParentPartner).PartnerName;
                }

                lista.Add(novi);
            }

            return lista;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return View(InMemoryDB._list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View(GenerateStats());
        }
        [HttpGet]
        public IActionResult Update(int SelectedID)
        {
            
                var model = InMemoryDB._list.Where(x => x.PartnerID == SelectedID).SingleOrDefault();
                return View(model);
            
        }

    
        [HttpPost]
        public IActionResult Create(DateTime Date, decimal Amount)
        {
            var FI = new FinancialItem();
            FI.PartnerID = InMemoryDB._list.Count() + 1;
            FI.Date = Date;
            FI.Amount = Amount;
            InMemoryDB._list.Add(FI);
            return View("GetList", InMemoryDB._list);
        }

        [HttpPost]
        public IActionResult Delete(int SelectedID)
        {
            var s = InMemoryDB._list.FirstOrDefault(x =>  x.PartnerID == SelectedID );
            if (s != null)
            {
                InMemoryDB._list.Remove(s);
                return View("GetList", InMemoryDB._list);
            }
            else {
                return View();

            }

        }

        [HttpPost]
        public ActionResult Update(int SelectedID, FinancialItem model)
        {
            var data = InMemoryDB._list.FirstOrDefault(x => x.PartnerID == SelectedID);

            if (data != null) 
            {
                InMemoryDB._list.FirstOrDefault(x => x.PartnerID == SelectedID).Date = model.Date;
                InMemoryDB._list.FirstOrDefault(x => x.PartnerID == SelectedID).Amount = model.Amount;
             
                return View("Getlist", InMemoryDB._list);
            }

            return View();
         }


        [HttpPost]
        public IActionResult Calculation()
        {
            return View(GenerateStats());


        }


        [HttpPost]
        public IActionResult CalculationFee()
        {
            List<CalculationFeeDTO> calculationFeeList = new List<CalculationFeeDTO>();

            foreach (Partner partner in InMemoryDB._partnerList)
            {
                decimal teamShoppingAmount = GetTeamShoppingAmount(partner.PartnerID);
                decimal personalShoppingAmount = GetTotalAmountForPartnerID(partner.PartnerID);

                CalculationFeeDTO calculationFeeDTO = new CalculationFeeDTO();
                calculationFeeDTO.PartnerName = partner.PartnerName;
                calculationFeeDTO.TeamShoppingAmount = teamShoppingAmount;
                calculationFeeDTO.TotalShoppingAmount = teamShoppingAmount + personalShoppingAmount;
                calculationFeeDTO.PersonalBonus = personalShoppingAmount * partner.FeePercent / 100;
                calculationFeeDTO.TeamBonus = GetTeamBonusByID(partner.PartnerID, partner.FeePercent);
                calculationFeeList.Add(calculationFeeDTO);
            }


            return View(calculationFeeList);
        }

        private decimal GetTeamBonusByID(int partnerID, decimal PersonalBonus)
        {
            decimal totalAmount = 0;
            var childrenPartners = (from PL in InMemoryDB._partnerList.ToList()
                                    where (PL.ParentPartner == partnerID && PL.FeePercent < PersonalBonus)
                                    select new Partner(PL));

            foreach (var partner in childrenPartners)
            {
                decimal totalAmountChild = (GetTotalAmountForPartnerID(partner.PartnerID) + GetTeamShoppingAmount(partner.PartnerID));
                totalAmount += totalAmountChild * (PersonalBonus - partner.FeePercent) / 100;  
            }

            return totalAmount;
        }

        private decimal GetTeamShoppingAmount(int partnerID)
        {
            var childrenPartners = (from PL in InMemoryDB._partnerList.ToList()
                         where PL.ParentPartner == partnerID
                         select new Partner(PL));

            if (childrenPartners.Count() == 0)
            {
                return 0;
            }

            decimal total = 0;
            foreach (var partner in childrenPartners)
            {
                total += GetTotalAmountForPartnerID(partnerID) + GetTeamShoppingAmount(partner.PartnerID);
            }

            return total;
        }

        private decimal GetTotalAmountForPartnerID(int partnerID)
        {
            var financialItems = (from LL in InMemoryDB._list.ToList()
                                  where LL.PartnerID == partnerID
                                  select new FinancialItem(LL));

            decimal total = 0;
            foreach (var item in financialItems)
            {
                total += item.Amount;
            }

            return total;
        }
    }
}
