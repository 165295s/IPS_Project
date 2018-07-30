using IPS_Prototype.Class;
using IPS_Prototype.Model;
using IPS_Prototype.RetrieveClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace IPS_Prototype.DAL
{
    public class DALMembership
    {
        DbHelper dbhelp = new DbHelper();

        // Member_MemberManagement.aspx

        public DataTable GetMembershipType(int typeid)
        {

            string commandtext = "SELECT * FROM membership.TBL_MEMBERSHIP where PERSON_ID =" + typeid;
            DataTable dt = dbhelp.ExecDataReader(commandtext);

            return dt;
        }

        //private void DeleteRecord()
        //{
        //    string query = "delete from membership.TBL_PERSON_PA where PERSON_ID =1; delete from membership.TBL_PERSONAL_ASSISTANT where PA_ID=(Select PA_ID from membership.TBL_PERSON_PA where PERSON_ID=1); delete from event.TBL_EVENT_GUEST_INVITE where PERSON_ID=(Select PERSON_ID from membership.TBL_PERSON_PA where PErson_ID=1);delete" +
        //        " from TBL_PERSON wehrer Person_Id=1; ";
        //}

        //To delete Individual Associate data based on the PersonID in Member_MemberManagement.aspx
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

        //DDL Honorific
        public DataTable GetLookupSearch(int pid)
        {

            string commandtext = "SELECT HONORIFIC  FROM membership.TBL_PERSON WHERE PERSON_ID = @pid";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@pid", pid);
            return dt;
        }

        //DDL Honorific
        public DataTable GetGender(int pid)
        {
            string commandtext = "SELECT GENDER FROM membership.TBL_PERSON WHERE PERSON_ID = @pid";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@pid", pid);
            return dt;
        }

        //DDL Nationality
        public DataTable GetNationality(int pid)
        {
            string commandtext = "SELECT NATIONALITY FROM membership.TBL_PERSON WHERE PERSON_ID = @pid";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@pid", pid);
            return dt;
        }

        //To edit selected Person Data based on the current PersonID in Member_MemberManagement.aspx
        //Parameter of current PersonID is oldusername
        public int EditModalPerson(int pid, string fname, string sname, string gender, string source, string honorific, string salutation, string telnum, string email, string nationality, DateTime modified, string des1, string dep1, string org1, string des2, string dep2, string org2, string sdr, string fnametags)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

            string commandtext = "UPDATE membership.TBL_PERSON SET FIRST_NAME = @firstname, SURNAME = @surname, GENDER = @gender, SOURCE = @source, HONORIFIC = @honorific, SALUTATION = @salutation, TEL_NUM = @telnum, EMAIL_ADDR = @email, NATIONALITY = @nationality, Modified_DT = @modified_date, DESIGNATION_1 = @des1, DEPARTMENT_1 = @dep1, ORGANISATION_1 = @org1, DESIGNATION_2 = @des2, DEPARTMENT_2 = @dep2, ORGANISATION_2 = @org2, SPECIAL_DIETARY_REQUIREMENT = @sdr, FULLNAME_NAMETAGS = @fullnamenametags WHERE PERSON_ID = @pid;";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@firstname", fname, "@surname", sname, "@gender", gender, "@source", source, "@honorific", honorific, "@salutation", salutation, "@telnum", telnum, "@email", email, "@nationality", nationality, "@modified_date", modified, "@des1", des1, "@dep1", dep1, "@org1", org1, "@des2", des2, "@dep2", dep2, "@org2", org2, "@sdr", sdr, "@fullnamenametags", fnametags, "@pid", pid);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);

            return result;
        }

        //Retrieve all Person Data at Edit Modal
        //CHRIS CHANGE
        public PersonModel GetPersonData(int personid)
        {
            PersonModel person = new PersonModel();
            DataTable dt;
            string commandtext = "SELECT FIRST_NAME, SURNAME, GENDER, SOURCE, HONORIFIC, SALUTATION, TEL_NUM, EMAIL_ADDR, NATIONALITY, DESIGNATION_1, DEPARTMENT_1, ORGANISATION_1, DESIGNATION_2, DEPARTMENT_2, ORGANISATION_2, SPECIAL_DIETARY_REQUIREMENT, FULLNAME_NAMETAGS FROM membership.TBL_PERSON WHERE PERSON_ID = @personid";
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
            
            return person;
        }
        //CHRIS ADDED NEW METHOD
        public PersonModel GetCAREPData(int CAREPID)
        {
            PersonModel person = new PersonModel();
            DataTable dt;
            string commandtext = " SELECT p.FIRST_NAME, p.SURNAME, p.GENDER, p.SOURCE, p.HONORIFIC, p.SALUTATION, p.TEL_NUM, p.EMAIL_ADDR, p.NATIONALITY, p.DESIGNATION_1, p.DEPARTMENT_1, p.ORGANISATION_1, p.DESIGNATION_2, p.DEPARTMENT_2, p.ORGANISATION_2, p.SPECIAL_DIETARY_REQUIREMENT, ca.FULLNAME_NAMETAGS, ca.STATUS,CA.FACILITATOR_BRIEFED,CA.EMAIL_SENT,CA.ROLE,p.SOURCE,p.CAT_1,p.CAT_2 FROM membership.TBL_PERSON p INNER JOIN membership.TBL_ORG_CA_REP ca ON p.PERSON_ID = ca.PERSON_ID  WHERE ca.CA_REP_ID = @personid";
            dt = dbhelp.ExecDataReader(commandtext, "@personid", CAREPID);
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
            person.status = dt.Rows[0]["FULLNAME_NAMETAGS"].ToString();
            person.faciBriefed = dt.Rows[0]["FACILITATOR_BRIEFED"].ToString();
            person.emailSent = dt.Rows[0]["EMAIL_SENT"].ToString();
            person.role = dt.Rows[0]["ROLE"].ToString();
            person.source = dt.Rows[0]["SOURCE"].ToString();
            person.cat1 = dt.Rows[0]["CAT_1"].ToString();
            person.cat2 = dt.Rows[0]["CAT_2"].ToString();

            return person;
        }

        public DataTable getAllMembershipDetailPerson()
        {
            string queryStr = "SELECT p.PERSON_ID, CONCAT(p.FIRST_NAME, ' ', p.SURNAME) AS FULLNAME, p.EMAIL_ADDR, m.DONOR_TIER, m.EXPIRY_DT FROM membership.TBL_PERSON p INNER JOIN membership.TBL_MEMBERSHIP m ON p.PERSON_ID = m.PERSON_ID ";
            DataTable dt = dbhelp.ExecDataReader(queryStr);
            return dt;
        }

        public DataTable GetIndivPAInfo(int personId) {

            string commandtext = @"SELECT pa.HONORIFIC, CONCAT(pa.FIRST_NAME, ' ', pa.SURNAME) AS FULLNAME, pa.EMAIL_ADDR, pa.TEL_NUM from
membership.TBL_PERSONAL_ASSISTANT pa INNER JOIN membership.TBL_PERSON_PA ppa on pa.PA_ID = ppa.PA_ID
INNER JOIN membership.TBL_PERSON p ON p.PERSON_ID = ppa.PERSON_ID INNER JOIN membership.TBL_MEMBERSHIP m ON m.PERSON_ID = p.PERSON_ID
where p.PERSON_ID ="+personId+";";
            DataTable dt = dbhelp.ExecDataReader(commandtext);

            return dt;
        }

        public DataTable GetCAREPPAInfo(int personId)
        {
            string commandtext = "SELECT pa.HONORIFIC, CONCAT(pa.FIRST_NAME, ' ', pa.SURNAME) AS FULLNAME, pa.EMAIL_ADDR, pa.TEL_NUM from membership.TBL_PERSONAL_ASSISTANT pa INNER JOIN membership.TBL_CA_REP_PA ppa on pa.PA_ID = ppa.PA_ID INNER JOIN membership.TBL_ORG_CA_REP p ON p.PERSON_ID = ppa.PERSON_ID where p.PERSON_ID = @pid";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@pid", personId);
            return dt;
        }

        public DataTable getAllMembershipDetailOrg()
        {
            //CHRIS CHANGE: added CA_REP_ID to SQL Query , ADDED ORG ID TO THE QUERY
            // dbhelp.ExecNonQuery("UPDATE c SET c.TER_SENT_DT = @SentDate, c.TER_RECEIVED_DT = @ReceivedDate, c.TER_DETAILS = @Details, c.Modified_Date = @modified_date FROM Contribution c inner join Membership m ON c.CONTRIBUTION_ID = m.CONTRIBUTION_ID inner join Person p ON m.PERSON_ID = p.PERSON_ID WHERE p.EMAIL_ADDR = @email;", "@SentDate", sentdate, "@ReceivedDate", receiveddate, "@Details", details, "@modified_date", modified, "@email", email);
            string queryStr = "SELECT r.PERSON_ID,r.CA_REP_ID,o.NAME, m.DONOR_TIER, m.EXPIRY_DT, r.FULLNAME_NAMETAGS, r.ROLE, p.DESIGNATION_1, p.DEPARTMENT_1, p.ORGANISATION_1, o.ORG_ID FROM membership.TBL_ORGANISATION o INNER JOIN membership.TBL_MEMBERSHIP m ON o.ORG_ID = m.ORG_ID inner join membership.TBL_ORG_CA_REP r ON r.ORG_ID = o.ORG_ID inner join membership.TBL_PERSON p ON p.PERSON_ID = r.PERSON_ID";
            //string queryStr = "SELECT o.NAME, m.DonorTier, m.Expiry_Date FROM Organisation o INNER JOIN Membership m ON o.ORG_ID = m.ORG_ID";
            DataTable dt = dbhelp.ExecDataReader(queryStr);
            return dt;
        }

        public DataTable getAllMembershipRenewalDetailOrg()
        {
            //For the new Membership Renewal page (Qiqi)
            string queryStr = /*"SELECT O.ORG_ID, o.NAME, m.DONOR_TIER, m.EXPIRY_DT FROM membership.TBL_ORGANISATION o INNER JOIN membership.TBL_MEMBERSHIP m ON o.ORG_ID = m.ORG_ID";*/
                @"SELECT distinct o.ORG_ID, o.NAME, m.DONOR_TIER, m.EXPIRY_DT, c.CONTRIBUTION_STATUS, Sum(ct.Amount) as Amount_Paid, Count(ct.Amount) As Times_Paid 
FROM membership.TBL_ORGANISATION o INNER JOIN membership.TBL_MEMBERSHIP m ON o.ORG_ID = m.ORG_ID
LEFT JOIN shared.Tbl_Contribution c on m.MEMBER_ID= c.MEMBER_ID
LEFT JOIN shared.TBL_CONTRIBUTION_TRANSACTION ct on ct.CONTRIBUTION_ID =c.CONTRIBUTION_ID Group by o.ORG_ID, o.NAME, m.DONOR_TIER, m.EXPIRY_DT, c.CONTRIBUTION_STATUS";
            DataTable dt = dbhelp.ExecDataReader(queryStr);
            return dt;
        }

        public DataTable getAllMembershipRenewalDetailPerson()
        {
            //For the new Membership Renewal page (Qiqi)
            string queryStr = @"SELECT distinct p.PERSON_ID, CONCAT(p.FIRST_NAME, ' ', p.SURNAME) AS FULLNAME, p.EMAIL_ADDR, m.DONOR_TIER,
m.EXPIRY_DT, c.CONTRIBUTION_STATUS, Sum(ct.Amount) as Amount_Paid, Count(ct.Amount) As Times_Paid, c.Contribution_Id FROM membership.TBL_PERSON p INNER JOIN membership.TBL_MEMBERSHIP m ON p.PERSON_ID = m.PERSON_ID
LEFT JOIN shared.Tbl_Contribution c on m.MEMBER_ID= c.MEMBER_ID
LEFT JOIN shared.TBL_CONTRIBUTION_TRANSACTION ct on ct.CONTRIBUTION_ID =c.CONTRIBUTION_ID Group by p.PERSON_ID, p.FIRST_NAME,p.SURNAME,c.Contribution_Id
, p.EMAIL_ADDR, m.DONOR_TIER, m.EXPIRY_DT, c.CONTRIBUTION_STATUS";
            DataTable dt = dbhelp.ExecDataReader(queryStr);
            return dt;
        }

        public DataTable getAllCategory()
        {
            string commandtext = "SELECT * FROM membership.TBL_PERSON Order By PERSON_ID";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;
        }

        // Member_MemberRenewal.aspx : Individual
        public MemberInfo GetIndividualData(string IndividualID)
        {
            MemberInfo member = new MemberInfo();
            DataTable dt;
            string commandtext = "SELECT m.DONOR_TIER, m.EXPIRY_DT, CONCAT(p.FIRST_NAME, ' ', p.SURNAME) AS FULLNAME FROM membership.TBL_PERSON p INNER JOIN membership.TBL_MEMBERSHIP m ON p.PERSON_ID = m.PERSON_ID WHERE p.EMAIL_ADDR = @EMAIL_ADDR";
            dt = dbhelp.ExecDataReader(commandtext, "@EMAIL_ADDR", IndividualID);
            member.ExpiryDate = dt.Rows[0]["EXPIRY_DT"].ToString();
            member.DonorTier = dt.Rows[0]["DONOR_TIER"].ToString();
            member.IndividualName = dt.Rows[0]["FULLNAME"].ToString();
            return member;
        }

        public MemberInfo GetIndividualDataRenewal(string IndividualID)
        {
            MemberInfo member = new MemberInfo();
            DataTable dt;
            string commandtext = "SELECT m.Member_ID, m.DONOR_TIER, m.EXPIRY_DT, CONCAT(p.FIRST_NAME, ' ', p.SURNAME) AS FULLNAME FROM membership.TBL_PERSON p INNER JOIN membership.TBL_MEMBERSHIP m ON p.PERSON_ID = m.PERSON_ID WHERE p.EMAIL_ADDR = @EMAIL_ADDR";
            dt = dbhelp.ExecDataReader(commandtext, "@EMAIL_ADDR", IndividualID);
            member.ExpiryDate = dt.Rows[0]["EXPIRY_DT"].ToString();
            member.DonorTier = dt.Rows[0]["DONOR_TIER"].ToString();
            member.IndividualName = dt.Rows[0]["FULLNAME"].ToString();
            member.MemberId = int.Parse(dt.Rows[0]["Member_ID"].ToString());
            return member;
        }

        public MemberInfo GetOrganisationDataRenewal(string OrganisationID)
        {
            MemberInfo member = new MemberInfo();
            DataTable dt;
            string commandtext = "SELECT m.Member_ID, m.DONOR_TIER, m.EXPIRY_DT FROM membership.TBL_ORGANISATION o INNER JOIN membership.TBL_MEMBERSHIP m ON o.ORG_ID = m.ORG_ID WHERE o.NAME = @NAME";
            dt = dbhelp.ExecDataReader(commandtext, "@NAME", OrganisationID);
            member.ExpiryDate = dt.Rows[0]["EXPIRY_DT"].ToString();
            member.DonorTier = dt.Rows[0]["DONOR_TIER"].ToString();
            member.MemberId = int.Parse(dt.Rows[0]["Member_ID"].ToString());
            return member;
        }

        public int EditIndividual(string donortier, string expirydate, string email, DateTime modified)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            string commandtext = "UPDATE m SET m.DONOR_TIER = @donor_tier, m.EXPIRY_DT = @expiry_date, m.MODIFIED_DT = @modified_date FROM membership.TBL_MEMBERSHIP m inner join membership.TBL_PERSON p ON m.PERSON_ID = p.PERSON_ID WHERE p.EMAIL_ADDR = @EMAIL_ADDR1;";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@donor_tier", donortier, "@expiry_date", expirydate, "@EMAIL_ADDR", email, "@modified_date", modified, "@EMAIL_ADDR1", email);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);

            return result;
        }

        //To create new contribution in Member_MemberRenewal.aspx
        // For the initial DB where Contribution ID has to be in MEMBERSHIP table as 1 Contributionid only
        /*public int InsertIndividualContribution(string email, decimal amount, DateTime paymentreceiveddate, DateTime createddate, string paymentdets, string paymentmode, string paymentpurpose)
        {
            int result = 0;
            try
            {
                int newContributionId = (int)dbhelp.ExecScalar("INSERT INTO shared.TBL_CONTRIBUTION (AMOUNT, PAYMENT_RECEIVED_DT, CREATED_DT, PAYMENT_DETAILS, PAYMENT_MODE, PAYMENT_PURPOSE) output INSERTED.CONTRIBUTION_ID VALUES (@amount,@paymentreceiveddate, @createddate, @paymentdets, @paymentmode, @paymentpurpose) SELECT CAST(scope_identity() AS int)", "@amount", amount, "@paymentreceiveddate", paymentreceiveddate, "@createddate", createddate, "@paymentdets", paymentdets, "@paymentmode", paymentmode, "@paymentpurpose", paymentpurpose);
               // int newContributionId = (int)dbhelp.ExecScalar("INSERT INTO shared.TBL_CONTRIBUTION (AMOUNT, PAYMENT_RECEIVED_DT, CREATED_DT, PAYMENT_DETAILS, PAYMENT_MODE, PAYMENT_PURPOSE) output INSERTED.CONTRIBUTION_ID VALUES (@amount,@paymentreceiveddate, @createddate, @paymentdets, @paymentmode, @paymentpurpose) SELECT CAST(scope_identity() AS int)", "@amount", amount, "@paymentreceiveddate", paymentreceiveddate, "@createddate", createddate, "@paymentdets", paymentdets, "@paymentmode", paymentmode, "@paymentpurpose", paymentpurpose);
                dbhelp.ExecNonQuery("UPDATE m SET m.CONTRIBUTION_ID = @newContributionId FROM membership.TBL_MEMBERSHIP m inner join membership.TBL_PERSON p ON m.PERSON_ID = p.PERSON_ID WHERE p.EMAIL_ADDR = @email;", "@newContributionId", newContributionId, "@email", email);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return result;
        }*/

        public bool HasMemberPaid(int personId)
        {
            bool hasPaid = false;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

            string query = "Select * from  shared.TBL_CONTRIBUTION where Member_Id=(Select Member_Id from membership.TBL_MEMBERSHIP " +
                " Where Person_Id="+personId+")";
            DataTable dataTable = dbhelp.ExecDataReader(query);
            if (dataTable != null && dataTable.Rows.Count > 0)
                hasPaid = true;
            return hasPaid;
        }

        public bool HasMemberPaidOrg(int orgId)
        {
            bool hasPaid = false;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

            string query = "Select * from  shared.TBL_CONTRIBUTION where Member_Id=(Select Member_Id from membership.TBL_MEMBERSHIP " +
                " Where Org_Id =" + orgId + ")";
            DataTable dataTable = dbhelp.ExecDataReader(query);
            if (dataTable != null && dataTable.Rows.Count > 0)
                hasPaid = true;
            return hasPaid;
        }

        public bool HasMemberPartiallyPaid(int memberId)
        {
            bool hasPaid = false;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

            string query = "Select * from  shared.TBL_CONTRIBUTION where Contribution_Status='Installment' and Member_Id=" + memberId;
            DataTable dataTable = dbhelp.ExecDataReader(query);
            if (dataTable != null && dataTable.Rows.Count > 0)
                hasPaid = true;
            return hasPaid;
        }

        public float GetPartialPayment(int contibutionId)
        {
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            float amountPaid = 0;
            string query = "Select * from shared.TBL_CONTRIBUTION_Transaction where Contribution_Id=" + contibutionId ;
            DataTable dataTable = dbhelp.ExecDataReader(query);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    amountPaid += float.Parse(dataRow[1].ToString());
                }
            }
            return amountPaid;
        }

        // to insert for renewal
        public int InsertIndividualContribution(IndividualContribution individualContribution)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand mycmd1 = new SqlCommand();

                string commandtext_Contribution = "INSERT INTO shared.TBL_CONTRIBUTION (MEMBER_ID, TOTAL_AMOUNT, CONTRIBUTION_DT, CREATED_DT, CONTRIBUTION_STATUS, Contribution_Paid) VALUES ((select" +
                    " Member_Id from membership.TBL_Membership where Person_Id=@personId), @totalamount, @contributiondt, @createddate,@status,0)";
                mycmd1 = dbhelp.CreateCommand(commandtext_Contribution, CommandType.Text, "@personId", individualContribution.PersonId, "@totalamount", individualContribution.TotalAmount,
                    "@contributiondt", individualContribution.ContributionDate, "@createddate", individualContribution.ContributionCreatedDate, "@status", individualContribution.Status);

                string commandtext_contributiontransaction = "INSERT INTO shared.TBL_CONTRIBUTION_TRANSACTION (CONTRIBUTION_ID, AMOUNT, PAYMENT_RECEIVED_DT, CREATED_DT, PAYMENT_DETAILS, PAYMENT_MODE," +
                    " PAYMENT_PURPOSE) VALUES ((SELECT MAX(CONTRIBUTION_ID) FROM shared.TBL_CONTRIBUTION), @amount, @paymentreceiveddate, @createddate, @paymentdets, @paymentmode, @paymentpurpose)";
                mycmd = dbhelp.CreateCommand(commandtext_contributiontransaction, CommandType.Text, "@amount", individualContribution.Amoount, "@paymentreceiveddate",
                    individualContribution.PaymentReceivedDate, "@createddate", individualContribution.CreatedDate, "@paymentdets", individualContribution.PaymentDetails,
                    "@paymentmode", individualContribution.PaymentMode, "@paymentpurpose", individualContribution.PaymentPurpose);

               //bhelp.ExecNonQuery("UPDATE c SET c.CONTRIBUTION_PAID = CONTRIBUTION_PAID + @amount FROM shared.TBL_CONTRIBUTION c inner join shared.TBL_CONTRIBUTION_TRANSACTION ct ON c.CONTRIBUTION_ID = ct.CONTRIBUTION_ID");

                transcommand.Add(mycmd1);
                transcommand.Add(mycmd);

                result = dbhelp.ExecTrans(transcommand);
                UpdatePaymentStatus(individualContribution);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return result;
        }

        //To create new contribution in Member_MemberRenewal.aspx
        public int InsertOrganisationContribution(IndividualContribution individualContribution)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand mycmd1 = new SqlCommand();

                string commandtext_Contribution = "INSERT INTO shared.TBL_CONTRIBUTION (MEMBER_ID, TOTAL_AMOUNT, CONTRIBUTION_DT, CREATED_DT, CONTRIBUTION_STATUS) VALUES ((select" +
                    " Member_Id from membership.TBL_Membership where Org_Id=@orgId), @totalamount, @contributiondt, @createddate,@status)";
                mycmd1 = dbhelp.CreateCommand(commandtext_Contribution, CommandType.Text, "@orgId", individualContribution.OrgId, "@totalamount", individualContribution.TotalAmount,
                    "@contributiondt", individualContribution.ContributionDate, "@createddate", individualContribution.ContributionCreatedDate, "@status", individualContribution.Status);

                string commandtext_contributiontransaction = "INSERT INTO shared.TBL_CONTRIBUTION_TRANSACTION (CONTRIBUTION_ID, AMOUNT, PAYMENT_RECEIVED_DT, CREATED_DT, PAYMENT_DETAILS, PAYMENT_MODE," +
                    " PAYMENT_PURPOSE) VALUES ((SELECT MAX(CONTRIBUTION_ID) FROM shared.TBL_CONTRIBUTION), @amount, @paymentreceiveddate, @createddate, @paymentdets, @paymentmode, @paymentpurpose)";
                mycmd = dbhelp.CreateCommand(commandtext_contributiontransaction, CommandType.Text, "@amount", individualContribution.Amoount, "@paymentreceiveddate",
                    individualContribution.PaymentReceivedDate, "@createddate", individualContribution.CreatedDate, "@paymentdets", individualContribution.PaymentDetails,
                    "@paymentmode", individualContribution.PaymentMode, "@paymentpurpose", individualContribution.PaymentPurpose);

                dbhelp.ExecNonQuery(string.Format("UPDATE c SET c.CONTRIBUTION_PAID = CONTRIBUTION_PAID + {0} FROM shared.TBL_CONTRIBUTION Where Contribution_Id={1}", individualContribution.Amoount, individualContribution.ContributionId));

                transcommand.Add(mycmd1);
                transcommand.Add(mycmd);

                result = dbhelp.ExecTrans(transcommand);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            UpdatePaymentStatus(individualContribution);
            return result;
        }

        // to insert for renewal
        public int InsertIndividualContributionInstallment(IndividualContribution individualContribution)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext_contributiontransaction = "INSERT INTO shared.TBL_CONTRIBUTION_TRANSACTION (CONTRIBUTION_ID, AMOUNT, PAYMENT_RECEIVED_DT, CREATED_DT, PAYMENT_DETAILS, PAYMENT_MODE," +
                    " PAYMENT_PURPOSE) VALUES (@contributionId,@amount, @paymentreceiveddate, @createddate, @paymentdets, @paymentmode, @paymentpurpose)";
                mycmd = dbhelp.CreateCommand(commandtext_contributiontransaction, CommandType.Text, "@contributionId", individualContribution.ContributionId, "@personId", individualContribution.PersonId, "@amount", individualContribution.Amoount, "@paymentreceiveddate",
                    individualContribution.PaymentReceivedDate, "@createddate", individualContribution.CreatedDate, "@paymentdets", individualContribution.PaymentDetails,
                    "@paymentmode", individualContribution.PaymentMode, "@paymentpurpose", individualContribution.PaymentPurpose);
                transcommand.Add(mycmd);

                result = dbhelp.ExecTrans(transcommand);
                UpdatePaymentStatus(individualContribution);
               // dbhelp.ExecNonQuery(string.Format("UPDATE shared.TBL_CONTRIBUTION SET CONTRIBUTION_PAID = CONTRIBUTION_PAID + {0}  Where Contribution_Id={1}", individualContribution.Amoount, individualContribution.ContributionId));
               
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
             
            return result;
        }

        // to insert for renewal
        public int InsertIndividualContributionInstallmentOrganisation(IndividualContribution individualContribution)
        {
            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext_contributiontransaction = "INSERT INTO shared.TBL_CONTRIBUTION_TRANSACTION (CONTRIBUTION_ID, AMOUNT, PAYMENT_RECEIVED_DT, CREATED_DT, PAYMENT_DETAILS, PAYMENT_MODE," +
                    " PAYMENT_PURPOSE) VALUES ((SELECT c.CONTRIBUTION_ID FROM shared.TBL_CONTRIBUTION c INNER JOIN membership.TBL_MEMBERSHIP m ON c.MEMBER_ID = m.MEMBER_ID WHERE m.ORG_ID=@orgId)," +
                    " @amount, @paymentreceiveddate, @createddate, @paymentdets, @paymentmode, @paymentpurpose)";
                mycmd = dbhelp.CreateCommand(commandtext_contributiontransaction, CommandType.Text, "@orgId", individualContribution.OrgId, "@amount", individualContribution.Amoount, "@paymentreceiveddate",
                    individualContribution.PaymentReceivedDate, "@createddate", individualContribution.CreatedDate, "@paymentdets", individualContribution.PaymentDetails,
                    "@paymentmode", individualContribution.PaymentMode, "@paymentpurpose", individualContribution.PaymentPurpose);
                transcommand.Add(mycmd);

                result = dbhelp.ExecTrans(transcommand);
                dbhelp.ExecNonQuery("UPDATE c SET c.CONTRIBUTION_PAID = CONTRIBUTION_PAID + @amount FROM shared.TBL_CONTRIBUTION c inner join shared.TBL_CONTRIBUTION_TRANSACTION ct ON c.CONTRIBUTION_ID = ct.CONTRIBUTION_ID");
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            //  UpdatePaymentStatus(individualContribution);
            return result;
        }

        public void UpdatePaymentStatus(IndividualContribution individualContribution)
        {
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            try
            {
                string commandtext_Contribution =string.Format( @"SELECT top 1  ct.TOTAL_AMOUNT, Sum(c.AMOUNT) as Total_Paid from 
            shared.TBL_CONTRIBUTION_TRANSACTION c INNER JOIN shared.TBL_CONTRIBUTION ct
        on c.CONTRIBUTION_ID = ct.CONTRIBUTION_ID where ct.Contribution_Id={1} and ct.Member_Id = (select Member_Id from membership.TBL_Membership
          where Person_Id = {0}) group by ct.TOTAL_AMOUNT, ct.CONTRIBUTION_ID order by ct.CONTRIBUTION_ID desc", individualContribution.PersonId,  individualContribution.ContributionId);
                mycmd = dbhelp.CreateCommand(commandtext_Contribution, CommandType.Text);
                transcommand.Add(mycmd);
                mycmd.Connection = dbhelp._conn;
                dbhelp._conn.Open();
                var dataReader = mycmd.ExecuteReader();
                individualContribution.Status = "Installment";
                if (dataReader!=null && dataReader.HasRows)
                {
                    dataReader.Read();
                    var TotalAmount = dataReader.GetDecimal(0);
                    var paidAmount = dataReader.GetDecimal(1);
                    if (paidAmount >= TotalAmount)
                        individualContribution.Status = "Full";
                    dataReader.Close();
                }
               
                string newQuery = string.Format("update shared.TBL_CONTRIBUTION set CONTRIBUTION_STATUS ='{0}', Contribution_Paid=Contribution_Paid+{1} Where Member_Id=(select Member_Id from membership.TBL_Membership " +
                    " where Person_Id={3}) and Contribution_Id={2}", individualContribution.Status, individualContribution.Amoount, individualContribution.ContributionId, individualContribution.PersonId);
                mycmd.CommandText = newQuery;
                mycmd.ExecuteNonQuery();
                dbhelp._conn.Close();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
        }


        // Member_MemberTerInd.aspx : Individual
        //public int EditTerIndividual(string email, DateTime sentdate, DateTime receiveddate, string details, DateTime modified)
        //{
        //    int result = 0;
        //    try
        //    {
        //        dbhelp.ExecNonQuery("UPDATE c SET c.TER_SENT_DT = @SentDate, c.TER_RECEIVED_DT = @ReceivedDate, c.TER_DETAILS = @Details, c.MODIFIED_DT = @modified_date FROM shared.TBL_CONTRIBUTION c inner join membership.TBL_MEMBERSHIP m ON c.CONTRIBUTION_ID = m.CONTRIBUTION_ID inner join membership.TBL_PERSON p ON m.PERSON_ID = p.PERSON_ID WHERE p.EMAIL_ADDR = @email;", "@SentDate", sentdate, "@ReceivedDate", receiveddate, "@Details", details, "@modified_date", modified, "@email", email);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.WriteErrorLog(ex.ToString());
        //    }
        //    return result;
        //}

        public int EditTerIndividual(string email, DateTime sentdate, DateTime receiveddate, string details, DateTime modified, string contributionId)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            //string commandtext = "UPDATE ct SET ct.TER_SENT_DT = @tersentdate, ct.TER_RECEIVED_DT = @terreceiveddate, ct.TER_DETAILS = @terdetails, ct.MODIFIED_DT = @modified_date FROM shared.TBL_CONTRIBUTION ct inner join shared.TBL_CONTRIBUTION c" +
            //    "ON c.CONTRIBUTION_ID = ct.CONTRIBUTION_ID INNER JOIN membership.TBL_MEMBERSHIP m ON m.MEMBER_ID = c.MEMBER_ID INNER JOIN TBL_PERSON p ON p.PERSON_ID = m.PERSON_ID WHERE p.EMAIL_ADDR = @EMAIL_ADDR1;";
            string commandtext = @"UPDATE ct SET ct.TER_SENT_DT = CONVERT(date,@tersentdate,103), ct.TER_RECEIVED_DT = CONVERT(date,@terreceiveddate,103), ct.TER_DETAILS = @terdetails, ct.MODIFIED_DT = CONVERT(date,@modified_date,103)
            FROM shared.TBL_CONTRIBUTION_TRANSACTION ct inner join shared.TBL_CONTRIBUTION c
                ON c.CONTRIBUTION_ID = ct.CONTRIBUTION_ID INNER JOIN membership.TBL_MEMBERSHIP m
                ON m.MEMBER_ID = c.MEMBER_ID INNER JOIN membership.TBL_PERSON p
                ON p.PERSON_ID = m.PERSON_ID
                WHERE p.EMAIL_ADDR = @EMAIL_ADDR1 and ct.CONTRIBUTION_ID = @contributionID";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@tersentdate", sentdate, "@terreceiveddate", receiveddate, "@terdetails", details, "@modified_date", modified, "@EMAIL_ADDR", email, "@EMAIL_ADDR1", email, "@contributionID", contributionId);


            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);
            return result;
        }

        // Member_MemberTerOrg.aspx : Organisation
        public int EditTerOrganisation(string name, DateTime sentdate, DateTime receiveddate, string details, DateTime modified)
        {
            int result = 0;
            try
            {
                dbhelp.ExecNonQuery("UPDATE c SET c.TER_SENT_DT = @SentDate, c.TER_RECEIVED_DT = @ReceivedDate, c.TER_DETAILS = @Details, c.MODIFIED_DT = @modified_date FROM shared.TBL_CONTRIBUTION c inner join membership.TBL_MEMBERSHIP m ON c.CONTRIBUTION_ID = m.CONTRIBUTION_ID  inner join membership.TBL_ORGANISATION o ON m.ORG_ID = o.ORG_ID WHERE o.NAME = @orgname;", "@SentDate", sentdate, "@ReceivedDate", receiveddate, "@Details", details, "@modified_date", modified, "@orgname", name);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return result;
        }


        // Organisation
        public MemberInfo GetOrgData(string OrganisationID)
        {
            MemberInfo member = new MemberInfo();
            DataTable dt;
            string commandtext = "SELECT m.DONOR_TIER, m.EXPIRY_DT FROM membership.TBL_ORGANISATION o INNER JOIN membership.TBL_MEMBERSHIP m ON o.ORG_ID = m.ORG_ID WHERE o.NAME = @NAME";
            dt = dbhelp.ExecDataReader(commandtext, "@NAME", OrganisationID);
            member.ExpiryDate = (dt.Rows[0]["EXPIRY_DT"].ToString());
            member.DonorTier = dt.Rows[0]["DONOR_TIER"].ToString();
            return member;
        }

        public int EditOrg(string donortier, string expirydate, string name, DateTime modified)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();
            SqlCommand mycmd1 = new SqlCommand();

            string commandtext = "UPDATE m SET m.DONOR_TIER = @donor_tier, m.EXPIRY_DT = @expiry_date, m.MODIFIED_DT = @modified_date FROM membership.TBL_MEMBERSHIP m inner join membership.TBL_ORGANISATION o ON m.ORG_ID = o.ORG_ID WHERE o.NAME = @NAME1;";
            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@donor_tier", donortier, "@expiry_date", expirydate, "@NAME", name, "@modified_date", modified, "@NAME1", name);
            transcommand.Add(mycmd);
            result = dbhelp.ExecTrans(transcommand);

            return result;
        }

        //not used 
        //To create new contribution in Member_MemberRenewal.aspx
        public int InsertOrgContribution(string name, decimal amount, string paymentreceiveddate, DateTime createddate, string paymentdets, string paymentmode, string paymentpurpose)
        {
            int result = 0;
            try
            {
                int newContributionId = (int)dbhelp.ExecScalar("INSERT INTO shared.TBL_CONTRIBUTION (AMOUNT, PAYMENT_RECEIVED_DT, CREATED_DT, PAYMENT_DETAILS, PAYMENT_MODE, PAYMENT_PURPOSE) output INSERTED.CONTRIBUTION_ID VALUES (@amount,@paymentreceiveddate, @createddate, @paymentdets, @paymentmode, @paymentpurpose) SELECT CAST(scope_identity() AS int)", "@amount", amount, "@paymentreceiveddate", paymentreceiveddate, "@createddate", createddate, "@paymentdets", paymentdets, "@paymentmode", paymentmode, "@paymentpurpose", paymentpurpose);
                dbhelp.ExecNonQuery("UPDATE m SET m.CONTRIBUTION_ID = @newContributionId FROM membership.TBL_MEMBERSHIP m inner join membership.TBL_ORGANISATION o ON m.ORG_ID = o.ORG_ID WHERE o.NAME = @name;", "@newContributionId", newContributionId, "@name", name);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return result;
        }

    }
}

