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
    public class DALFundraising
    {
        DbHelper dbhelp = new DbHelper();
        // Get all Event Type
        public DataTable GetAllType(string search)
        {
            string commandtext = "SELECT Code,Code_Desc FROM admin.TBL_CODE_LOOKUP WHERE Code_Type = @search";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@search", search);
            return dt;
        }

        // Get Event Name of particular type
        public DataTable GetEventName(string search)
        {
            string commandtext = "SELECT NAME FROM event.TBL_EVENT WHERE EVENT_TYPE_CODE = @search";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@search", search);
            return dt;
        }

        // Display text boxes after event name selected
        public DataTable GetEventTbDates(string search)
        {
            string commandtext = "SELECT START_DT_TIME, END_DT_TIME FROM event.TBL_EVENT WHERE NAME = @search";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@search", search);
            return dt;
        }

        //Retrieve all Person Data at Edit Modal
        public PersonModel GetPersonData(int personid)
        {
            PersonModel person = new PersonModel();
            DataTable dt;
            string commandtext = "SELECT FIRST_NAME, SURNAME, GENDER, SOURCE, HONORIFIC, SALUTATION, TEL_NUM, EMAIL_ADDR, NATIONALITY, DESIGNATION_1, DEPARTMENT_1, ORGANISATION_1, DESIGNATION_2, DEPARTMENT_2, ORGANISATION_2, SPECIAL_DIETARY_REQUIREMENT, FULLNAME_NAMETAGS, CAT_1, CAT_2 FROM membership.TBL_PERSON WHERE PERSON_ID = @personid";
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
            person.cat1 = dt.Rows[0]["CAT_1"].ToString();
            person.cat2 = dt.Rows[0]["CAT_2"].ToString();

            return person;
        }

        //Individual Associate Modal Inserting to Donation Table
        public int InsertIndividualDonation(EventInfo eventInfo)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext_Contribution = "INSERT INTO dbo.TBL_DONATION (PERSON_ID, DONATION_DT, DONATION_AMOUNT, CREATED_DT, EVENT_NAME, START_DT_TIME" +
                    ", END_DT_TIME, EVENT_TYPE_CODE) VALUES ((SELECT MAX(PERSON_ID) FROM event.TBL_EVENT_GUEST_INVITE where Person_Id=@personId), @donationdt, " +
                    "@donationamt, @createddate, @eventname, @eventstarttime, @eventendtime, @eventtype)";
                mycmd = dbhelp.CreateCommand(commandtext_Contribution, CommandType.Text, "@personId", eventInfo.PersonId, "@donationdt", eventInfo.DonationDate,
                    "@donationamt", eventInfo.DonationAmt, "@createddate", eventInfo.CreatedDate, "@eventname", eventInfo.EventName, "@eventstarttime", eventInfo.EventStartDate,
                    "@eventendtime", eventInfo.EventEndDate, "@eventtype", eventInfo.EventType);
                transcommand.Add(mycmd);
                result = dbhelp.ExecTrans(transcommand);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return result;
        }

        //Individual Associate Modal Inserting to Donation Table without events
        public int InsertIndividualDonationWithoutEvent(EventInfo eventInfo)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext_Contribution = "INSERT INTO dbo.TBL_DONATION (PERSON_ID, DONATION_DT, DONATION_AMOUNT, CREATED_DT) VALUES ((SELECT MAX(PERSON_ID)" +
                    " FROM event.TBL_EVENT_GUEST_INVITE where Person_Id=@personId), @donationdt, @donationamt, @createddate)";
                mycmd = dbhelp.CreateCommand(commandtext_Contribution, CommandType.Text, "@personId", eventInfo.PersonId, "@donationdt", eventInfo.DonationDate,
                    "@donationamt", eventInfo.DonationAmt, "@createddate", eventInfo.CreatedDate);
                transcommand.Add(mycmd);
                result = dbhelp.ExecTrans(transcommand);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return result;
        }

        //Retrieve all Organisation Data for CA Modal
        public OrganisationModel GetOrgData(int orgid)
        {
            OrganisationModel org = new OrganisationModel();
            DataTable dt;
            string commandtext = "SELECT NAME, TEL_NUM, OFFICE_NUM, POINT_OF_CONTACT, MAILING_ADD_LINE_1, MAILING_ADD_LINE_2," +
                "MAILING_ADD_CITY, MAILING_ADD_POSTAL, WEBSITE_URL, BIZ_DESCRIPTION, UEN, NOTES FROM membership.TBL_ORGANISATION" +
                " WHERE ORG_ID = @orgid";
            dt = dbhelp.ExecDataReader(commandtext, "@orgid", orgid);
            org.orgname = dt.Rows[0]["NAME"].ToString();
            org.telNo = dt.Rows[0]["TEL_NUM"].ToString();
            org.officeNo = dt.Rows[0]["OFFICE_NUM"].ToString();
            org.pointOfContact = dt.Rows[0]["POINT_OF_CONTACT"].ToString();
            org.AddLine1 = dt.Rows[0]["MAILING_ADD_LINE_1"].ToString();
            org.AddLine2 = dt.Rows[0]["MAILING_ADD_LINE_2"].ToString();
            org.City = dt.Rows[0]["MAILING_ADD_CITY"].ToString();
            org.Postal = dt.Rows[0]["MAILING_ADD_POSTAL"].ToString();
            org.Website = dt.Rows[0]["WEBSITE_URL"].ToString();
            org.BizDesc = dt.Rows[0]["BIZ_DESCRIPTION"].ToString();
            org.UEN = dt.Rows[0]["UEN"].ToString();
            org.Notes = dt.Rows[0]["NOTES"].ToString();
            return org;
        }

        //Corporate Associate Modal Inserting to Donation Table
        public int InsertOrganisationDonation(EventInfo eventInfo)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext_Contribution = "INSERT INTO dbo.TBL_DONATION (ORG_ID, DONATION_DT, DONATION_AMOUNT, CREATED_DT, EVENT_NAME, START_DT_TIME" +
                    ", END_DT_TIME, EVENT_TYPE_CODE) VALUES ((SELECT ORG_ID FROM membership.TBL_Membership where ORG_ID=@orgId), @donationdt, " +
                    "@donationamt, @createddate, @eventname, @eventstarttime, @eventendtime, @eventtype)";
                mycmd = dbhelp.CreateCommand(commandtext_Contribution, CommandType.Text, "@orgId", eventInfo.OrgId, "@donationdt", eventInfo.DonationDate,
                    "@donationamt", eventInfo.DonationAmt, "@createddate", eventInfo.CreatedDate, "@eventname", eventInfo.EventName, "@eventstarttime", eventInfo.EventStartDate,
                    "@eventendtime", eventInfo.EventEndDate, "@eventtype", eventInfo.EventType);
                transcommand.Add(mycmd);
                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return result;
        }

        //Corporate Associate Modal Inserting to Donation Table
        public int InsertOrganisationDonationWithoutEvent(EventInfo eventInfo)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext_Contribution = "INSERT INTO dbo.TBL_DONATION (ORG_ID, DONATION_DT, DONATION_AMOUNT, CREATED_DT) VALUES " +
                    "((SELECT ORG_ID FROM membership.TBL_Membership where ORG_ID=@orgId), @donationdt, @donationamt, @createddate)";
                mycmd = dbhelp.CreateCommand(commandtext_Contribution, CommandType.Text, "@orgId", eventInfo.OrgId, "@donationdt", eventInfo.DonationDate,
                    "@donationamt", eventInfo.DonationAmt, "@createddate", eventInfo.CreatedDate);
                transcommand.Add(mycmd);
                result = dbhelp.ExecTrans(transcommand);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return result;
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
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@search", search);
            return dt;
        }

        public DataTable GetCat2()
        {
            string commandtext = "SELECT Code, Code_Desc FROM admin.TBL_CODE_LOOKUP WHERE code_type = 'CAT2';";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;
        }

        //Prospective Modal Insert w/ events
        public int AddProspective(ArrayList prospectiveList)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand MycmdDonations = new SqlCommand();

                string commandtext = "INSERT INTO membership.TBL_PERSON(First_Name, surname, Gender, Honorific, Salutation, Tel_Num, Email_Addr, Designation_1, Department_1, Organisation_1, Designation_2, Department_2, " +
                    "Organisation_2, Special_Dietary_Requirement, Nationality, Fullname_Nametags, SOURCE, CAT_1, CAT_2, Created_DT) " +
                    "VALUES(@fName, @sName, @gender, @honorific, @salutation, @tel_num, @email_addr, @desig1, @dept1, @org1, @desig2, @dept2, @org2, @SDR, @nationality, @fullnameNT, @source, @cat1, @cat2, @created_date)";

                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@fName", prospectiveList[3], "@sNAme", prospectiveList[4], "@gender", prospectiveList[5], "@honorific", prospectiveList[6],
                    "@salutation", prospectiveList[7], "@tel_num", prospectiveList[8], "@email_addr", prospectiveList[9], "@desig1", prospectiveList[10],
                    "@dept1", prospectiveList[11], "@org1", prospectiveList[12], "@desig2", prospectiveList[13], "@dept2", prospectiveList[14], "@org2", prospectiveList[15], "@SDR", prospectiveList[16],
                    "@nationality", prospectiveList[17], "@fullnameNT", prospectiveList[18], "@source", prospectiveList[23], "@cat1", prospectiveList[24], "@cat2", prospectiveList[25], "@created_date", DateTime.Now);

                string memCommandText = "INSERT INTO dbo.TBL_DONATION(Person_Id, Prospective_Id, DONATION_AMOUNT, DONATION_DT, EVENT_NAME, EVENT_TYPE_CODE, START_DT_TIME, END_DT_TIME, CREATED_DT) " +
                    "VALUES ( (SELECT MAX(person_id) FROM membership.TBL_PERSON), (SELECT MAX(person_id) FROM membership.TBL_PERSON), @dAmount, CONVERT(date,@ddate,103), @evename, @eventtypecode, " +
                    "CONVERT(datetime,@estartdate,103), CONVERT(datetime,@eenddate,103), @cdate)";

                MycmdDonations = dbhelp.CreateCommand(memCommandText, CommandType.Text, "@dAmount", prospectiveList[1], "@ddate", prospectiveList[2], "@evename", prospectiveList[22],
                    "@eventtypecode", prospectiveList[21], "@estartdate", prospectiveList[19], "@eenddate", prospectiveList[20], "@cdate", DateTime.Now);

                transcommand.Add(mycmd);
                transcommand.Add(MycmdDonations);
                result = dbhelp.ExecTrans(transcommand);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            finally
            {
                //createPros_Record();
            }
            return result;
        }

        //Prospective Modal Insert w/out events
        public int AddProspectiveNoEvent(ArrayList prospectiveList)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand MycmdDonations = new SqlCommand();

                string commandtext = "INSERT INTO membership.TBL_PERSON(First_Name, surname, Gender, Honorific, Salutation, Tel_Num, Email_Addr, Designation_1, Department_1, Organisation_1, Designation_2, Department_2, " +
                    "Organisation_2, Special_Dietary_Requirement, Nationality, Fullname_Nametags, SOURCE, CAT_1, CAT_2, Created_DT) " +
                    "VALUES(@fName, @sName, @gender, @honorific, @salutation, @tel_num, @email_addr, @desig1, @dept1, @org1, @desig2, @dept2, @org2, @SDR, @nationality, @fullnameNT, @source, @cat1, @cat2, @created_date)";

                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@fName", prospectiveList[3], "@sNAme", prospectiveList[4], "@gender", prospectiveList[5], "@honorific", prospectiveList[6],
                    "@salutation", prospectiveList[7], "@tel_num", prospectiveList[8], "@email_addr", prospectiveList[9], "@desig1", prospectiveList[10],
                    "@dept1", prospectiveList[11], "@org1", prospectiveList[12], "@desig2", prospectiveList[13], "@dept2", prospectiveList[14], "@org2", prospectiveList[15], "@SDR", prospectiveList[16],
                    "@nationality", prospectiveList[17], "@fullnameNT", prospectiveList[18], "@source", prospectiveList[23], "@cat1", prospectiveList[24], "@cat2", prospectiveList[25], "@created_date", DateTime.Now);

                string memCommandText = "INSERT INTO dbo.TBL_DONATION(Person_Id, Prospective_Id, DONATION_AMOUNT, DONATION_DT, CREATED_DT) " +
                    "VALUES ( (SELECT MAX(person_id) FROM membership.TBL_PERSON), (SELECT MAX(person_id) FROM membership.TBL_PERSON), @dAmount, CONVERT(date,@ddate,103), @cdate)";

                MycmdDonations = dbhelp.CreateCommand(memCommandText, CommandType.Text, "@dAmount", prospectiveList[1], "@ddate", prospectiveList[2], "@cDate", DateTime.Now);

                transcommand.Add(mycmd);
                transcommand.Add(MycmdDonations);
                result = dbhelp.ExecTrans(transcommand);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            finally
            {
                //createPros_Record();
            }
            return result;
        }

        //Fundraising_Management.aspx
        //getting all donors
        public DataTable getAllDonors()
        {
            string queryStr = "SELECT d.DONATION_ID, p.PERSON_ID, d.ORG_ID, d.PROSPECTIVE_ID, p.FULLNAME_NAMETAGS, d.DONATION_AMOUNT, d.DONATION_DT, d.EVENT_NAME, '(Prospective)' AS Indicator FROM membership.TBL_PERSON p " +
                "INNER JOIN dbo.TBL_DONATION d ON p.PERSON_ID = d.PERSON_ID WHERE PROSPECTIVE_ID IS NOT NULL UNION " +
                "SELECT d.DONATION_ID, p.PERSON_ID, d.ORG_ID, d.PROSPECTIVE_ID, p.FULLNAME_NAMETAGS, d.DONATION_AMOUNT, d.DONATION_DT, d.EVENT_NAME, '(Individual Associate)'  As Indicator from membership.TBL_PERSON p INNER JOIN dbo.TBL_DONATION d ON p.PERSON_ID = d.PERSON_ID " +
                "INNER JOIN membership.TBL_MEMBERSHIP m on p.person_id = m.person_id UNION " +
                "SELECT d.DONATION_ID, d.PERSON_ID, o.ORG_ID, d.PROSPECTIVE_ID, o.NAME AS FULLNAME_NAMETAGS, d.DONATION_AMOUNT, d.DONATION_DT, d.EVENT_NAME, '(Corporate Associate)' As Indicator FROM membership.TBL_ORGANISATION o INNER JOIN dbo.TBL_DONATION d ON o.ORG_ID = d.ORG_ID " +
                "INNER JOIN membership.TBL_MEMBERSHIP m on o.ORG_ID = m.ORG_ID";
            DataTable dt = dbhelp.ExecDataReader(queryStr);
            return dt;
        }

        //get Fundraising Data for edit
        //Retrieve all Person Data at Edit Modal
        public EventInfo GetFundraisingData(int donationid)
        {
            EventInfo fundraising = new EventInfo();
            DataTable dt;
            string commandtext = "SELECT DONATION_DT, DONATION_AMOUNT, EVENT_NAME, EVENT_TYPE_CODE, START_DT_TIME, END_DT_TIME FROM dbo.TBL_DONATION" +
                " WHERE DONATION_ID = @donationid";
            dt = dbhelp.ExecDataReader(commandtext, "@donationid", donationid);
            fundraising.DonationDate = DateTime.Parse(dt.Rows[0]["DONATION_DT"].ToString());
            fundraising.DonationAmt = decimal.Parse(dt.Rows[0]["DONATION_AMOUNT"].ToString());
            fundraising.EventName = dt.Rows[0]["EVENT_NAME"].ToString();
            fundraising.EventType = dt.Rows[0]["EVENT_TYPE_CODE"].ToString();
            if (dt.Rows[0]["START_DT_TIME"].ToString() != "" || dt.Rows[0]["END_DT_TIME"].ToString() != "")
            {
                fundraising.EventStartDate = DateTime.Parse(dt.Rows[0]["START_DT_TIME"].ToString());
                fundraising.EventEndDate = DateTime.Parse(dt.Rows[0]["END_DT_TIME"].ToString());
            }

            return fundraising;
        }

        //update IA without event
        public int EditIndividualDonorNoEvent(string donationamt, string donationdate, int donationid)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            //string commandtext = "UPDATE d SET d.DONATION_AMOUNT = @donation_amt, d.DONATION_DT = @donation_date, FROM dbo.TBL_DONATION d inner join membership.TBL_PERSON p ON d.PERSON_ID = p.PERSON_ID WHERE p.PERSON_ID = @PERSON_ID1 WHERE d.DONATION_ID = @donationid;";
            string commandtext = @"UPDATE d SET d.DONATION_AMOUNT = @donation_amt, d.DONATION_DT = CONVERT(date,@donation_date,103)
FROM dbo.TBL_DONATION d inner join membership.TBL_PERSON p ON d.PERSON_ID = p.PERSON_ID
WHERE d.DONATION_ID = @donationid";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@donation_amt", donationamt, "@donation_date", donationdate, "@donationid", donationid);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);
            return result;
        }
        //update IA with event
        public int EditIndividualDonorWithEvent(string donationamt, string donationdate, int donationid, string eventname, string eventtype, DateTime startdate, DateTime enddate)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            string commandtext = @"UPDATE d SET d.DONATION_AMOUNT = @donation_amt, d.DONATION_DT = CONVERT(date,@donation_date,103), d.EVENT_NAME = @eventname,
d.EVENT_TYPE_CODE = @eventtype, d.START_DT_TIME = CONVERT(date,@startdate,103), d.END_DT_TIME = CONVERT(date,@enddate,103)
FROM dbo.TBL_DONATION d inner join membership.TBL_PERSON p ON d.PERSON_ID = p.PERSON_ID
WHERE d.DONATION_ID = @donationid";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@donation_amt", donationamt, "@donation_date", donationdate, "@donationid", donationid, "@eventname", eventname, "@eventtype", eventtype,
                "@startdate", startdate, "@enddate", enddate);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);
            return result;
        }

        public int DeleteDonationRecord(int donationid)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            string commandtext = "delete from dbo.TBL_DONATION where DONATION_ID=@id;";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@id", donationid);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);

            return result;
        }

        //update CA without event
        public int EditCorporateDonorNoEvent(string donationamt, string donationdate, int donationid)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            string commandtext = @"UPDATE d SET d.DONATION_AMOUNT = @donation_amt, d.DONATION_DT = CONVERT(date,@donation_date,103)
FROM dbo.TBL_DONATION d inner join membership.TBL_ORGANISATION o ON d.ORG_ID = o.ORG_ID
WHERE d.DONATION_ID = @donationid";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@donation_amt", donationamt, "@donation_date", donationdate, "@donationid", donationid);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);
            return result;
        }
        //update CA with event
        public int EditCorporateDonorWithEvent(string donationamt, string donationdate, int donationid, string eventname, string eventtype, DateTime startdate, DateTime enddate)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            string commandtext = @"UPDATE d SET d.DONATION_AMOUNT = @donation_amt, d.DONATION_DT = CONVERT(date,@donation_date,103), d.EVENT_NAME = @eventname,
d.EVENT_TYPE_CODE = @eventtype, d.START_DT_TIME = CONVERT(date,@startdate,103), d.END_DT_TIME = CONVERT(date,@enddate,103)
FROM dbo.TBL_DONATION d inner join membership.TBL_ORGANISATION o ON d.ORG_ID = o.ORG_ID
WHERE d.DONATION_ID = @donationid";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@donation_amt", donationamt, "@donation_date", donationdate, "@donationid", donationid, "@eventname", eventname, "@eventtype", eventtype,
                "@startdate", startdate, "@enddate", enddate);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);
            return result;
        }

    }
}