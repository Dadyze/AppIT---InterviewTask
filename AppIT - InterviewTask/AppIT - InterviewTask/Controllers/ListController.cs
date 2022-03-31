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
            return View();
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
                else
                    return View();
            }

        [HttpPost]
        public IActionResult Calculation()
        {
            List<CalculationDTO> lista = new List<CalculationDTO>();
            var query = (from LL in InMemoryDB._list.ToList()
                         join PL in InMemoryDB._partnerList.ToList()
                             on LL.PartnerID equals PL.PartnerID
                         select new { LL.Amount, PL.PartnerName, PL.FeePercent , PL.ParentPartner});

            foreach (var item in query)
            {
                CalculationDTO novi = new CalculationDTO();
                novi.Amount = item.Amount;
                novi.PartnerName = item.PartnerName;
                novi.FeePercent = item.FeePercent;
                novi.ParentPartner = InMemoryDB._partnerList.SingleOrDefault( x => x.PartnerID == item.ParentPartner).PartnerName;
                lista.Add(novi);
            }
            return View(lista);
        }
    }
}
