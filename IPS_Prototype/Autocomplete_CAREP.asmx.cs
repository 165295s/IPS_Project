using IPS_Prototype.RetrieveClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace IPS_Prototype
{
    /// <summary>
    /// Summary description for testWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class testWebService : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<PersonModel> GetAutoCompleteData(string txt)
        {
            // your code to query the database goes here
            //List<PersonModel> result = new List<PersonModel>();
            List<PersonModel> result = new List<PersonModel>();
            List<PersonModel> resultReturn = new List<PersonModel>();

            //PersonModel p1 = new PersonModel();
            PersonModel person = new PersonModel();
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["IPS"].ToString();
            string sqlQuery = "Select p.person_id,p.First_Name, p.surname,ca.fullname_nametags, p.gender, p.honorific,p.salutation,p.tel_num,p.email_addr, p.nationality,p.designation_1,p.department_1,p.organisation_1,p.designation_2,p.department_2,p.organisation_2,p.special_dietary_requirement, ca.role,ca.status,p.source,p.cat_1,p.cat_2,ca.role,ca.email_sent,ca.facilitator_briefed from membership.TBL_PERSON p INNER JOIN membership.TBL_ORG_CA_REP ca on p.person_id = ca.person_id where p.first_name like  @SearchText + '%'";
            using (SqlConnection obj_SqlConnection = new SqlConnection(con))
            {
                //"Select CONCAT(first_name,' ', last_name) as txt from person where first_name like '%'+@SearchText+'%';"

                using (SqlCommand obj_Sqlcommand = new SqlCommand(sqlQuery, obj_SqlConnection))
                {
                    obj_SqlConnection.Open();
                    obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt.Trim());
                    SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                    while (obj_result.Read())
                    {
                        //result.Add(string.Format("{0}/{1}/{2}", obj_result["txt"], obj_result["email"], obj_result["pid"].ToString()));


                        person.id = obj_result["person_id"].ToString();
                        person.firstName = obj_result["First_Name"].ToString();
                        person.surname = obj_result["surname"].ToString();
                        person.fullNameNametag = obj_result["fullname_nametags"].ToString();
                        person.gender = obj_result["gender"].ToString();
                        person.honorific = obj_result["honorific"].ToString();
                        person.salutation = obj_result["salutation"].ToString();
                        person.telNum = obj_result["tel_num"].ToString();
                        person.email = obj_result["email_addr"].ToString();
                        person.nationality = obj_result["nationality"].ToString();
                        person.designation1 = obj_result["designation_1"].ToString();
                        person.department1 = obj_result["department_1"].ToString();
                        person.organisation1 = obj_result["organisation_1"].ToString();
                        person.designation2 = obj_result["designation_2"].ToString();
                        person.department2 = obj_result["department_2"].ToString();
                        person.organisation2 = obj_result["organisation_2"].ToString();
                        person.SDR = obj_result["special_dietary_requirement"].ToString();
                        person.source = obj_result["source"].ToString();
                        person.cat1 = obj_result["cat_1"].ToString();
                        person.cat2 = obj_result["cat_2"].ToString();
                        person.status = obj_result["status"].ToString();
                        person.role = obj_result["role"].ToString();
                        person.emailSent = obj_result["email_sent"].ToString();
                        person.faciBriefed = obj_result["facilitator_briefed"].ToString();
                        result.Add(person);

                        //result.Add(obj_result["desig"].ToString().TrimEnd());

                        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                        var objectAsJsonString = serializer.Serialize(person);
                        PersonModel deserializedObject = serializer.Deserialize<PersonModel>(objectAsJsonString);
                        resultReturn.Add(deserializedObject);

                    }

                    //foreach (PersonModel p1 in result)
                    //{
                    //    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    //    var objectAsJsonString = serializer.Serialize(p1);
                    //    PersonModel deserializedObject = serializer.Deserialize<PersonModel>(objectAsJsonString);
                    //    resultReturn.Add(deserializedObject);

                    //}
                }
                return resultReturn;

                //PersonModel p1 = new PersonModel();
                //MemoryStream stream1 = new MemoryStream();
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PersonModel));
                //ser.WriteObject(stream1, p1);
                //stream1.SetLength(0);
                //PersonModel p2 = (PersonModel)ser.ReadObject(stream1);

                //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var objectAsJsonString = serializer.Serialize(person);
                //PersonModel deserializedObject = serializer.Deserialize<PersonModel>(objectAsJsonString);




                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(person));

            }
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(result.GetType());
            //MemoryStream memoryStream = new MemoryStream();
            //serializer.WriteObject(memoryStream, result);
            //var json =  new JavaScriptSerializer().Serialize(result);
            //Context.Response.Write(json)


        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<PersonModel> GetAutoCompleteDataIndiv(string txt)
        {
            // your code to query the database goes here
            //List<PersonModel> result = new List<PersonModel>();
            List<PersonModel> result = new List<PersonModel>();
            List<PersonModel> resultReturn = new List<PersonModel>();

            //PersonModel p1 = new PersonModel();
            PersonModel person = new PersonModel();
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["IPS"].ToString();
            string sqlQuery = "Select p.person_id,p.First_Name, p.surname,p.fullname_nametags, p.gender, p.honorific,p.salutation,p.tel_num,p.email_addr, p.nationality,p.designation_1,p.department_1,p.organisation_1,p.designation_2,p.department_2,p.organisation_2,p.special_dietary_requirement,m.STATUS,p.source,p.cat_1,p.cat_2 from membership.TBL_PERSON p INNER JOIN membership.TBL_MEMBERSHIP m on p.person_id = m.MEMBER_ID where p.first_name like @SearchText + '%'";
            using (SqlConnection obj_SqlConnection = new SqlConnection(con))
            {
                //"Select CONCAT(first_name,' ', last_name) as txt from person where first_name like '%'+@SearchText+'%';"

                using (SqlCommand obj_Sqlcommand = new SqlCommand(sqlQuery, obj_SqlConnection))
                {
                    obj_SqlConnection.Open();
                    obj_Sqlcommand.Parameters.AddWithValue("@SearchText", txt.Trim());
                    SqlDataReader obj_result = obj_Sqlcommand.ExecuteReader();
                    while (obj_result.Read())
                    {
                        //result.Add(string.Format("{0}/{1}/{2}", obj_result["txt"], obj_result["email"], obj_result["pid"].ToString()));


                        person.id = obj_result["person_id"].ToString();
                        person.firstName = obj_result["First_Name"].ToString();
                        person.surname = obj_result["surname"].ToString();
                        person.fullNameNametag = obj_result["fullname_nametags"].ToString();
                        person.gender = obj_result["gender"].ToString();
                        person.honorific = obj_result["honorific"].ToString();
                        person.salutation = obj_result["salutation"].ToString();
                        person.telNum = obj_result["tel_num"].ToString();
                        person.email = obj_result["email_addr"].ToString();
                        person.nationality = obj_result["nationality"].ToString();
                        person.designation1 = obj_result["designation_1"].ToString();
                        person.department1 = obj_result["department_1"].ToString();
                        person.organisation1 = obj_result["organisation_1"].ToString();
                        person.designation2 = obj_result["designation_2"].ToString();
                        person.department2 = obj_result["department_2"].ToString();
                        person.organisation2 = obj_result["organisation_2"].ToString();
                        person.SDR = obj_result["special_dietary_requirement"].ToString();
                        person.source = obj_result["source"].ToString();
                        person.cat1 = obj_result["cat_1"].ToString();
                        person.cat2 = obj_result["cat_2"].ToString();
                        person.status = obj_result["status"].ToString();

                        result.Add(person);

                        //result.Add(obj_result["desig"].ToString().TrimEnd());

                        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                        var objectAsJsonString = serializer.Serialize(person);
                        PersonModel deserializedObject = serializer.Deserialize<PersonModel>(objectAsJsonString);
                        resultReturn.Add(deserializedObject);

                    }

                    //foreach (PersonModel p1 in result)
                    //{
                    //    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    //    var objectAsJsonString = serializer.Serialize(p1);
                    //    PersonModel deserializedObject = serializer.Deserialize<PersonModel>(objectAsJsonString);
                    //    resultReturn.Add(deserializedObject);

                    //}
                }
                return resultReturn;

                //PersonModel p1 = new PersonModel();
                //MemoryStream stream1 = new MemoryStream();
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PersonModel));
                //ser.WriteObject(stream1, p1);
                //stream1.SetLength(0);
                //PersonModel p2 = (PersonModel)ser.ReadObject(stream1);

                //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var objectAsJsonString = serializer.Serialize(person);
                //PersonModel deserializedObject = serializer.Deserialize<PersonModel>(objectAsJsonString);




                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(person));

            }
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(result.GetType());
            //MemoryStream memoryStream = new MemoryStream();
            //serializer.WriteObject(memoryStream, result);
            //var json =  new JavaScriptSerializer().Serialize(result);
            //Context.Response.Write(json)


        }




    }



}
