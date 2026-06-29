using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Models
{
    public class HowPaid
    {
        public decimal HowPaidLink { get; set; }
        public DateTime? TransDate { get; set; }
        public decimal? EmpNum { get; set; }
        public float? Tender { get; set; }
        public decimal? MethodNum { get; set; }
        public float? Change { get; set; }
        public decimal? Authorized { get; set; }
        public string AuthCode { get; set; }
        public decimal? MemCode { get; set; }
        public float? ExchangeRate { get; set; }
        public decimal Transact { get; set; }
        public decimal? PayType { get; set; }
        public DateTime? OpenDate { get; set; }
        public decimal? PunchIndex { get; set; }
        public decimal UpdateStatus { get; set; }
        public decimal? Settled { get; set; }
        public decimal? Status { get; set; }
        public decimal? Approved { get; set; }
        public decimal? StatNum { get; set; }
        public decimal? IsPayInOut { get; set; }
        public string PayReason { get; set; }
        public decimal? MealTime { get; set; }
        public decimal? RevCenter { get; set; }
        public decimal? Voided { get; set; }
        public decimal? VoidedLink { get; set; }
        public float? LcuDiff { get; set; }
        public decimal? EnforcedGrat { get; set; }
        public float? GratAmount { get; set; }
        public decimal? OrigMethodNum { get; set; }
        public string CardType { get; set; }
        public decimal SNum { get; set; }
        //public float? Surcharge { get; set; }
    }
}
