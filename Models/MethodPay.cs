using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Models
{
    public class MethodPay
    {
        public int? MethodNum { get; set; }
        public string Currency { get; set; }
        public string AuthReqR { get; set; }
        public short? IsActive { get; set; }
        public double? Exchange { get; set; }
        public string Descript { get; set; }
        public short? SecLevel { get; set; }
        public int? NumDecimals { get; set; }
    }
}
