using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using NLN_Linen.Models;
using System.Web.Security;
using NLN_Linen.Services;

namespace NLN_Linen.Account
{
    public partial class ResetPassword : Page
    {
        LOGIN_Service oLoginService = new LOGIN_Service();
        //static string username;
        //protected string StatusMessage
        //{
        //    get;
        //    private set;
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnReset.UniqueID;
            if (!this.IsPostBack)
            {
            }
        }
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Password.Text.Trim()) && !string.IsNullOrEmpty(ConfirmPassword.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(Password.Text.Trim()) != !string.IsNullOrEmpty(ConfirmPassword.Text.Trim()))
                {
                    FailureText.Text = "Password not match.";
                    ErrorMessage.Visible = true;
                }
                else
                {
                    //updateCustomer
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    oLoginService.changePassword(ticket.Name.ToString(), Cryto.Encrypt(ConfirmPassword.Text.Trim()));

                    ticket = new FormsAuthenticationTicket(ticket.Version, ticket.Name.ToString(), DateTime.Now, DateTime.Now.AddMinutes(28800), true, ticket.UserData.ToString(), FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(ticket.Name.ToString(), true));
                }
            }
            else
            {
                FailureText.Text = "Please enter required field.";
                ErrorMessage.Visible = true;
            }
            //string code = IdentityHelper.GetCodeFromRequest(Request);
            //if (code != null)
            //{
            //    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //    var user = manager.FindByName(Email.Text);
            //    if (user == null)
            //    {
            //        ErrorMessage.Text = "No user found";
            //        return;
            //    }
            //    var result = manager.ResetPassword(user.Id, code, Password.Text);
            //    if (result.Succeeded)
            //    {
            //        Response.Redirect("~/Account/ResetPasswordConfirmation");
            //        return;
            //    }
            //    ErrorMessage.Text = result.Errors.FirstOrDefault();
            //    return;
            //}

            //ErrorMessage.Text = "An error has occurred";
        }

        protected void ConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Password.Text.Trim()) != !string.IsNullOrEmpty(ConfirmPassword.Text.Trim()))
            {
                FailureText.Text = "Password not match.";
                ErrorMessage.Visible = true;
            }
        }
    }
}