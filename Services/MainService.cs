using DataIntegration.DbContexts;
using DataIntegration.Models;
using Microsoft.EntityFrameworkCore;
using POS.Models;
using Serilog;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataIntegration.Services
{
    /// <summary>
    /// Data-access service for the integration jobs.
    ///
    /// Two deliberate design choices fix the "records go missing" / "updates are
    /// ignored" problems:
    ///
    ///   1. UPSERT, not insert-only. Every Save* method inserts when the row is
    ///      new and updates the changed columns when it already exists
    ///      (see <see cref="UpsertAsync{TEntity}"/>).
    ///
    ///   2. A FRESH DbContext per operation, taken from an
    ///      <see cref="IDbContextFactory{TContext}"/>. The previous code reused a
    ///      single long-lived context for the whole app, so its change tracker
    ///      accumulated every entity ever saved and eventually threw duplicate-
    ///      tracking errors that were swallowed — silently dropping rows. A
    ///      short-lived context per call cannot accumulate and is safe to use
    ///      from the background sync timers.
    ///
    /// Errors are logged WITH the exception and context, then rethrown, so the
    /// caller can record the failure instead of losing the row without a trace.
    ///
    /// Assumes EF Core 5+ (required for IDbContextFactory). See the DI note at
    /// the bottom of this file.
    /// </summary>
    public class MainService : IMainService
    {
        private readonly IDbContextFactory<OracleDbContext> _contextFactory;

        public MainService(IDbContextFactory<OracleDbContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        // -----------------------------------------------------------------
        // Saves (insert-or-update)
        //
        // The second argument is the BUSINESS KEY used to decide whether the
        // row already exists. Verify the flagged ones against your schema.
        // -----------------------------------------------------------------

        public Task SaveDayInfo(Dayinfo dayinfo) =>
            UpsertAsync(dayinfo, x => x.Uiid == dayinfo.Uiid);

        public Task SavePosdetail(Posdetail posdetail) =>
            UpsertAsync(posdetail, x => x.Opendate == posdetail.Opendate
                                     && x.Snum == posdetail.Snum
                                     && x.Transact == posdetail.Transact
                                     && x.Uniqueid == posdetail.Uniqueid);

        public Task SavePoshdelivery(PoshDelivery poshDelivery) =>
            UpsertAsync(poshDelivery, x => x.OpenDate == poshDelivery.OpenDate
                                        && x.SNum == poshDelivery.SNum
                                        && x.Transact == poshDelivery.Transact);

        public Task SavePosheader(Posheader posheader) =>
            UpsertAsync(posheader, x => x.Opendate == posheader.Opendate
                                     && x.Snum == posheader.Snum
                                     && x.Transact == posheader.Transact);

        public Task SavePosbank(Posbank posbank) =>
            UpsertAsync(posbank, x => x.Snum == posbank.Snum && x.Uniqueid == posbank.Uniqueid);

        public Task SavePunchpayroll(Punchpayroll punchpayroll) =>
            UpsertAsync(punchpayroll, x => x.Storeid == punchpayroll.Storeid
                                        && x.Punchindex == punchpayroll.Punchindex);

        // NOTE: Save originally matched on Storeid while Delete matched on
        // Storenum. Using Storeid here to match the original Save check —
        // please confirm which column is the real unique key.
        public Task SavePunchclock(Punchclock punchclock) =>
            UpsertAsync(punchclock, x => x.Uniqueid == punchclock.Uniqueid
                                      && x.Storeid == punchclock.Storeid);

        public Task SaveMember(Member member) =>
            UpsertAsync(member, x => x.Snum == member.Snum && x.Memcode == member.Memcode);

        public Task SaveStocktakedetail(Stocktakedetail stocktakedetail) =>
            UpsertAsync(stocktakedetail, x => x.Uniqueid == stocktakedetail.Uniqueid
                                           && x.Snum == stocktakedetail.Snum);

        // NOTE: original matched on Empnum + Empname, which would create a
        // duplicate whenever a name changed. For a true upsert the key should
        // be the identity (Empnum). Confirm before deploying.
        public Task SaveEmployee(Employee employee) =>
            UpsertAsync(employee, x => x.Empnum == employee.Empnum);

        public Task SaveJobpos(Jobpo jobpos) =>
            UpsertAsync(jobpos, x => x.Jobpos == jobpos.Jobpos && x.Storeid == jobpos.Storeid);

        public Task SaveHowpaidaid(HowPaid howPaid) =>
            UpsertAsync(howPaid, x => x.HowPaidLink == howPaid.HowPaidLink && x.SNum == howPaid.SNum);

        public Task SaveSalestype(Salestype salestype) =>
            UpsertAsync(salestype, x => x.Saletypeindex == salestype.Saletypeindex
                                     && x.Snum == salestype.Snum);

        public Task SaveEmpdepartment(Empdepartment empdepartment) =>
            UpsertAsync(empdepartment, x => x.Deptnum == empdepartment.Deptnum
                                         && x.Storeid == empdepartment.Storeid);

        // The original had NO existence check for these three, so I do not know
        // their unique key. They remain insert-only. To make them upserts too,
        // give me the key column(s) and I'll switch them to UpsertAsync.
        public Task SaveProduct(Product products) => InsertAsync(products);
        public Task SavePromo(Promo products) => InsertAsync(products);
        public Task SaveMethodPay(MethodPay products) => InsertAsync(products);

        // -----------------------------------------------------------------
        // Deletes
        //
        // With upserts in place, the delete-then-save pattern in the caller is
        // now redundant for Dayinfo / Punchclock / Punchpayroll. These remain
        // for explicit removals and interface compatibility.
        // -----------------------------------------------------------------

        public Task DeleteDayinfo(DateTime? opendate, int? snum) =>
            DeleteAsync<Dayinfo>(x => x.Opendate == opendate && x.Snum == snum);

        public Task DeleteEmployee(int Empnum) =>
            DeleteAsync<Employee>(x => x.Empnum == Empnum);

        public Task DeletePosbank(Posbank posbank) =>
            DeleteAsync<Posbank>(x => x.Opendate == posbank.Opendate
                                   && x.Snum == posbank.Snum
                                   && x.Uniqueid == posbank.Uniqueid);

        // Preserved exactly as the original (matches on Storenum).
        public Task DeletePunchclock(int uniqueid, int? snum) =>
            DeleteAsync<Punchclock>(x => x.Uniqueid == uniqueid && x.Storenum == snum);

        public Task DeletePunchPayRoll(int uniqueid, int? snum) =>
            DeleteAsync<Punchpayroll>(x => x.Punchindex == uniqueid && x.Storeid == snum);

        // -----------------------------------------------------------------
        // Generic helpers
        // -----------------------------------------------------------------

        /// <summary>
        /// Inserts <paramref name="incoming"/> if no row matches
        /// <paramref name="keyPredicate"/>; otherwise copies its values onto the
        /// existing row. EF marks only the columns whose value actually changed
        /// as Modified, so unchanged columns are left untouched.
        /// </summary>
        private async Task UpsertAsync<TEntity>(
            TEntity incoming,
            Expression<Func<TEntity, bool>> keyPredicate)
            where TEntity : class
        {
            if (incoming is null) throw new ArgumentNullException(nameof(incoming));

            try
            {
                using var db = _contextFactory.CreateDbContext();
                var set = db.Set<TEntity>();

                var existing = await set.FirstOrDefaultAsync(keyPredicate).ConfigureAwait(false);

                if (existing is null)
                {
                    await set.AddAsync(incoming).ConfigureAwait(false);
                }
                else
                {
                    var entry = db.Entry(existing);
                    var primaryKey = entry.Metadata.FindPrimaryKey();

                    // Remember the real key so an unset incoming key can't overwrite it.
                    object[] originalKeyValues = primaryKey?
                        .Properties
                        .Select(p => entry.Property(p.Name).CurrentValue)
                        .ToArray();

                    // Copy every mapped scalar from incoming -> existing.
                    // Only the genuinely-changed columns get marked Modified.
                    entry.CurrentValues.SetValues(incoming);

                    if (primaryKey != null)
                    {
                        for (int i = 0; i < primaryKey.Properties.Count; i++)
                        {
                            entry.Property(primaryKey.Properties[i].Name).CurrentValue =
                                originalKeyValues[i];
                        }
                    }
                }

                await db.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Upsert failed for {Entity}.", typeof(TEntity).Name);
                throw; // surface it so the caller can record the failed row
            }
        }

        private async Task InsertAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            try
            {
                using var db = _contextFactory.CreateDbContext();
                await db.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Insert failed for {Entity}.", typeof(TEntity).Name);
                throw;
            }
        }

        private async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> match)
            where TEntity : class
        {
            try
            {
                using var db = _contextFactory.CreateDbContext();
                var entity = await db.Set<TEntity>().FirstOrDefaultAsync(match).ConfigureAwait(false);
                if (entity != null)
                {
                    db.Set<TEntity>().Remove(entity);
                    await db.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Delete failed for {Entity}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task ClearTable(string tableName)
        {
            try
            {
                using var db = _contextFactory.CreateDbContext();
                await db.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName}").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to clear table {Table}.", tableName);
                throw;
            }
        }
    }
}

/*
 ---------------------------------------------------------------------------
 DEPENDENCY-INJECTION CHANGE REQUIRED (one line)

 MainService now depends on IDbContextFactory<OracleDbContext> instead of a
 single shared OracleDbContext. In your service registration (Program.cs /
 App startup), register the factory:

     services.AddDbContextFactory<OracleDbContext>(options =>
         options.UseOracle(connectionString));   // or your provider's Use... call

 Notes:
  * Remove the captive single context if MainService (or the view-model that
    holds it) outlives a normal scope — that capture is what caused the change
    tracker to accumulate and drop rows.
  * If other services still inject OracleDbContext directly, either switch them
    to the factory too, or also keep services.AddDbContext<OracleDbContext>(...)
    alongside the factory.
  * On EF Core < 5 (no IDbContextFactory), inject IServiceScopeFactory instead
    and resolve a fresh OracleDbContext inside a new scope per operation — the
    effect is identical. I can provide that variant if needed.
 ---------------------------------------------------------------------------
*/
