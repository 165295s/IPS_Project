using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Prototype.RetrieveClass
{
    public class OrgInfo
    {
        public string id { get; set; }
        public string orgName { get; set; }
        public string mailLine1 { get; set; }
        public string mailLine2 { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string telNo { get; set; }
        public string officeNo { get; set; }
        public string websiteURL { get; set; }
        public string busDesc { get; set; }
        public string PoC { get; set; }
        public string notes { get; set; }
        public string uen { get; set; }


        public OrgInfo() { }
    }
}