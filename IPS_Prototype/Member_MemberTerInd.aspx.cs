using IPS_Prototype.Class;
using IPS_Prototype.DAL;
using IPS_Prototype.RetrieveClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPS_Prototype
{
    public partial class Member_MemberTerInd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IndividualID = (string)(Session["IndividualID"]);
                DALMembership dao = new DALMembership();
                MemberInfo member = new MemberInfo();
                member = dao.GetIndividualData(IndividualID);
                if (IndividualID != null)
                {
                    //If session "IndividualID" is equals null, means delete button event was triggered from Member_MemberManagement.aspx table
                    title.InnerText = "Membership > Member TER";
                    individualName.Value = member.IndividualName;
                    individualID.Value = IndividualID;
                    UserTerHeader.InnerText = "TER For " + individualName.Value;
                    sentdate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    receiveddate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }
        }

        protected void Submit_Ter(object sender, EventArgs e)
        {
            if ((string)(Session["IndividualID"]) != null)
            {
                int createindcheck = 0;
                try
              {
                var sent_date = sentdate.Value;
                var received_date = receiveddate.Value;
                var ter_details = terdetails.Value;

                if (!string.IsNullOrEmpty(sent_date) && !string.IsNullOrEmpty(received_date))
                {
                    DALMembership mem = new DALMembership();
                    createindcheck = mem.EditTerIndividual(individualID.Value, DateTime.Parse(sent_date), DateTime.Parse(received_date), ter_details, DateTime.Now);
                    if (createindcheck == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Renewed!');", true);
                    }
                }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                    }
              }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorLog(ex.ToString());
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }
        }



    }
}