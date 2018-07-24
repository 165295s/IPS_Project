using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using IPS_Prototype.Class;

namespace IPS_Prototype.DAL
{
    public class EventsDAO
    {
        DbHelper dbhelp = new DbHelper();
        public DataTable GetEvents()
        {

            string commandtext = "SELECT Name, START_DT_TIME, END_DT_TIME, Status FROM event.TBL_EVENT;";
            DataTable dt = dbhelp.ExecDataReader(commandtext);

            return dt;
        }

        public DataTable SearchOrganisation(ArrayList org, string name, int index, int eventid, string source, string cat1, string designation, string cat2)
        {
            if(org.Count > 0)
            {
                string state = "";
                if (org.Count != 0)
                {
                    state = " AND o.Name = " + "'" + org[0].ToString() + "'";

                    if (org.Count > 1)
                    {

                        for (int i = 1; i < org.Count; i++)
                        {
                            state = state + " OR o.Name = " + "'" + org[i].ToString() + "'";
                        }

                    }

                    string commandtext1 = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, o.Name FROM membership.TBL_PERSON p inner join membership.TBL_ORG_CA_REP oc ON p.Person_Id = oc.Person_Id inner join membership.TBL_ORGANISATION o ON o.Org_Id = oc.Org_Id inner join membership.TBL_MEMBERSHIP m ON m.Org_Id = o.Org_Id WHERE m.Status = @status" + state + ";";
                    DataTable dt = dbhelp.ExecDataReader(commandtext1, "@status", "Active");
                    return dt;
                }
                else
                {
                    string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, o.Name FROM membership.TBL_PERSON p inner join membership.TBL_ORG_CA_REP oc ON p.Person_Id = oc.Person_Id inner join membership.TBL_ORGANISATION o ON o.Org_Id = oc.Org_Id inner join membership.TBL_MEMBERSHIP m ON m.Org_Id = o.Org_Id WHERE m.Status = @status;";
                    DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active");
                    return dt;

                }
            }
            if(index == 0)
            {
                string commandtext1 = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, o.Name FROM membership.TBL_PERSON p inner join membership.TBL_ORG_CA_REP oc ON p.Person_Id = oc.Person_Id inner join membership.TBL_ORGANISATION o ON o.Org_Id = oc.Org_Id inner join membership.TBL_MEMBERSHIP m ON m.Org_Id = o.Org_Id WHERE m.Status = @status AND p.FULLNAME_NAMETAGS LIKE '%" + name + "%';";
                DataTable dt = dbhelp.ExecDataReader(commandtext1, "@status", "Active");
                return dt;
            }
         
            else if(index == 1)
            {
                string commandtext = "SELECT PERSON_ID FROM event.TBL_EVENT_GUEST_INVITE WHERE EVENT_ID = @eventid;";
                ArrayList arrperson = new ArrayList();
                DataTable personid = dbhelp.ExecDataReader(commandtext, "@eventid", eventid);
                DataTable person = new DataTable();
               

                for(int i = 0; i < personid.Rows.Count; i++)
                {
                    string commandtext1 = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, o.Name FROM membership.TBL_PERSON p inner join membership.TBL_ORG_CA_REP oc ON p.Person_Id = oc.Person_Id inner join membership.TBL_ORGANISATION o ON o.Org_Id = oc.Org_Id inner join membership.TBL_MEMBERSHIP m ON m.Org_Id = o.Org_Id WHERE m.Status = @status AND p.PERSON_ID = @personid;";
                    person = dbhelp.ExecDataReader(commandtext1, "@status", "Active", "@personid", personid.Rows[i]["PERSON_ID"]);
                    if(person.Rows.Count > 0)
                    {
                        person.Merge(person);
                    }
                }

                return person;
            }
            else if(index == 2)
            {
                string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, o.Name FROM membership.TBL_PERSON p inner join membership.TBL_ORG_CA_REP oc ON p.Person_Id = oc.Person_Id inner join membership.TBL_ORGANISATION o ON o.Org_Id = oc.Org_Id inner join membership.TBL_MEMBERSHIP m ON m.Org_Id = o.Org_Id WHERE m.Status = @status AND p.SOURCE = @source AND p.CAT_1 = @cat1;";
                DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active", "@source", source, "@cat1", cat1);
                return dt;
            }
            else if(index == 3)
            {
                string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, o.Name FROM membership.TBL_PERSON p inner join membership.TBL_ORG_CA_REP oc ON p.Person_Id = oc.Person_Id inner join membership.TBL_ORGANISATION o ON o.Org_Id = oc.Org_Id inner join membership.TBL_MEMBERSHIP m ON m.Org_Id = o.Org_Id WHERE m.Status = @status AND p.DESIGNATION_1 = @designation;";
                DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active", "@designation", designation);
                return dt;
            }
            else
            {
                string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, o.Name FROM membership.TBL_PERSON p inner join membership.TBL_ORG_CA_REP oc ON p.Person_Id = oc.Person_Id inner join membership.TBL_ORGANISATION o ON o.Org_Id = oc.Org_Id inner join membership.TBL_MEMBERSHIP m ON m.Org_Id = o.Org_Id WHERE m.Status = @status AND p.CAT_2 = @cat2;";
                DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active", "@cat2", cat2);
                return dt;
            }
          

            
            
        }

        public DataTable SearchIndividual(string name, int index, int eventid, string source, string cat1, string designation, string cat2)
        {
            
            if(index == 0)
            {
                string state = "";
                if (name != "")
                {
                    state = " AND p.FULLNAME_NAMETAGS LIKE '%" + name + "%'";
                    string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr FROM membership.TBL_PERSON p inner join membership.TBL_MEMBERSHIP m ON p.Person_Id = m.Person_Id WHERE m.Status = @status" + state + ";";
                    DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active");
                    return dt;
                }
                else
                {
                    string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr FROM membership.TBL_PERSON p inner join membership.TBL_MEMBERSHIP m ON p.Person_Id = m.Person_Id WHERE m.Status = @status;";
                    DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active");
                    return dt;
                }
            }
          
            else if(index == 1)
            {
                string commandtext = "SELECT PERSON_ID FROM event.TBL_EVENT_GUEST_INVITE WHERE EVENT_ID = @eventid;";
                DataTable personid = dbhelp.ExecDataReader(commandtext, "@eventid", eventid);
                DataTable person = new DataTable();
               


                for (int i = 0; i < personid.Rows.Count; i++)
                {
                    string commandtext1 = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr FROM membership.TBL_PERSON p inner join membership.TBL_MEMBERSHIP m ON p.Person_Id = m.Person_Id WHERE m.Status = @status AND p.PERSON_ID = @personid;";
                    person = dbhelp.ExecDataReader(commandtext1, "@status", "Active", "@personid", personid.Rows[i]["PERSON_ID"]);
                    if (person.Rows.Count > 0)
                    {
                        return person;
                    }
                }

                return person;
            }
            else if(index == 2)
            {
                string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr FROM membership.TBL_PERSON p inner join membership.TBL_MEMBERSHIP m ON p.Person_Id = m.Person_Id WHERE m.Status = @status AND p.SOURCE = @source AND p.CAT_1 = @cat1;";
                DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active", "@source", source, "@cat1", cat1);
                return dt;
            }
            else if(index == 3)
            {
                string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr FROM membership.TBL_PERSON p inner join membership.TBL_MEMBERSHIP m ON p.Person_Id = m.Person_Id WHERE m.Status = @status AND p.DESIGNATION_1 = @designation;";
                DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active", "@designation", designation);
                return dt;
            }
            else
            {
                string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr FROM membership.TBL_PERSON p inner join membership.TBL_MEMBERSHIP m ON p.Person_Id = m.Person_Id WHERE m.Status = @status AND p.CAT_2 = @cat2;";
                DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active", "@cat2", cat2);
                return dt;
            }
           
        }

        public DataTable GetSource()
        {
            
                string commandtext = "SELECT DISTINCT SOURCE FROM admin.TBL_CODE_SOURCE_CAT_LOOKUP;";
                DataTable dt = dbhelp.ExecDataReader(commandtext);
                return dt;
            
        }

        public DataTable GetCat1Defualt()
        {

            string commandtext = "SELECT CAT_1 FROM admin.TBL_CODE_SOURCE_CAT_LOOKUP WHERE SOURCE = 'Acad_TT';";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;

        }

        public DataTable GetCat1(string source)
        {

            string commandtext = "SELECT CAT_1 FROM admin.TBL_CODE_SOURCE_CAT_LOOKUP WHERE SOURCE = @source;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@source", source);
            return dt;

        }

        public DataTable GetDesignation()
        {

            string commandtext = "SELECT DISTINCT DESIGNATION_1 FROM membership.TBL_PERSON;";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;

        }

        public DataTable GetCat2()
        {

            string commandtext = "SELECT CODE_DESC FROM admin.TBL_CODE_LOOKUP WHERE CODE_TYPE = 'CAT2';";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;

        }

        public DataTable GetPastEvent()
        {

            string commandtext = "SELECT NAME, EVENT_ID FROM event.TBL_EVENT WHERE STATUS = 'Completed';";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;

        }



        

        public DataTable GetOrganisationName()
        {

            string commandtext = "SELECT Name FROM membership.TBL_ORGANISATION";
            DataTable dt = dbhelp.ExecDataReader(commandtext);

            return dt;
        }

        public DataTable GetIndividual()
        {

            string commandtext = "SELECT FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr FROM membership.TBL_PERSON p inner join membership.TBL_MEMBERSHIP m ON p.Person_Id = m.Person_Id WHERE m.Status = @status;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active");

            return dt;
        }

        public DataTable GetOrganisation()
        {

            string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, o.Name FROM membership.TBL_PERSON p inner join membership.TBL_ORG_CA_REP oc ON p.Person_Id = oc.Person_Id inner join membership.TBL_ORGANISATION o ON o.Org_Id = oc.Org_Id inner join membership.TBL_MEMBERSHIP m ON m.Org_Id = o.Org_Id WHERE m.Status = @status;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@status", "Active");

            return dt;
        }

        public DataTable GetEventCode()
        {

            string commandtext = "SELECT Code, Code_Desc FROM admin.TBL_CODE_LOOKUP WHERE Code_Type = @codetype;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@codetype", "EVENT");

            return dt;
        }

        public DataTable GetMemberID(int id)
        {

            string commandtext = "SELECT m.Member_Id FROM membership.TBL_PERSON p INNER JOIN membership.TBL_MEMBERSHIP m ON p.Person_Id = m.Person_Id WHERE p.Person_Id = @id;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@id", id);

            return dt;
        }

        public DataTable GetMemberOrgID(string organisation)
        {

            string commandtext = "SELECT m.Member_Id FROM membership.TBL_ORGANISATION o INNER JOIN membership.TBL_MEMBERSHIP m ON o.Org_Id = m.Org_Id WHERE o.Name = @organisation;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@organisation", organisation);

            return dt;
        }

        public DataTable GetEventID()
        {

            string commandtext = "SELECT TOP 1 Event_Id FROM event.TBL_EVENT ORDER BY Event_Id DESC;";
            DataTable dt = dbhelp.ExecDataReader(commandtext);

            return dt;
        }

        public DataTable GetSpecificEventID(string name, DateTime date)
        {
            string commandtext = "SELECT EVENT_ID FROM event.TBL_EVENT WHERE NAME = @name AND START_DT_TIME = @date;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@name", name, "@date", date);
            return dt;
        }

        public DataTable GetSpecificEventPaid(string name, DateTime date)
        {
            string commandtext = "SELECT PAID_EVENT FROM event.TBL_EVENT WHERE NAME = @name AND START_DT_TIME = @date;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@name", name, "@date", date);
            return dt;
        }

        public int AddEvent(string name, string type, string description, string venue, string paid, string personname, string fundraising, DateTime start, DateTime end, DateTime created)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext = "INSERT INTO event.TBL_EVENT (Name, START_DT_TIME, END_DT_TIME, Event_Type_Code, Description, Venue, Paid_Event, Status, Created_dt, Created_By, Fundraising) VALUES (@name, @eventdate, @eventdateEnd, @eventtype, @description, @venue, @paidevent, @status, @createddate, @createdby, @fundraising);";

                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@name", name, "@eventdate", start, "@eventdateEnd", end, "@eventtype", type, "description", description, "@venue", venue, "@paidevent", paid, "@status", "Ongoing", "createddate", created, "@createdby", personname, "@fundraising", fundraising);

                transcommand.Add(mycmd);

                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;
        }

        public DataTable GetGuestID(string email)
        {

            string commandtext = "SELECT Person_Id from membership.TBL_PERSON where Email_Addr = @email;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@email", email);

            return dt;
        }

        public int AddGuestList(int PerID, int EventID, DateTime date, string role, double charge, DateTime curDate, string createdBy)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext = "INSERT INTO event.TBL_EVENT_GUEST_INVITE (Event_Id, Person_Id, Guest_Role, Invite_DT, Event_Charge, Created_dt, Created_By) VALUES (@eventid, @personid, @guestrole, @invitedate, @eventcharge, @createddate, @createdby);";

                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@eventid", EventID, "@personid", PerID, "guestrole", role, "@invitedate", date, "@eventcharge", charge, "@createddate", curDate, "createdby", createdBy);

                transcommand.Add(mycmd);

                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;
        }

        public int AddGuestListNotPaid(int PerID, int EventID, DateTime date, string role, DateTime curDate, string createdBy)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext = "INSERT INTO event.TBL_EVENT_GUEST_INVITE (Event_Id, Person_Id, Guest_Role, Invite_DT, Created_DT, Created_By) VALUES (@eventid, @personid, @guestrole, @invitedate, @createddate, @createdby);";

                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@eventid", EventID, "@personid", PerID, "guestrole", role, "@invitedate", date, "@createddate", curDate, "createdby", createdBy);

                transcommand.Add(mycmd);

                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;
        }

        public DataTable GetGuestPerson(int id)
        {

            string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, g.Guest_Role, g.EVENT_CHARGE, g.Invite_DT from membership.TBL_PERSON p INNER JOIN event.TBL_EVENT_GUEST_INVITE g ON p.Person_Id = g.Person_Id where g.Event_Id = @id;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@id", id);

            return dt;
        }

        public DataTable GetGuestSpecific(int id, string role)
        {

            string commandtext = "SELECT p.FULLNAME_NAMETAGS AS Full_Name, p.Honorific, p.Email_Addr, g.Guest_Role, g.EVENT_CHARGE, g.Invite_DT from membership.TBL_PERSON p INNER JOIN event.TBL_EVENT_GUEST_INVITE g ON p.Person_Id = g.Person_Id where g.Event_Id = @id AND g.GUEST_ROLE = @role;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@id", id, "@role", role);

            return dt;
        }

        public int DeleteGuest(int eventid, string email)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext = "DELETE g FROM event.TBL_EVENT_GUEST_INVITE g INNER JOIN membership.TBL_PERSON p ON p.PERSON_ID = g.PERSON_ID WHERE p.EMAIL_ADDR = @email AND g.EVENT_ID = @eventid;";




                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@email", email, "@eventid", eventid);
                

                transcommand.Add(mycmd);
                

                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;

          
        }

        public int UpdateAllGuest(string honourific, string name, string email, string role, string invite, double charge)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();
                SqlCommand mycmd1 = new SqlCommand();
                DateTime dt = Convert.ToDateTime(invite);

                string commandtext = "UPDATE p SET p.HONORIFIC = @honorific, p.FULLNAME_NAMETAGS = @fullname, p.EMAIL_ADDR = @email FROM membership.TBL_PERSON p inner join event.TBL_EVENT_GUEST_INVITE g on p.PERSON_ID = g.PERSON_ID WHERE p.EMAIL_ADDR = @email1;";
                string commandtext1 = "UPDATE g SET g.GUEST_ROLE = @role, g.INVITE_DT = @date, g.EVENT_CHARGE = @charge FROM membership.TBL_PERSON p inner join event.TBL_EVENT_GUEST_INVITE g on p.PERSON_ID = g.PERSON_ID WHERE p.EMAIL_ADDR = @email;";

                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@honorific", honourific, "@fullname", name, "@email", email, "@email1", email);
                mycmd1 = dbhelp.CreateCommand(commandtext1, CommandType.Text, "@role", role, "@date", dt, "@charge", charge, "@email", email);

                transcommand.Add(mycmd);
                transcommand.Add(mycmd1);

                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;
        }


    }
}