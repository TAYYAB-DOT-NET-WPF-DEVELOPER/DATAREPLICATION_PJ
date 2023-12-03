using DataIntegration.Models;
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
            _dbContext.AddAsync(dayinfo);
            await _dbContext.SaveChangesAsync();
        }

    }
}