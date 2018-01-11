using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using NLN_Linen.Models;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using NLN_Linen.Services;

namespace NLN_Linen.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnLogin.UniqueID;
            if (!this.IsPostBack)
            {
                if (this.Page.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("/Account/Login.aspx");
                }
            }
            //RegisterHyperLink.NavigateUrl = "Register";
            //// Enable this once you have account confirmation enabled for password reset functionality
            ////ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
        }
        protected void SetCookie(int version, string userData)// Version is Role, userData is BuCode
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(version, Username.Text, DateTime.Now, DateTime.Now.AddMinutes(28800), true, userData, FormsAuthentication.FormsCookiePath);
            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            Response.Cookies.Add(cookie);
        }
        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int userId = 0;
                int roles = 0;
                string buCode = null;
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[SP_VALIDATION_LOGIN]"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", Username.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", Cryto.Encrypt(Password.Text.Trim()));
                        cmd.Connection = con;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        userId = Convert.ToInt32(reader["UserId"]);
                        roles = Convert.ToInt32(reader["Roles"]);
                        buCode = reader["BU_CODE"].ToString();
                        con.Close();
                    }

                    switch (userId)
                    {
                        case -1:
                            FailureText.Text = "Username and/or password is incorrect.";
                            ErrorMessage.Visible = true;
                            break;
                        case -2:
                            SetCookie(roles, buCode);
                            Response.Redirect("/Account/ResetPassword");
                            break;
                        default:
                            SetCookie(roles, buCode);
                            Response.Redirect(FormsAuthentication.GetRedirectUrl(Username.Text, true));
                            break;
                    }
                }
                //// Validate the user password
                //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                //// This doen't count login failures towards account lockout
                //// To enable password failures to trigger lockout, change to shouldLockout: true
                //var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                //switch (result)
                //{
                //    case SignInStatus.Success:
                //        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                //        break;
                //    case SignInStatus.LockedOut:
                //        Response.Redirect("/Account/Lockout");
                //        break;
                //    case SignInStatus.RequiresVerification:
                //        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                //                                        Request.QueryString["ReturnUrl"],
                //                                        RememberMe.Checked),
                //                          true);
                //        break;
                //    case SignInStatus.Failure:
                //    default:
                //        FailureText.Text = "Invalid login attempt";
                //        ErrorMessage.Visible = true;
                //        break;
                //}
            }
        }

        //protected void btnUpdateChangePassword_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text))
        //    {
        //        DataTable dt = (DataTable)ViewState["dtLogin"];
        //        //updateCustomer
        //        loginService.changePassword(Username.Text, Cryto.Encrypt(txtPassword.Text));
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        sb.Append(@"<script type='text/javascript'>");
        //        sb.Append("alert('Records Updated Successfully');");
        //        sb.Append("$('#editModal').modal('hide');");
        //        sb.Append(@"</script>");
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        //        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, Username.Text, DateTime.Now, DateTime.Now.EditMinutes(28800), true, dt.Rows[0]["Role_Nam"].ToString(), FormsAuthentication.FormsCookiePath);
        //        string hash = FormsAuthentication.Encrypt(ticket);
        //        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

        //        if (ticket.IsPersistent)
        //        {
        //            cookie.Expires = ticket.Expiration;
        //        }
        //        Response.Cookies.Edit(cookie);
        //        Response.Redirect(FormsAuthentication.GetRedirectUrl(Username.Text, true));
        //    }
        //    else
        //    {
        //        FailureText.Text = "Please enter required field.";
        //        ErrorMessage.Visible = true;
        //    }
        //}

        //protected void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtPassword.Text) != !string.IsNullOrEmpty(txtConfirmPassword.Text))
        //    {
        //        FailureTextChange.Text = "Password not match.";
        //        ErrorMessageChange.Visible = true;
        //    }
        //}
    }
}