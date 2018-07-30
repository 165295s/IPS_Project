using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Prototype.RetrieveClass
{
    public class OrganisationModel
    {
        //Person Table
        public string orgid { get; set; }
        public string orgname { get; set; }
        public string telNo { get; set; }
        public string officeNo { get; set; }
        public string pointOfContact { get; set; }
        public string AddLine1 { get; set; }
        public string AddLine2 { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public string Website { get; set; }
        public string BizDesc { get; set; }
        public string UEN { get; set; }
        public string Notes { get; set; }
    }
}