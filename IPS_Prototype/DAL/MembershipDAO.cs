using IPS_Prototype.Class;
using IPS_Prototype.RetrieveClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IPS_Prototype.DAL
{
    public class MembershipDAO
    {
        DbHelper dbhelp = new DbHelper();


        public DataTable GetPAInfo(string caRepID)
        {
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            string commandtext = "  Select pa.PA_ID,pa.Honorific as Honorific,pa.first_name as First_Name,pa.surname as Surname, Pa.Tel_Num as Telephone from membership.TBL_PERSONAL_ASSISTANT PA INNER JOIN  membership.TBL_CA_REP_PA crp on pa.PA_Id = crp.PA_Id INNER JOIN membership.TBL_Org_Ca_Rep OCR on OCR.CA_Rep_Id = crp.CA_Rep_Id INNER JOIN membership.TBL_ORGANISATION org on org.Org_Id = OCR.Org_Id where OCR.CA_Rep_Id = @caRepId;";

            DataTable dt = dbhelp.ExecDataReader(commandtext, "@caRepId", caRepID);

            return dt;

        }

        public DataTable GetCAREP()
        {
            string commandtext = "Select p.person_id as Person_Id,ca.ca_rep_id as CA_Rep_Id,ca.Org_Id as Org_Id ,p.Honorific AS Honorific,p.First_Name as First_Name, p.surname as Surname, p.Designation_1 as Designation,p.Nationality as Nationality,ca.Role as Role From membership.TBL_PERSON p INNER JOIN membership.TBL_ORG_CA_REP CA  on p.Person_Id = ca.Person_Id where ca.Org_Id = (Select org_id from membership.TBL_ORGANISATION where Org_Id =(Select Max(org_id) from membership.TBL_ORGANISATION));";
            DataTable dt = dbhelp.ExecDataReader(commandtext);


            return dt;

        }
        public DataTable GetPAInfo()
        {
            string commandtext = "Select p.person_id as Person_Id,ca.ca_rep_id as CA_Rep_Id,ca.Org_Id as Org_Id ,p.Honorific AS Honorific,p.First_Name as First_Name, p.surname as Surname, p.Designation_1 as Designation,p.Nationality as Nationality,ca.Role as Role From membership.TBL_PERSON p INNER JOIN membership.TBL_ORG_CA_REP CA  on p.Person_Id = ca.Person_Id where ca.Org_Id = (Select org_id from membership.TBL_ORGANISATION where Org_Id =(Select Max(org_id) from membership.TBL_ORGANISATION));";
            DataTable dt = dbhelp.ExecDataReader(commandtext);


            return dt;

        }

        public DataTable GetIndivPAInfo()
        {
            string commandtext = "SELECT Honorific AS Honorific, First_Name AS First_Name, surname AS Surname, Email_Addr AS Email_Addr, Tel_Num AS Tel_Num from membership.TBL_PERSONAL_ASSISTANT; ";
            DataTable dt = dbhelp.ExecDataReader(commandtext);


            return dt;

        }

        public DataTable GetLookup()
        {

            string commandtext = "SELECT Code_Type, Code, Code_Desc FROM admin.TBL_CODE_LOOKUP;";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;
        }

        public DataTable GetLookupSearch(string search)
        {

            string commandtext = "SELECT Code,Code_Desc FROM admin.TBL_CODE_LOOKUP WHERE Code_Type = @search";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@search", search);
            return dt;
        }

        public DataTable GetSource()
        {

            string commandtext = "SELECT DISTINCT source FROM admin.TBL_CODE_SOURCE_CAT_LOOKUP;";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;
        }

        public DataTable GetCat1(string search)
        {

            string commandtext = "SELECT DISTINCT cat_1 FROM admin.TBL_CODE_SOURCE_CAT_LOOKUP WHERE source = @search;";
            DataTable dt = dbhelp.ExecDataReader(commandtext,"@search",search);
            return dt;
        }

        public DataTable GetCat2()
        {

            string commandtext = "SELECT Code, Code_Desc FROM admin.TBL_CODE_LOOKUP WHERE code_type = 'CAT2';";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;
        }


        //Retrieve all Person Data at Edit Modal
        public PersonModel GetPersonData(string personid)
        {
            PersonModel person = new PersonModel();
            DataTable dt;
            string commandtext = "SELECT p.FIRST_NAME, p.SURNAME, p.GENDER, p.SOURCE, p.HONORIFIC, p.SALUTATION, p.TEL_NUM, p.EMAIL_ADDR, p.NATIONALITY, p.DESIGNATION_1, p.DEPARTMENT_1, p.ORGANISATION_1, p.DESIGNATION_2, p.DEPARTMENT_2, p.ORGANISATION_2, p.SPECIAL_DIETARY_REQUIREMENT, p.FULLNAME_NAMETAGS, m.STATUS FROM membership.TBL_PERSON p INNER JOIN membership.TBL_MEMBERSHIP m ON p.PERSON_ID = m.MEMBER_ID  WHERE p.PERSON_ID = @personid";
            dt = dbhelp.ExecDataReader(commandtext, "@personid", personid);
            person.firstName = dt.Rows[0]["FIRST_NAME"].ToString();
            person.surname = dt.Rows[0]["SURNAME"].ToString();
            person.gender = dt.Rows[0]["GENDER"].ToString();
            person.source = dt.Rows[0]["SOURCE"].ToString();
            person.honorific = dt.Rows[0]["HONORIFIC"].ToString();
            person.salutation = dt.Rows[0]["SALUTATION"].ToString();
            person.telNum = dt.Rows[0]["TEL_NUM"].ToString();
            person.email = dt.Rows[0]["EMAIL_ADDR"].ToString();
            person.nationality = dt.Rows[0]["NATIONALITY"].ToString();
            person.designation1 = dt.Rows[0]["DESIGNATION_1"].ToString();
            person.department1 = dt.Rows[0]["DEPARTMENT_1"].ToString();
            person.organisation1 = dt.Rows[0]["ORGANISATION_1"].ToString();
            person.designation2 = dt.Rows[0]["DESIGNATION_2"].ToString();
            person.department2 = dt.Rows[0]["DEPARTMENT_2"].ToString();
            person.organisation2 = dt.Rows[0]["ORGANISATION_2"].ToString();
            person.SDR = dt.Rows[0]["SPECIAL_DIETARY_REQUIREMENT"].ToString();
            person.fullNameNametag = dt.Rows[0]["FULLNAME_NAMETAGS"].ToString();
            person.status = dt.Rows[0]["STATUS"].ToString();
            return person;
        }

        public PersonModel GetCAREPData(string carepid)
        {
            PersonModel person = new PersonModel();
            DataTable dt;
            string commandtext = " SELECT o.NAME,p.FIRST_NAME, p.SURNAME, p.GENDER, p.SOURCE, p.HONORIFIC, p.SALUTATION, p.TEL_NUM, p.EMAIL_ADDR, p.NATIONALITY, p.DESIGNATION_1, p.DEPARTMENT_1, p.ORGANISATION_1, p.DESIGNATION_2, p.DEPARTMENT_2, p.ORGANISATION_2, p.SPECIAL_DIETARY_REQUIREMENT, ca.FULLNAME_NAMETAGS, ca.STATUS,CA.FACILITATOR_BRIEFED,CA.EMAIL_SENT,CA.ROLE FROM membership.TBL_PERSON p INNER JOIN membership.TBL_ORG_CA_REP ca ON p.PERSON_ID = ca.PERSON_ID INNER JOIN membership.TBL_ORGANISATION o on o.ORG_ID = ca.ORG_ID  WHERE ca.PERSON_ID =  @carepid";
            dt = dbhelp.ExecDataReader(commandtext, "@carepid", carepid);
            person.orgName = dt.Rows[0]["NAME"].ToString();
            person.firstName = dt.Rows[0]["FIRST_NAME"].ToString();
            person.surname = dt.Rows[0]["SURNAME"].ToString();
            person.gender = dt.Rows[0]["GENDER"].ToString();
            person.source = dt.Rows[0]["SOURCE"].ToString();
            person.honorific = dt.Rows[0]["HONORIFIC"].ToString();
            person.salutation = dt.Rows[0]["SALUTATION"].ToString();
            person.telNum = dt.Rows[0]["TEL_NUM"].ToString();
            person.email = dt.Rows[0]["EMAIL_ADDR"].ToString();
            person.nationality = dt.Rows[0]["NATIONALITY"].ToString();
            person.designation1 = dt.Rows[0]["DESIGNATION_1"].ToString();
            person.department1 = dt.Rows[0]["DEPARTMENT_1"].ToString();
            person.organisation1 = dt.Rows[0]["ORGANISATION_1"].ToString();
            person.designation2 = dt.Rows[0]["DESIGNATION_2"].ToString();
            person.department2 = dt.Rows[0]["DEPARTMENT_2"].ToString();
            person.organisation2 = dt.Rows[0]["ORGANISATION_2"].ToString();
            person.SDR = dt.Rows[0]["SPECIAL_DIETARY_REQUIREMENT"].ToString();
            person.fullNameNametag = dt.Rows[0]["FULLNAME_NAMETAGS"].ToString();
            person.status = dt.Rows[0]["STATUS"].ToString();
            person.role = dt.Rows[0]["ROLE"].ToString();
            person.faciBriefed = dt.Rows[0]["FACILITATOR_BRIEFED"].ToString();
            person.emailSent = dt.Rows[0]["EMAIL_SENT"].ToString();
            return person;
        }













        //CHRIS 
        //To Add a PA in Personal Assistant table and Ca_Rep_PA table as well
        public int addCAREP_PA(ArrayList carep_PAList)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand carep_pa_tbl = new SqlCommand();

                string commandtext = "INSERT INTO membership.TBL_PERSONAL_ASSISTANT (Honorific,First_Name, surname, Email_Addr, Tel_Num, Created_DT) VALUES(@honorific,@fName,@lName,@email,@telNum,@createdDate);";
                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@honorific", carep_PAList[0], "@fName", carep_PAList[1], "@lName", carep_PAList[2], "email", carep_PAList[3], "@telNum", carep_PAList[4], "@createdDate", DateTime.Now.ToShortDateString());

                string commandtext1 = "INSERT INTO membership.TBL_CA_REP_PA(Person_Id,CA_Rep_Id,Org_Id,PA_Id) VALUES (@personID,@caID,@orgID,(SELECT MAX(Pa_Id) FROM membership.TBL_PERSONAL_ASSISTANT)); ";
                carep_pa_tbl = dbhelp.CreateCommand(commandtext1, CommandType.Text, "@personID", carep_PAList[5], "@caID", carep_PAList[6], "@orgID", carep_PAList[7]);

                transcommand.Add(mycmd);
                transcommand.Add(carep_pa_tbl);
                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {

                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;

        }

        //CHRIS
        //To create a new Organisation in Organisation Table
        public int addOrg(ArrayList orgList)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand orgMembershipMycmd = new SqlCommand();



                string commandtext = "INSERT INTO membership.TBL_ORGANISATION (NAME, Mailing_Add_Line_1, Mailing_Add_Line_2, Mailing_Add_City, Mailing_Add_Postal, Tel_Num, Office_Num, Website_Url, Biz_Description, Point_Of_Contact, Created_DT, Notes,UEN) VALUES(@orgName, @mailAddLine1,@mailAddLine2,@mailAddCity,@mailAddPostal,@telNum,@offNum,@websiteURL,@bizDesc,@POC,@created_Date,@notes,@uen)";
                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@orgName", orgList[3], "@mailAddLine1", orgList[4], "mailAddLine2", orgList[5], "@mailAddCity", orgList[6], "@mailAddPostal", orgList[7], "@telNum", orgList[8], "@offNum", orgList[9], "@websiteURL", orgList[10], "@bizDesc", orgList[11], "@POC", orgList[12], "@created_Date", DateTime.Now.ToShortDateString(), "@notes", orgList[13], "@UEN", orgList[14]);

                string orgMembershipCmdText = "INSERT INTO membership.TBL_MEMBERSHIP (Org_Id,Donor_Tier,Created_DT,Expiry_DT) VALUES ( (SELECT MAX(Org_Id) FROM membership.TBL_ORGANISATION), @dTier,@cDate,CONVERT(date,@expDate,103));";

                orgMembershipMycmd = dbhelp.CreateCommand(orgMembershipCmdText, CommandType.Text, "@dTier", orgList[1], "@cDate", DateTime.Now.ToShortDateString(), "@expDate", orgList[2]);


                transcommand.Add(mycmd);
                transcommand.Add(orgMembershipMycmd);
                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {

                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;

        }

        //CHRIS
        //To create a new CAREP in CAREP Table
        public int addCAREP(ArrayList carepList)
        {
            int result = 0;

            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand carepCmd = new SqlCommand();

                // "INSERT INTO Org_Ca_Rep (Org_Id, Role, Fullname_Nametags, Status, Created_Date, Person_Id) VALUES( (SELECT MAX(Org_Id) FROM Organisation), @role, @fullNameNT, @status, @created_date, (SELECT)";
                string commandtext = "INSERT INTO membership.TBL_PERSON (First_Name, Surname,Gender,Honorific,Salutation,Tel_Num,Email_Addr, Created_DT, Designation_1, Department_1, Organisation_1,Designation_2,Department_2,Organisation_2,Special_Dietary_Requirement,Nationality,Fullname_Nametags,Source,Cat_1,Cat_2) VALUES( @fName,@sName,@gender,@honorific,@salutation,@tel_num,@email_addr,@created_date,@desig1,@dept1,@org1,@desig2,@dept2,@org2,@SDR,@nationality,@fullNameNT,@source,@cat1,@cat2);";
                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@fName", carepList[0], "@sNAme", carepList[1], "@gender", carepList[2], "@honorific", carepList[3], "@salutation", carepList[4], "@tel_num", carepList[5], "@email_addr", carepList[6], "@created_date", DateTime.Now.ToShortDateString(), "@desig1", carepList[7], "@dept1", carepList[8], "@org1", carepList[9], "@desig2", carepList[10], "@dept2", carepList[11], "@org2", carepList[12], "@SDR", carepList[13], "@nationality", carepList[14], "@fullNameNT", carepList[18],"@source", carepList[20],"@cat1", carepList[21],"cat2", carepList[22]);

                string carepCommanText = "INSERT INTO membership.TBL_ORG_CA_REP (Org_Id, Role, Status ,Fullname_Nametags, Created_DT, Person_Id,Facilitator_Briefed,Email_Sent) VALUES( (SELECT MAX(Org_Id) FROM membership.TBL_ORGANISATION), @role,@status ,@fullNameNT, @created_date, (SELECT MAX(Person_Id) FROM membership.TBL_PERSON),@faciBriefed,@emailSent );";
                carepCmd = dbhelp.CreateCommand(carepCommanText, CommandType.Text, "@role", carepList[15], "@status", carepList[16], "@created_date", DateTime.Now.ToShortDateString(), "@fullNameNT", carepList[17], "@faciBriefed", carepList[18], "@emailSent", carepList[19]);



                transcommand.Add(mycmd);
                transcommand.Add(carepCmd);
                result = dbhelp.ExecTrans(transcommand);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());



            }

            return result;

        }
        //CHRIS
        //To Create new record in Person_PA table when person is created
        public int addPA_Record(List<Object> paList)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                //SqlCommand mycmd = new SqlCommand();

                //string commandtext = "INSERT INTO Person_PA (Person_Id, PA_Id) VALUES (  (SELECT MAX(person_id) FROM person), (SELECT MAX(pa_id) FROM Personal_Assistant) )";
                //mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text);
                string commandtext = "INSERT INTO membership.TBL_PERSON_PA (Person_Id, PA_Id) VALUES ( (SELECT MAX(person_id) FROM membership.TBL_PERSON), @PA_ID )";

                for (int i = 0; i < paList.Count; i++)
                {
                    SqlCommand mycmd = new SqlCommand();

                    mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@PA_ID", paList[i]);
                    transcommand.Add(mycmd);



                }

                result = dbhelp.ExecTrans(transcommand);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());



            }


            return result;


        }
        //CHRIS
        //TO create new Individual 
        public int AddPerson(ArrayList pList)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand orgMycmd = new SqlCommand();
                SqlCommand mycmd1 = new SqlCommand();


                string commandtext = "INSERT INTO membership.TBL_PERSON (First_Name, surname,Gender,Honorific,Salutation,Tel_Num,Email_Addr, Created_DT, Designation_1, Department_1, Organisation_1,Designation_2,Department_2,Organisation_2,Special_Dietary_Requirement,Nationality,Fullname_Nametags,source,cat_1,cat_2) VALUES( @fName,@sName,@gender,@honorific,@salutation,@tel_num,@email_addr,@created_date,@desig1,@dept1,@org1,@desig2,@dept2,@org2,@SDR,@nationality, @fullNameNT, @source, @cat1,@cat2)";
                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@fName", pList[3], "@sNAme", pList[4], "@gender", pList[5], "@honorific", pList[6], "@salutation", pList[7], "@tel_num", pList[8], "@email_addr", pList[9], "@created_date", DateTime.Now.ToShortDateString(), "@desig1", pList[10], "@dept1", pList[11], "@org1", pList[12], "@desig2", pList[13], "@dept2", pList[14], "@org2", pList[15], "@SDR", pList[16], "@nationality", pList[17], "@fullNameNT",pList[18],"@source",pList[20],"@cat1",pList[21],"@cat2",pList[22]);


                string memCommandText = "INSERT INTO membership.TBL_MEMBERSHIP(Person_Id,Designation,Donor_Tier,Created_DT,Expiry_DT,Status) VALUES ( (SELECT MAX(person_id) FROM membership.TBL_PERSON),@desig,@dTier,@cDate, CONVERT(date,@expDate,103),@status)";
                orgMycmd = dbhelp.CreateCommand(memCommandText, CommandType.Text, "@desig", pList[10], "@dTier", pList[1], "@cDate", DateTime.Now.ToShortDateString(), "@expDate", pList[2],"@status",pList[19]);


                //string personPACmdText = "INSERT INTO Person_PA (Person_Id, PA_Id) VALUES (  (SELECT MAX(person_id) FROM person), (SELECT MAX(pa_id) FROM Personal_Assistant) )";
                //mycmd1 = dbhelp.CreateCommand(personPACmdText, CommandType.Text);







                transcommand.Add(mycmd);
                transcommand.Add(orgMycmd);
                //transcommand.Add(mycmd1);


                result = dbhelp.ExecTrans(transcommand);
                //int x =createPA_Record(paIdList);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());



            }
            finally
            {

                //createPA_Record();

            }
            return result;


        }

        //CHRIS
        //To create new PA in AddPA_Modal.ascx
        public int AddPA(string honorific, string fName, string sName, string tel_num, string email)
        {

            int result = 0;
            //Object pa_ID;
            string date = DateTime.Now.ToShortDateString();
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand mycmd1 = new SqlCommand();
                //paIdList = new List<Object>();


                string commandtext = "INSERT INTO membership.TBL_PERSONAL_ASSISTANT (Honorific, First_Name, surname, Email_Addr, Tel_Num, Created_DT) VALUES(@honorific, @first_name, @last_name, @email, @tel_num, @create_date);";
                //string commandtext_PA = "SELECT MAX(PA_Id) FROM Personal_Assistant";
                string commandtext_PA = "INSERT INTO membership.TBL_PERSON_PA (Person_Id, PA_Id) VALUES ( (SELECT MAX(person_id) FROM membership.TBL_PERSON), (SELECT MAX(PA_ID) FROM membership.TBL_PERSONAL_ASSISTANT) )";

                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@honorific", honorific, "@first_name", fName, "@last_name", sName, "@email", email, "@tel_num", tel_num, "@create_date", date);
                //mycmd1 = dbhelp.CreateCommand(commandtext_PA, CommandType.Text);
                mycmd1 = dbhelp.CreateCommand(commandtext_PA, CommandType.Text);
                transcommand.Add(mycmd);
                transcommand.Add(mycmd1);

                result = dbhelp.ExecTrans(transcommand);
                //pa_ID = new Object ();
                //pa_ID = dbhelp.ExecScalar("SELECT MAX(PA_Id) FROM Personal_Assistant");
                //paIdList.Add(pa_ID);








            }
            catch (Exception ex)
            {

                ErrorLog.WriteErrorLog(ex.ToString());

            }
            return result;

        }

        public int UpdateCAREP(int pid, string fname, string sname, string gender, string source, string honorific, string salutation, string telnum, string email, string nationality, DateTime modified, string des1, string dep1, string org1, string des2, string dep2, string org2, string sdr, string fnametags,string role, string status, string faciBriefed, string emailSent)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            SqlCommand mycmd1 = new SqlCommand();
            string commandtext = "UPDATE membership.TBL_PERSON SET FIRST_NAME = @firstname, SURNAME = @surname, GENDER = @gender, SOURCE = @source, HONORIFIC = @honorific, SALUTATION = @salutation, TEL_NUM = @telnum, EMAIL_ADDR = @email, NATIONALITY = @nationality, Modified_DT = @modified_date, DESIGNATION_1 = @des1, DEPARTMENT_1 = @dep1, ORGANISATION_1 = @org1, DESIGNATION_2 = @des2, DEPARTMENT_2 = @dep2, ORGANISATION_2 = @org2, SPECIAL_DIETARY_REQUIREMENT = @sdr, FULLNAME_NAMETAGS = @fullnamenametags WHERE PERSON_ID = @pid;";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@firstname", fname, "@surname", sname, "@gender", gender, "@source", source, "@honorific", honorific, "@salutation", salutation, "@telnum", telnum, "@email", email, "@nationality", nationality, "@modified_date", modified, "@des1", des1, "@dep1", dep1, "@org1", org1, "@des2", des2, "@dep2", dep2, "@org2", org2, "@sdr", sdr, "@fullnamenametags", fnametags, "@pid", pid);
            string carepCMDTxt = "UPDATE membership.TBL_ORG_CA_REP SET ROLE = @role, FULLNAME_NAMETAGS = @fullnameNT, STATUS = @status, FACILITATOR_BRIEFED = @faciBriefed, EMAIL_SENT = @emailSent WHERE PERSON_ID = @pid;";
            mycmd1 = dbhelp.CreateCommand(carepCMDTxt, CommandType.Text, "@role", role, "@fullnameNT", fnametags, "@status", status, "@faciBriefed", faciBriefed, "@emailSent", emailSent, "@pid", pid);

            transcommand.Add(mycmd);
            transcommand.Add(mycmd1);
            result = dbhelp.ExecTrans(transcommand);

            return result;
        }

        public int UpdateIndividual(int pid, string fname, string sname, string gender, string source, string honorific, string salutation, string telnum, string email, string nationality, DateTime modified, string des1, string dep1, string org1, string des2, string dep2, string org2, string sdr, string fnametags, string status)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            SqlCommand mycmd1 = new SqlCommand();
            string commandtext = "UPDATE membership.TBL_PERSON SET FIRST_NAME = @firstname, SURNAME = @surname, GENDER = @gender, SOURCE = @source, HONORIFIC = @honorific, SALUTATION = @salutation, TEL_NUM = @telnum, EMAIL_ADDR = @email, NATIONALITY = @nationality, Modified_DT = @modified_date, DESIGNATION_1 = @des1, DEPARTMENT_1 = @dep1, ORGANISATION_1 = @org1, DESIGNATION_2 = @des2, DEPARTMENT_2 = @dep2, ORGANISATION_2 = @org2, SPECIAL_DIETARY_REQUIREMENT = @sdr, FULLNAME_NAMETAGS = @fullnamenametags WHERE PERSON_ID = @pid;";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@firstname", fname, "@surname", sname, "@gender", gender, "@source", source, "@honorific", honorific, "@salutation", salutation, "@telnum", telnum, "@email", email, "@nationality", nationality, "@modified_date", modified, "@des1", des1, "@dep1", dep1, "@org1", org1, "@des2", des2, "@dep2", dep2, "@org2", org2, "@sdr", sdr, "@fullnamenametags", fnametags, "@pid", pid);
            string commandtext1 = "UPDATE membership.TBL_MEMBERSHIP SET STATUS = @status, DESIGNATION = @desig WHERE PERSON_ID = @pid;";
            mycmd1 = dbhelp.CreateCommand(commandtext1, CommandType.Text, "@status", status, "@desig",des1,"@pid", pid);

            transcommand.Add(mycmd);
            transcommand.Add(mycmd1);
            result = dbhelp.ExecTrans(transcommand);

            return result;
        }
        public int DeleteCAREPPA(string pa_id) {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            string commandtext = "DELETE FROM membership.TBL_PERSONAL_ASSISTANT WHERE PA_ID = @pa_id; " +
                                   "DELETE FROM membership.TBL_CA_REP_PA WHERE PA_ID = @pa_id; ";

            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@pa_id", pa_id);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);

            return result;

        }

        public int DeleteIARecord(int personid)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            //string commandtext = "delete from membership.TBL_PERSON_PA where PERSON_ID=1;" + 
            //                     "delete from membership.TBL_PERSONAL_ASSISTANT where PA_ID=(Select PA_ID from membership.TBL_PERSON_PA where PERSON_ID=1); " +
            //                     "delete from membership.TBL_MEMBERSHIP where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON where PERSON_ID=1); " +
            //                     "delete from event.TBL_EVENT_GUEST_INVITE where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON where PERSON_ID=1); " +
            //                     "delete from TBL_PERSON where PERSON_ID=1; ";
            string commandtext = "delete from membership.TBL_PERSONAL_ASSISTANT where PA_ID=(Select top 1 PA_ID from membership.TBL_PERSON_PA where PERSON_ID=@id); " +
                                 "delete from membership.TBL_PERSON_PA where PERSON_ID=@id;" +
                                 "delete from membership.TBL_MEMBERSHIP where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON where PERSON_ID=@id); " +
                                 "delete from event.TBL_EVENT_GUEST_INVITE where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON where PERSON_ID=@id); " +
                                 "delete from membership.TBL_PERSON where PERSON_ID=@id;";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@id", personid);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);

            return result;
        }
        public int DeleteCAREPRecord(int personid)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            //string commandtext = "delete from membership.TBL_PERSON_PA where PERSON_ID=1;" + 
            //                     "delete from membership.TBL_PERSONAL_ASSISTANT where PA_ID=(Select PA_ID from membership.TBL_PERSON_PA where PERSON_ID=1); " +
            //                     "delete from membership.TBL_MEMBERSHIP where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON where PERSON_ID=1); " +
            //                     "delete from event.TBL_EVENT_GUEST_INVITE where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON where PERSON_ID=1); " +
            //                     "delete from TBL_PERSON where PERSON_ID=1; ";
            string commandtext = "delete from membership.TBL_PERSONAL_ASSISTANT where PA_ID=(Select top 1 PA_ID from membership.TBL_PERSON_PA where PERSON_ID=@id); " +
                                 "delete from membership.TBL_CA_REP_PA where PERSON_ID=@id;" +
                                 "delete from membership.TBL_MEMBERSHIP where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON where PERSON_ID=@id); " +
                                 "delete from event.TBL_EVENT_GUEST_INVITE where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON where PERSON_ID=@id); " +
                                 "delete from membership.TBL_PERSON where PERSON_ID=@id;" +
                                 "delete from membership.TBL_ORG_CA_REP where PERSON_ID=@id";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@id", personid);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);

            return result;
        }


    }
}