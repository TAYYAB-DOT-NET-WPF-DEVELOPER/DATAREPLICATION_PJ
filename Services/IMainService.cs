using DataIntegration.Models;
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
        Task DeleteDayinfo(string opendate, int? snum);
        Task SavePosdetail(Posdetail posdetail);
        Task SavePosheader(Posheader posheader);
    }
}
