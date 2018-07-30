using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Prototype.RetrieveClass
{
    public class EventInfo
    {
        public int PersonId { get; set; }
        public int OrgId { get; set; }
        public DateTime DonationDate { get; set; }
        public decimal DonationAmt { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
    }
}