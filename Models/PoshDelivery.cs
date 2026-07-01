using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration.Models
{
    public class PoshDelivery
    {
        public int Transact { get; set; }
        public int? EmpNum { get; set; }
        public int? MemCode { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? TimeIn { get; set; }
        public int? PunchIndex { get; set; }
        public short? DeliveryStatus { get; set; }
        public short? UpdateStatus { get; set; }
        public int? SNum { get; set; }
        public string? PLink { get; set; }
        public int? TripId { get; set; }
        public string? QLink { get; set; }
        public string? Delivered { get; set; }
    }
}
