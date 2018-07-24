using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;
using IPS_Prototype.RetrieveClass;

namespace IPS_Prototype
{
    public partial class Events_Invite : System.Web.UI.Page
    {
        EventsDAO dao = new EventsDAO();
        List<Person> glist = new List<Person>();
        List<Person> gListOrg = new List<Person>();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Name.InnerText = Request.QueryString["Name"];
            Datetime.InnerText = Request.QueryString["Date"];
            getCheckboxOrg();

            if (CAType.Value == "IA")
            {
                Individual.Attributes.CssStyle.Add("display", "block");
                Organisation.Attributes.CssStyle.Add("display", "none");
                CACheckbox.Attributes.CssStyle.Add("display", "none");
            }
            else
            {
                Individual.Attributes.CssStyle.Add("display", "none");
                Organisation.Attributes.CssStyle.Add("display", "block");
                CACheckbox.Attributes.CssStyle.Add("display", "block");
            }

            if(Request.QueryString["Paid"].ToString() == "true")
            {
                payment.Attributes.CssStyle.Add("display", "block");
            }
            else
            {
                payment.Attributes.CssStyle.Add("display", "none");
            }

           

            if (!IsPostBack)
            {
                Session["ID"] = glist;
                Session["IDORG"] = gListOrg;
                
                bindIndiData();
                bindOrgdata();

                DataTable dt = new DataTable();
                dt = dao.GetSource();
                Source.DataSource = dt;
                Source.DataTextField = "SOURCE";
                Source.DataBind();

                DataTable cat1defualt = new DataTable();
                cat1defualt = dao.GetCat1Defualt();
                FilterCat1.DataSource = cat1defualt;
                FilterCat1.DataTextField = "CAT_1";
                FilterCat1.DataBind();

                DataTable designation = new DataTable();
                designation = dao.GetDesignation();
                FilterDesignation.DataSource = designation;
                FilterDesignation.DataTextField = "DESIGNATION_1";
                FilterDesignation.DataBind();

                DataTable cat2 = new DataTable();
                cat2 = dao.GetCat2();
                FilterCat2.DataSource = cat2;
                FilterCat2.DataTextField = "CODE_DESC";
                FilterCat2.DataBind();

                DataTable past = new DataTable();
                past = dao.GetPastEvent();
                FilterPastevent.DataSource = past;
                FilterPastevent.DataTextField = "NAME";
                FilterPastevent.DataValueField = "EVENT_ID";
                FilterPastevent.DataBind();
            }
               

                

        }

        protected void Individual_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckBoxState();
            IndividualTable.PageIndex = e.NewPageIndex;

            //rebind your gridview - GetSource(),Datasource of your GirdView
            bindIndiData();
            PopulateCheckBoxState();
           
        }

        protected void Organisation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckBoxOrgState();
            OrganisationTable.PageIndex = e.NewPageIndex;

            //rebind your gridview - GetSource(),Datasource of your GirdView
            bindOrgdata();
            PopulateOrganisationCheckBoxState();
            
        }

        protected void RetrieveTable(object sender, EventArgs e)
        {
            ArrayList org = new ArrayList();
            string name = FilterTxt.Value;
            CheckBoxList checkboxlist = (CheckBoxList)checkboxes.FindControl("CheckBoxList");
            foreach (ListItem item in checkboxlist.Items)
            {
                if (item.Selected)
                {
                    org.Add(item.Text);
                }
            }

            SaveCheckBoxOrgState();
            OrganisationTable.DataSource = dao.SearchOrganisation(org, name, int.Parse(hiddenselect.Value), int.Parse(FilterPastevent.SelectedValue.ToString()), Source.SelectedItem.Text, FilterCat1.SelectedItem.Text, FilterDesignation.Value, FilterCat2.Value);
            OrganisationTable.DataBind();
           
            PopulateOrganisationCheckBoxState();

            SaveCheckBoxState();
            IndividualTable.DataSource = dao.SearchIndividual(name, int.Parse(hiddenselect.Value), int.Parse(FilterPastevent.SelectedValue.ToString()), Source.SelectedItem.Text, FilterCat1.SelectedItem.Text, FilterDesignation.Value, FilterCat2.Value);
            IndividualTable.DataBind();
           
            PopulateCheckBoxState();

        }

        protected void IndividualCheck(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            DropDownList role = (DropDownList)row.FindControl("Role");
            if (chk.Checked)
            {

                glist = Session["ID"] as List<Person>;
                
                
                glist.Add(new Person { Email = row.Cells[3].Text, Role = role.SelectedItem.Value });
                
                chk.Checked = true;
                Session["ID"] = glist;
            }
            else
            {
                glist = Session["ID"] as List<Person>;
                
                for(int i = 0; i < glist.Count; i++)
                {
                    if(glist[i].Email == row.Cells[3].Text)
                    {
                        glist.RemoveAt(i);
                    }
                }
             
                chk.Checked = false;
                Session["ID"] = glist;
            }

            
        }

        protected void OrganisationCheck(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            DropDownList role = (DropDownList)row.FindControl("Role");
            if (chk.Checked)
            {
                gListOrg = Session["IDORG"] as List<Person>;
                
                gListOrg.Add(new Person {Email = row.Cells[3].Text, Organisation = row.Cells[4].Text, Role = role.SelectedItem.Value });
                
                chk.Checked = true;
                Session["IDORG"] = gListOrg;
            }
            else
            {
                gListOrg = Session["IDORG"] as List<Person>;
               
                for (int i = 0; i < gListOrg.Count; i++)
                {
                    if (gListOrg[i].Email == row.Cells[3].Text)
                    {
                        gListOrg.RemoveAt(i);
                    }
                }

                chk.Checked = false;
                Session["IDORG"] = gListOrg;
            }

          
        }

      protected void GuestlistBtn(object sender, EventArgs e)
        {
            ArrayList id = new ArrayList();
            ArrayList MemID = new ArrayList();
            ArrayList role = new ArrayList();
            int check = 0;
            
            
            glist = Session["ID"] as List<Person>;
            for(int i = 0; i < glist.Count; i++)
            {
                DataTable dt = new DataTable();
                dt = dao.GetGuestID(glist[i].Email.ToString());
                id.Add(dt.Rows[0]["Person_Id"]);
            }

            for (int i = 0; i < id.Count; i++)
            {
                check++;
                DataTable dt = new DataTable();
                dt = dao.GetMemberID(int.Parse(id[i].ToString()));
                MemID.Add(dt.Rows[0]["Member_Id"].ToString());
            }

            for (int i = 0; i < glist.Count; i++)
            {
                role.Add(glist[i].Role.ToString());
            }

            gListOrg = Session["IDORG"] as List<Person>;
            for(int i = 0; i < gListOrg.Count; i++)
            {
                DataTable dt = new DataTable();
                dt = dao.GetMemberOrgID(gListOrg[i].Organisation.ToString());
                MemID.Add(dt.Rows[0]["Member_Id"].ToString());
            }

            for(int i = 0; i < gListOrg.Count; i++)
            {
                check++;
                DataTable dt = new DataTable();
                dt = dao.GetGuestID(gListOrg[i].Email.ToString());
                id.Add(dt.Rows[0]["Person_Id"]);
            }

            for (int i = 0; i < gListOrg.Count; i++)
            {
                role.Add(gListOrg[i].Role.ToString());
            }

            string eventID = Request.QueryString["ID"];
            DateTime invitedate = Convert.ToDateTime(Request.QueryString["Date"].ToString());

           

            for(int i = 0; i < check; i++)
            {
                if(Request.QueryString["Paid"].ToString() == "true")
                {
                    int result = dao.AddGuestList(int.Parse(id[i].ToString().Trim()), int.Parse(eventID.Trim()), invitedate, role[i].ToString().Trim(), double.Parse(Charge.Value.Trim()), DateTime.Now, Session["name"].ToString());

                }
                else
                {
                    int result = dao.AddGuestListNotPaid(int.Parse(id[i].ToString().Trim()), int.Parse(eventID.Trim()), invitedate, role[i].ToString().Trim(), DateTime.Now, Session["name"].ToString());

                }
            }

            
            Response.Redirect("Events_GuestList.aspx?ID=" + eventID + "&Name=" + Request.QueryString["Name"].ToString() + "&Date=" + Request.QueryString["Date"].ToString());


            //foreach (GridViewRow rw in IndividualTable.Rows)
            //{
            //    CheckBox chk = (CheckBox)rw.Cells[0].Controls[1];
            //    if (chk != null && chk.Checked)
            //    {
            //        glist.Add(rw.Cells[3].Text);
            //    }
            //}

            //foreach (GridViewRow rw in OrganisationTable.Rows)
            //{
            //    CheckBox chk = (CheckBox)rw.Cells[0].Controls[1];
            //    if (chk != null && chk.Checked)
            //    {
            //        glist.Add(rw.Cells[3].Text);
            //    }
            //}

            Session.Remove("ID");

        }

        private void SaveCheckBoxState()
        {
            ArrayList individualchk = new ArrayList();
            string index = "";

            foreach (GridViewRow row in IndividualTable.Rows)
            {
                index = (string)IndividualTable.DataKeys[row.RowIndex].Value;
                bool result = ((CheckBox)row.FindControl("IndividualInvite")).Checked;

                if (Session["Selected"] != null)
                {
                    individualchk = (ArrayList)Session["Selected"];
                }

                if (result)
                {
                    if (!individualchk.Contains(index))
                    {
                        individualchk.Add(index);
                    }
                }
                else
                {
                    individualchk.Remove(index);
                }
            }
            if (individualchk != null && individualchk.Count > 0)
            {
                Session["Selected"] = individualchk;
            }
        }

        private void SaveCheckBoxOrgState()
        {
            ArrayList organisationchk = new ArrayList();
            string index = "";

            foreach (GridViewRow row in OrganisationTable.Rows)
            {
                index = (string)OrganisationTable.DataKeys[row.RowIndex].Value;
                bool result = ((CheckBox)row.FindControl("OrganisationInvite")).Checked;

                if (Session["SelectedOrganisation"] != null)
                {
                    organisationchk = (ArrayList)Session["SelectedOrganisation"];
                }

                if (result)
                {
                    if (!organisationchk.Contains(index))
                    {
                        organisationchk.Add(index);
                    }
                }
                else
                {
                    organisationchk.Remove(index);
                }
            }
            if (organisationchk != null && organisationchk.Count > 0)
            {
                Session["SelectedOrganisation"] = organisationchk;
            }
        }

        private void PopulateCheckBoxState()
        {
            ArrayList individualchk = (ArrayList)Session["Selected"];
            if (individualchk != null && individualchk.Count > 0)
            {
                foreach (GridViewRow row in IndividualTable.Rows)
                {
                    string index = (string)IndividualTable.DataKeys[row.RowIndex].Value;
                    if (individualchk.Contains(index))
                    {
                        CheckBox cbSelect = (CheckBox)row.FindControl("IndividualInvite");
                        cbSelect.Checked = true;
                    }
                }
            }
        }

        private void PopulateOrganisationCheckBoxState()
        {
            ArrayList organisationchk = (ArrayList)Session["SelectedOrganisation"];
            if (organisationchk != null && organisationchk.Count > 0)
            {
                foreach (GridViewRow row in OrganisationTable.Rows)
                {
                    string index = (string)OrganisationTable.DataKeys[row.RowIndex].Value;
                    if (organisationchk.Contains(index))
                    {
                        CheckBox cbSelect = (CheckBox)row.FindControl("OrganisationInvite");
                        cbSelect.Checked = true;
                    }
                }
            }
        }

        private void bindIndiData()
        {
            IndividualTable.DataSource = dao.GetIndividual();
            IndividualTable.DataBind();
            IndividualTable.HeaderRow.TableSection = TableRowSection.TableHeader;

          
        }

        private void bindOrgdata()
        {
            OrganisationTable.DataSource = dao.GetOrganisation();
            OrganisationTable.DataBind();
            OrganisationTable.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        private void getCheckboxOrg()
        {
            DataTable dt = new DataTable();
            dt = dao.GetOrganisationName();
            CheckBoxList cbList = new CheckBoxList();

            cbList.ID = "CheckBoxList";
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                cbList.Items.Add(new ListItem(dt.Rows[i]["Name"].ToString(), dt.Rows[i]["Name"].ToString()));
                
                
            }
            if((dt.Rows.Count % 3) == 0)
            {
                cbList.RepeatColumns = (dt.Rows.Count / 3) + 1;
            }
            else
            {
                cbList.RepeatColumns = (dt.Rows.Count / 3);
            }
            

            checkboxes.Controls.Add(cbList);

        }

        protected void RoesDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndividualTable.PageSize = int.Parse(RowsSelect.SelectedItem.ToString());
            OrganisationTable.PageSize = int.Parse(RowsSelect.SelectedItem.ToString());

            SaveCheckBoxState();
            bindIndiData();
            PopulateCheckBoxState();

            SaveCheckBoxOrgState();
            bindOrgdata();
            PopulateOrganisationCheckBoxState();
        }

        protected void FilterCat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = dao.GetCat1(Source.SelectedItem.Text);
            FilterCat1.DataSource = dt;
            FilterCat1.DataTextField = "CAT_1";
            FilterCat1.DataBind();
        }

        protected void FilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveCheckBoxState();
            bindIndiData();
            PopulateCheckBoxState();

            SaveCheckBoxOrgState();
            bindOrgdata();
            PopulateOrganisationCheckBoxState();
        }
    }
}