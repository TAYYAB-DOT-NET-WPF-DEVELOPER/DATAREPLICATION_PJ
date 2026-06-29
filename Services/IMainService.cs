using DataIntegration.Models;
using POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Services
{
    public interface IMainService
    {
        Task SaveDayInfo(Dayinfo dayinfo);
        Task DeletePunchclock(int uniquied, int? snum);
        Task DeletePunchPayRoll(int uniquied, int? snum);
        Task DeleteDayinfo(string opendate, int? snum);
        Task SavePosdetail(Posdetail posdetail);
        Task SavePoshdelivery(PoshDelivery poshDelivery);
        Task SaveProduct(Product Products);
        Task SaveMethodPay(MethodPay Products);
        Task SavePromo(Promo Products);
        Task SaveHowpaidaid(HowPaid howPaid);
        Task SavePosheader(Posheader posheader);
        Task SavePosbank(Posbank posbank);
        Task SavePunchpayroll(Punchpayroll punchpayroll);
        Task SaveStocktakedetail(Stocktakedetail storetakedetail);
        Task SaveEmployee(Employee employee);
        Task SaveMember(Member member);
        Task SaveSalestype(Salestype salestype);
        Task SavePunchclock(Punchclock punchclock);
        Task SaveEmpdepartment(Empdepartment empdepartment);
        Task SaveJobpos(Jobpo jobpos);
        Task DeleteEmployee(int Empnum);     
        Task DeletePosbank(Posbank posbank);
    }
}
