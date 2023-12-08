using DataIntegration.Models;
using System;
using System.Configuration;
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
        public async Task SaveDayInfo(Dayinfo dayinfo)
        {
            await _dbContext.AddAsync(dayinfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SavePosdetail(Posdetail posdetail)
        {
            var posdetailtocheck = _dbContext.Posdetails.FirstOrDefault(d => d.Opendate == posdetail.Opendate && d.Snum == posdetail.Snum && d.Uniqueid == posdetail.Uniqueid);
            if (posdetailtocheck is null)
            {
                await _dbContext.AddAsync(posdetail);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task SavePosheader(Posheader posheader)
        {
            var posheadertocheck = _dbContext.Posheaders.FirstOrDefault(d => d.Opendate == posheader.Opendate && d.Snum == posheader.Snum && d.Transact == posheader.Transact);
            if (posheadertocheck is null)
            {
                await _dbContext.AddAsync(posheader);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task SavePosbank(Posbank posbank)
        {
            var posbanktocheck = _dbContext.Posbanks.FirstOrDefault(d => d.Snum == posbank.Snum && d.Uniqueid == posbank.Uniqueid);
            if (posbanktocheck is null)
            {
                await _dbContext.AddAsync(posbank);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task SavePunchpayroll(Punchpayroll punchpayroll)
        {
            var punchpayrolltocheck = _dbContext.Punchpayrolls.FirstOrDefault(d => d.Storeid == punchpayroll.Storeid && d.Punchindex == punchpayroll.Punchindex);
            if (punchpayrolltocheck is null)
            {
                await _dbContext.AddAsync(punchpayroll);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task SaveStocktakedetail(Stocktakedetail stocktakedetail)
        {
            var stocktakedetailtocheck = _dbContext.Stocktakedetails.FirstOrDefault(d => d.Uniqueid == stocktakedetail.Uniqueid && d.Snum == stocktakedetail.Snum);
            if (stocktakedetailtocheck is null)
            {
                await _dbContext.AddAsync(stocktakedetail);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task SaveEmployee(Employee employee)
        {
            var employeetocheck = _dbContext.Employees.FirstOrDefault(d => d.Empnum == employee.Empnum && d.Empname == employee.Empname);
            if (employeetocheck is null)
            {
                await _dbContext.AddAsync(employee);
                await _dbContext.SaveChangesAsync();
            }
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
        public async Task DeleteEmployee(int Empnum)
        {
            var employeetodelete = _dbContext.Employees.FirstOrDefault(d => d.Empnum == Empnum);

            if (employeetodelete != null)
            {
                _dbContext.Employees.Remove(employeetodelete);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeletePosbank(Posbank posbank)
        {
            var posbanktodelete = _dbContext.Posbanks.FirstOrDefault(d => d.Opendate == posbank.Opendate && d.Snum == posbank.Snum && d.Uniqueid == posbank.Uniqueid);

            if (posbanktodelete != null)
            {
                _dbContext.Posbanks.Remove(posbanktodelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}