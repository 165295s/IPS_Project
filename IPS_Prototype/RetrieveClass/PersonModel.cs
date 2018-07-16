using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace IPS_Prototype.RetrieveClass
{
    [DataContract]
    public class PersonModel
    {
        [DataMember (Name = "id")]
        public string id { get; set; }

        [DataMember (Name ="firstName")]
        public string firstName { get; set; }

        [DataMember(Name ="surname")]
        public string surname { get; set; }

        [DataMember (Name = "fullname_nametags")]
        public string fullNameNametag { get; set; }

        [DataMember(Name = "salutation")]
        public string salutation { get; set; }

        [DataMember(Name = "nationality")]
        public string nationality { get; set; }

        [DataMember (Name = "email")]
        public string email { get; set; }

        [DataMember (Name = "honorific")]
        public string honorific { get; set; }

        [DataMember (Name = "gender")]
        public string gender { get; set; }

        [DataMember (Name = "status")]
        public string status { get; set; }

        [DataMember (Name = "role")]
        public string role { get; set; }

        [DataMember (Name = "telNum")]
        public string telNum { get; set; }

        [DataMember (Name = "organisation1")]
        public string organisation1 { get; set; }

        [DataMember (Name = "department1")]
        public string department1 { get; set; }

        [DataMember (Name = "designation1")]
        public string designation1 { get; set; }

        [DataMember (Name = "organisation2")]
        public string organisation2 { get; set; }

        [DataMember (Name = "department2")]
        public string department2 { get; set; }

        [DataMember (Name = "designation2")]
        public string designation2 { get; set; }
         
        [DataMember (Name = "SDR")]
        public string SDR { get; set; }

        [DataMember(Name = "Source")]
        public string source { get; set; }

        [DataMember(Name = "cat1")]
        public string cat1 { get; set; }
        [DataMember(Name = "cat2")]
        public string cat2 { get; set; }

        [DataMember(Name = "faciBriefed")]
        public string faciBriefed { get; set; }

        [DataMember(Name = "emailSent")]
        public string emailSent { get; set; }
        [DataMember(Name = "orgName")]
        public string orgName { get; set; }




        public PersonModel(string id, string firstName, string surname, string fullNameNametag, string salutation, string nationality, string email, string honorific, string gender, string status, string role, string telNum, string organisation1, string department1, string designation1, string organisation2, string department2, string designation2, string SDR)
        {
            this.id = id;
            this.firstName = firstName;
            this.surname = surname;
            this.fullNameNametag = fullNameNametag;
            this.salutation = salutation;
            this.nationality = nationality;
            this.email = email;
            this.honorific = honorific;
            this.gender = gender;
            this.status = status;
            this.role = role;
            this.telNum = telNum;
            this.organisation1 = organisation1;
            this.department1 = department1;
            this.designation1 = designation1;
            this.organisation2 = organisation2;
            this.department2 = department2;
            this.designation2 = designation2;
            this.SDR = SDR;
        }

    


        public PersonModel()
        {
            //this.id = null;
            //this.firstName = null;
            //this.surname = null;
            //this.fullNameNametag = null;
            //this.salutation = null;
            //this.nationality = null;
            //this.email = null;
            //this.honorific = null;
            //this.gender = null;
            //this.status = null;
            //this.role = null;
            //this.telNum = null;
            //this.organisation1 = null;
            //this.department1 = null;
            //this.designation1 = null;
            //this.organisation2 = null;
            //this.department2 = null;
            //this.designation2 = null;
            //SDR = null;



        }
    }
}