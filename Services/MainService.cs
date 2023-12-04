using DataIntegration.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataIntegration.Services
{
    public class MainService : IMainService
    {
        private readonly OracleDbContext _dbContext;

        public MainService(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteDayinfo(string opendate, int? snum)
        {
            var dayinfoToDelete = _dbContext.Dayinfos.FirstOrDefault(d => d.Opendate == opendate && d.Snum == snum);

            if (dayinfoToDelete != null)
            {
                _dbContext.Dayinfos.Remove(dayinfoToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SaveDayInfo(Dayinfo dayinfo)
        {
            await _dbContext.AddAsync(dayinfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SavePosdetail(Posdetail posdetail)
        {
            var posdetailtocheck = _dbContext.Posdetails.FirstOrDefault(d => d.Opendate == posdetail.Opendate && d.Snum == posdetail.Snum);
            if (posdetailtocheck is null)
            {
                await _dbContext.AddAsync(posdetail);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}