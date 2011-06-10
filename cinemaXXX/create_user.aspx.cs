
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cinemaXXX
{


	public partial class create_user : System.Web.UI.Page
	{
		protected void createUser(object sender, System.EventArgs e)
		{
			// Starts with lots of input validation
			TextBox[] checks = {fname, lname, phone, email, human, passwd, passwdAgain};
			TextBox[] lenChecks = {phone, email, passwd};
			bool notFilled = false, pwNoMatch = false, toShort = false, notHuman = false;
			
			foreach (TextBox check in checks) {
				if (check.Text.Length < 1) {
					notFilled = true;
					break;
				}
			}
			
			foreach (TextBox check in lenChecks) {
				if (check.Text.Length < 4) {
					notFilled = true;
					break;
				}
			}
			
			if (passwd.Text != passwdAgain.Text) pwNoMatch = true;
			if (human.Text.ToLower() != "yes") notHuman = true;
			
			if (notFilled || pwNoMatch || toShort || notHuman) { // There are errors in input, show them
				errors.Controls.Clear();
				if (notFilled) errors.Controls.Add(new LiteralControl("You need to fill out all of the items<br />"));
				if (pwNoMatch) errors.Controls.Add(new LiteralControl("The two passwords do not match<br />"));
				if (toShort) errors.Controls.Add(new LiteralControl("Some input is to short<br />"));
				if (notHuman) errors.Controls.Add(new LiteralControl("Sorry, you are not human!<br />"));
				
			}
			else { // This is where we try to save the user data
				User user = new User();
				user.write("fname", fname.Text);
				user.write("lname", lname.Text);
				user.write("phone", phone.Text);
				user.write("email", email.Text);
				user.write("passwd", passwd.Text);
				user.insert();
				Session["UserToken"] = user;
				Response.Redirect("Default.aspx");
			}
			
		}
	}
}

