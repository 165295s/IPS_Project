using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;

namespace IPS_Prototype
{
    public partial class Events_GuestList : System.Web.UI.Page
    {
        EventsDAO dao = new EventsDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
                GuestListTable.DataSource = dt;
                GuestListTable.DataBind();
                GuestListTable.HeaderRow.TableSection = TableRowSection.TableHeader;

                DataTable speaker = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
                GuestListSpeaker.DataSource = speaker;
                GuestListSpeaker.DataBind();
                GuestListSpeaker.HeaderRow.TableSection = TableRowSection.TableHeader;

                DataTable guest = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
                GuestListGuest.DataSource = guest;
                GuestListGuest.DataBind();
                GuestListGuest.HeaderRow.TableSection = TableRowSection.TableHeader;

                DataTable vip = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
                GuestListVIP.DataSource = vip;
                GuestListVIP.DataBind();
                GuestListVIP.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

           

            Name.InnerText = Request.QueryString["Name"].ToString();
            Datetime.InnerText = Request.QueryString["Date"].ToString();
            

        }

        protected void SaveVIPRows (object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string Honourific = ((TextBox)GuestListVIP.Rows[row.RowIndex].Cells[1].Controls[0]).Text;
            string Fullname = ((TextBox)GuestListVIP.Rows[row.RowIndex].Cells[2].Controls[0]).Text;
            string Email = ((TextBox)GuestListVIP.Rows[row.RowIndex].Cells[3].Controls[0]).Text;
            DropDownList ddl = row.FindControl("DropDownList1") as DropDownList;
            string role = ddl.SelectedItem.ToString();
            TextBox date = row.FindControl("Invite_dt") as TextBox;
            string invite = date.Text;
            string charge = ((TextBox)GuestListVIP.Rows[row.RowIndex].Cells[6].Controls[0]).Text;
            int check = dao.UpdateAllGuest(Honourific, Fullname, Email, role, invite, Convert.ToDouble(charge));
            GuestListVIP.EditIndex = -1;
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
            GuestListVIP.DataSource = dt;
            GuestListVIP.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        protected void EditVIPRows (object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            GuestListVIP.EditIndex = row.RowIndex;
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
            GuestListVIP.DataSource = dt;
            GuestListVIP.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        protected void DeleteVIPRows(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            
            string Email = GuestListVIP.Rows[row.RowIndex].Cells[3].Text;
            int check = dao.DeleteGuest(int.Parse(Request.QueryString["ID"]), Email);
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
            GuestListVIP.DataSource = dt;
            GuestListVIP.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        protected void EditAllRows (object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            GuestListTable.EditIndex = row.RowIndex;
            DataTable dt = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt;
            GuestListTable.DataBind();

            DataTable dt1 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
            GuestListSpeaker.DataSource = dt1;
            GuestListSpeaker.DataBind();

            DataTable dt2 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
            GuestListGuest.DataSource = dt2;
            GuestListGuest.DataBind();

            DataTable dt3 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
            GuestListVIP.DataSource = dt3;
            GuestListVIP.DataBind();
        }

        protected void SaveAllRows (object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string Honourific = ((TextBox)GuestListTable.Rows[row.RowIndex].Cells[1].Controls[0]).Text;
            string Fullname = ((TextBox)GuestListTable.Rows[row.RowIndex].Cells[2].Controls[0]).Text;
            string Email = ((TextBox)GuestListTable.Rows[row.RowIndex].Cells[3].Controls[0]).Text;
            DropDownList ddl = row.FindControl("DropDownList1") as DropDownList;
            string role = ddl.SelectedItem.ToString();
            //string role = ((DropDownList)GuestListTable.Rows[row.RowIndex].Cells[4].Controls[0]).SelectedItem.ToString();
            TextBox date = row.FindControl("Invite_dt") as TextBox;
            string invite = date.Text;
            string charge = ((TextBox)GuestListTable.Rows[row.RowIndex].Cells[6].Controls[0]).Text;
            int check = dao.UpdateAllGuest(Honourific, Fullname, Email, role, invite, Convert.ToDouble(charge));
            GuestListTable.EditIndex = -1;
            DataTable dt = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt;
            GuestListTable.DataBind();

            DataTable dt1 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
            GuestListSpeaker.DataSource = dt1;
            GuestListSpeaker.DataBind();

            DataTable dt2 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
            GuestListGuest.DataSource = dt2;
            GuestListGuest.DataBind();

            DataTable dt3 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
            GuestListVIP.DataSource = dt3;
            GuestListVIP.DataBind();

        }

        protected void DeleteALLRows(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
           
            string Email = GuestListTable.Rows[row.RowIndex].Cells[3].Text;
            int check = dao.DeleteGuest(int.Parse(Request.QueryString["ID"].ToString()), Email);
            DataTable dt = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt;
            GuestListTable.DataBind();

            DataTable dt1 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
            GuestListSpeaker.DataSource = dt1;
            GuestListSpeaker.DataBind();

            DataTable dt2 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
            GuestListGuest.DataSource = dt2;
            GuestListGuest.DataBind();

            DataTable dt3 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
            GuestListVIP.DataSource = dt3;
            GuestListVIP.DataBind();
        }

        protected void SaveSpeakerRows(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string Honourific = ((TextBox)GuestListSpeaker.Rows[row.RowIndex].Cells[1].Controls[0]).Text;
            string Fullname = ((TextBox)GuestListSpeaker.Rows[row.RowIndex].Cells[2].Controls[0]).Text;
            string Email = ((TextBox)GuestListSpeaker.Rows[row.RowIndex].Cells[3].Controls[0]).Text;
            DropDownList ddl = row.FindControl("DropDownList1") as DropDownList;
            string role = ddl.SelectedItem.ToString();
            TextBox date = row.FindControl("Invite_dt") as TextBox;
            string invite = date.Text;
            string charge = ((TextBox)GuestListSpeaker.Rows[row.RowIndex].Cells[6].Controls[0]).Text;
            int check = dao.UpdateAllGuest(Honourific, Fullname, Email, role, invite, Convert.ToDouble(charge));
            GuestListSpeaker.EditIndex = -1;
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
            GuestListSpeaker.DataSource = dt;
            GuestListSpeaker.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        

        protected void EditSpeakerRows(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            GuestListSpeaker.EditIndex = row.RowIndex;
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
            GuestListSpeaker.DataSource = dt;
            GuestListSpeaker.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        protected void DeleteSpeakerRows(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string Email = GuestListSpeaker.Rows[row.RowIndex].Cells[3].Text;
            int check = dao.DeleteGuest(int.Parse(Request.QueryString["ID"]), Email);
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
            GuestListSpeaker.DataSource = dt;
            GuestListSpeaker.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        protected void SaveGuestRows(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string Honourific = ((TextBox)GuestListGuest.Rows[row.RowIndex].Cells[1].Controls[0]).Text;
            string Fullname = ((TextBox)GuestListGuest.Rows[row.RowIndex].Cells[2].Controls[0]).Text;
            string Email = ((TextBox)GuestListGuest.Rows[row.RowIndex].Cells[3].Controls[0]).Text;
            DropDownList ddl = row.FindControl("DropDownList1") as DropDownList;
            string role = ddl.SelectedItem.ToString();
            TextBox date = row.FindControl("Invite_dt") as TextBox;
            string invite = date.Text;
            string charge = ((TextBox)GuestListGuest.Rows[row.RowIndex].Cells[6].Controls[0]).Text;
            int check = dao.UpdateAllGuest(Honourific, Fullname, Email, role, invite, Convert.ToDouble(charge));
            GuestListGuest.EditIndex = -1;
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
            GuestListGuest.DataSource = dt;
            GuestListGuest.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        protected void EditGuestRows(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            GuestListGuest.EditIndex = row.RowIndex;
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
            GuestListGuest.DataSource = dt;
            GuestListGuest.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        protected void DeleteGuestRows(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string Email = GuestListGuest.Rows[row.RowIndex].Cells[3].Text;
            int check = dao.DeleteGuest(int.Parse(Request.QueryString["ID"]), Email);
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
            GuestListGuest.DataSource = dt;
            GuestListGuest.DataBind();

            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();
        }

        protected void All_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          
            GuestListTable.PageIndex = e.NewPageIndex;

            //rebind your gridview - GetSource(),Datasource of your GirdView
            DataTable dt1 = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt1;
            GuestListTable.DataBind();


        }

        protected void Speaker_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            GuestListSpeaker.PageIndex = e.NewPageIndex;

            //rebind your gridview - GetSource(),Datasource of your GirdView
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
            GuestListSpeaker.DataSource = dt;
            GuestListSpeaker.DataBind();


        }

        protected void Guest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            GuestListGuest.PageIndex = e.NewPageIndex;

            //rebind your gridview - GetSource(),Datasource of your GirdView
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
            GuestListGuest.DataSource = dt;
            GuestListGuest.DataBind();


        }

        protected void VIP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            GuestListVIP.PageIndex = e.NewPageIndex;

            //rebind your gridview - GetSource(),Datasource of your GirdView
            DataTable dt = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
            GuestListVIP.DataSource = dt;
            GuestListVIP.DataBind();


        }

        protected void RoesDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            GuestListTable.PageSize = int.Parse(RowsDdl1.SelectedItem.ToString());
            GuestListSpeaker.PageSize = int.Parse(RowsDdl1.SelectedItem.ToString());
            GuestListGuest.PageSize = int.Parse(RowsDdl1.SelectedItem.ToString());
            GuestListVIP.PageSize = int.Parse(RowsDdl1.SelectedItem.ToString());

            DataTable dt = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
            GuestListTable.DataSource = dt;
            GuestListTable.DataBind();

            DataTable dt1 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Speaker");
            GuestListSpeaker.DataSource = dt1;
            GuestListSpeaker.DataBind();

            DataTable dt2 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "Guest");
            GuestListGuest.DataSource = dt2;
            GuestListGuest.DataBind();

            DataTable dt3 = dao.GetGuestSpecific(int.Parse(Request.QueryString["ID"].ToString()), "VIP");
            GuestListVIP.DataSource = dt3;
            GuestListVIP.DataBind();
        }

        protected void ExportExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GuestListTable.AllowPaging = false;
                DataTable dt = dao.GetGuestPerson(int.Parse(Request.QueryString["ID"].ToString()));
                GuestListTable.DataSource = dt;
                GuestListTable.DataBind();

                GuestListTable.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GuestListTable.HeaderRow.Cells)
                {
                    cell.BackColor = GuestListTable.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GuestListTable.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GuestListTable.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GuestListTable.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GuestListTable.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}