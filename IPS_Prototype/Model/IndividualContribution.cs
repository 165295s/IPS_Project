using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Prototype.Model
{
    public class IndividualContribution
    {
       public decimal Amoount{get;set;}
        public DateTime PaymentReceivedDate{get;set;}
        public DateTime CreatedDate{get;set;}
        public string PaymentDetails{get;set;}
        public string PaymentMode{get;set;}
        public string PaymentPurpose{get;set;}
        public decimal TotalAmount{get;set;}
        public DateTime ContributionDate{get;set;}
        public DateTime ContributionCreatedDate { get; set; }
        public int MemberId { get; set; }
        public int PersonId { get; set; }
        public int OrgId { get; set; }
        public string DonorTier { get; set; }
        public string ExpiryDate { get; set; }
        public string ContributionId { get; set; }
        public string Status { get; set; }
    }
}