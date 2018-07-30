using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using IPS_Prototype.RetrieveClass;

namespace IPS_Prototype
{
    /// <summary>
    /// Summary description for Fundraising_Autocomplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Fundraising_Autocomplete : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<PersonModel> GetFundraisingIndAutoComplete(string txt)
        {
            // your code to query the database goes here
            List<PersonModel> result = new List<PersonModel>();
            List<PersonModel> resultReturn = new List<PersonModel>();
            PersonModel person = new PersonModel();
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["IPS"].ToString();
            //string sqlQuery = "Select distinct p.email_addr, p.person_id,p.First_Name, p.surname, p.fullname_nametags from membership.TBL_PERSON p " +
            //    "INNER JOIN membership.TBL_MEMBERSHIP m on p.person_id = m.person_id INNER JOIN event.TBL_EVENT_GUEST_INVITE e " +
            //    "on e.person_id = m.person_id where p.fullname_nametags like '%' + @SearchText + '%'";
            string sqlQuery = "Select distinct p.email_addr, p.person_id,p.First_Name, p.surname, p.fullname_nametags from membership.TBL_PERSON p " +
               "INNER JOIN membership.TBL_MEMBERSHIP m on p.person_id = m.person_id where p.fullname_nametags like '%' + @SearchText + '%'";
            using (SqlConnection obj_SqlConnection = new SqlConnection(con))
            {

                using (SqlCommand obj_Sqlcommand = new SqlCommand(sqlQuery, obj_SqlConnection))
                {
                    obj_SqlConnection.Open();
                    obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt.Trim());
                    SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                    while (obj_result.Read())
                    {
                        person.fullNameNametag = obj_result["fullname_nametags"].ToString();
                        person.id = obj_result["person_id"].ToString();
                        person.firstName = obj_result["First_Name"].ToString();
                        person.surname = obj_result["surname"].ToString();
                        person.email = obj_result["email_addr"].ToString();
                        result.Add(person);
                        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                        var objectAsJsonString = serializer.Serialize(person);
                        PersonModel deserializedObject = serializer.Deserialize<PersonModel>(objectAsJsonString);
                        resultReturn.Add(deserializedObject);
                    }
                }
                return resultReturn;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<OrganisationModel> GetFundraisingOrgAutoComplete(string txt)
        {
            // your code to query the database goes here
            List<OrganisationModel> result = new List<OrganisationModel>();
            List<OrganisationModel> resultReturn = new List<OrganisationModel>();
            OrganisationModel organisation = new OrganisationModel();
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["IPS"].ToString();
            //string sqlQuery = "Select distinct o.NAME, o.OFFICE_NUM, o.ORG_ID from membership.TBL_ORGANISATION o " +
            //    "INNER JOIN membership.TBL_MEMBERSHIP m on o.ORG_ID = m.ORG_ID INNER JOIN event.TBL_EVENT_GUEST_INVITE e " +
            //    "on e.MEMBER_ID = m.MEMBER_ID where o.NAME like '%' + @SearchText + '%'";
            string sqlQuery = "Select distinct o.NAME, o.OFFICE_NUM, o.ORG_ID from membership.TBL_ORGANISATION o " +
                "INNER JOIN membership.TBL_MEMBERSHIP m on o.ORG_ID = m.ORG_ID where o.NAME like '%' + @SearchText + '%'";
            using (SqlConnection obj_SqlConnection = new SqlConnection(con))
            {

                using (SqlCommand obj_Sqlcommand = new SqlCommand(sqlQuery, obj_SqlConnection))
                {
                    obj_SqlConnection.Open();
                    obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt.Trim());
                    SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                    while (obj_result.Read())
                    {
                        organisation.officeNo = obj_result["OFFICE_NUM"].ToString();
                        organisation.orgname = obj_result["NAME"].ToString();
                        organisation.orgid = obj_result["ORG_ID"].ToString();
                        result.Add(organisation);
                        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                        var objectAsJsonString = serializer.Serialize(organisation);
                        OrganisationModel deserializedObject = serializer.Deserialize<OrganisationModel>(objectAsJsonString);
                        resultReturn.Add(deserializedObject);
                    }
                }
                return resultReturn;
            }
        }
    }
}
