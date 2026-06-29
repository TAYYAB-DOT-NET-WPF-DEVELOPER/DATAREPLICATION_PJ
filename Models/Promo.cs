using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Models
{
    public class Promo
    {
        public int PromoNum { get; set; }
        public string Descript { get; set; }
        public double? Amount { get; set; }
        public short? SecLevel { get; set; }
        public short? IsActive { get; set; }
        public double? Percent { get; set; }
        public short? IsManual { get; set; }
        public short? Tax1 { get; set; }
        public short? Tax2 { get; set; }
        public short? Tax3 { get; set; }
        public short? Tax4 { get; set; }
        public short? Tax5 { get; set; }
        public short? MemberOnly { get; set; }
        public double? AmountB { get; set; }
        public double? AmountC { get; set; }
        public double? PercentB { get; set; }
        public double? PercentC { get; set; }
        public DateTime? RangeStart { get; set; }
        public DateTime? RangeEnd { get; set; }
        public int? ProdNum { get; set; }
        public short? IsAutoProd { get; set; }
    }


}
