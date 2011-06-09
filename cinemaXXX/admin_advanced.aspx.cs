
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace cinemaXXX
{


	public partial class admin_advanced : System.Web.UI.Page
	{
	
		protected void Page_Init (object sender, EventArgs e) {
			
			if (Request["edit"] != null) {
				DataMaster.getTheFamily();
				var tmpObj = DataMaster.spawnTableObject(Request["edit"]);
				var elements = tmpObj.getAll();
				
				DropDownList dropDownList = new DropDownList();
				dropDownList.AutoPostBack = true;
				dropDownList.ID = "editThis";
				dropDownList.SelectedIndexChanged += new EventHandler(advIndexChanged);
				dropDownList.Items.Add(new ListItem());
				foreach (var elementkey in elements.Keys){
					ListItem li = new ListItem();
					li.Value = elementkey.ToString();
					li.Text = elements[elementkey].ToString();
					dropDownList.Items.Add(li);
				}
				PlaceHolderSelect.Controls.Add(new LiteralControl(Request["edit"] + " "));
				PlaceHolderSelect.Controls.Add(dropDownList);
			}
		}
	
		protected void Page_Load (object sender, EventArgs e)
		{
			if (!LoginHelper.isAdmin()) {
				throw new Exception("Go away!");
			}
			
			advAdminButtons.Visible = false;
			
			DataMaster.getTheFamily();

			foreach(var tablename in DataMaster.family.Keys) {				
				HtmlAnchor anchor = new HtmlAnchor();

				anchor.HRef = "/admin_advanced.aspx?edit=" + tablename;
				anchor.InnerHtml = tablename;
								
				PlaceHolderNavigation.Controls.Add(anchor);
				PlaceHolderNavigation.Controls.Add(new LiteralControl(" "));
			}

			
			if (Page.IsPostBack == true) {
				PlaceHolderEdit.Controls.Clear();
				var tmpObj = DataMaster.spawnTableObject(Request["edit"]);
				if (((DropDownList)PlaceHolderSelect.FindControl("editThis")).SelectedValue != "") {
					tmpObj.id = Convert.ToInt32(((DropDownList)PlaceHolderSelect.FindControl("editThis")).SelectedValue.ToString());
					tmpObj.get();
					tmpObj.createPlaceHolderTable(PlaceHolderEdit);
					advAdminButtons.Visible = true;
				}
			}
		}
		
		protected void advIndexChanged (object sender, EventArgs e) {
			PlaceHolderEdit.Controls.Clear();
			var tmpObj = DataMaster.spawnTableObject(Request["edit"]);
			if (((DropDownList)PlaceHolderSelect.FindControl("editThis")).SelectedValue != "") {
				tmpObj.id = Convert.ToInt32(((DropDownList)PlaceHolderSelect.FindControl("editThis")).SelectedValue);
				tmpObj.get();
				tmpObj.createPlaceHolderTable(PlaceHolderEdit);		
				advAdminButtons.Visible = true;
			}
		}
		
		protected void advUpdate (object sender, EventArgs e) {
			var tmpObj = DataMaster.spawnTableObject(Request["edit"].ToString());
			tmpObj.fillObjectFromPlaceHolder(PlaceHolderEdit);
			if (tmpObj.update()) {
				System.Web.HttpContext.Current.Response.Redirect("admin_advanced.aspx?edit=" + Request["edit"].ToString());
			}
		}
		
		protected void advInsert (object sender, EventArgs e) {
			var tmpObj = DataMaster.spawnTableObject(Request["edit"].ToString());
			tmpObj.fillObjectFromPlaceHolder(PlaceHolderEdit);
			if (tmpObj.insert()) {
				System.Web.HttpContext.Current.Response.Redirect("admin_advanced.aspx?edit=" + Request["edit"].ToString());
			}
		}
	}
}

