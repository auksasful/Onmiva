using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Onmiva.Models;
using Onmiva.Repos;

namespace Onmiva.Controllers
{
    public class UserManagementController : Controller
    {

        UserRepository userRepository = new UserRepository();


        //Registratin Action
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //Registration POST action
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "IsEmailVerified,ActivationCode")]User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }


            bool Status = false;
            string message = "";
            //
            //Model Validation
            if (ModelState.IsValid)
            {
                #region//Email already exist or not
                var isExist = userRepository.CheckUserExists(user);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Already Exists");
                    return View(user);
                }
                #endregion
                //Generate Activation Code
                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                if (userRepository.GetUsersCount() == 0)
                {
                    user.Role = "Admin";
                }
                else {
                    user.Role = "User";
                }
                //Password Hashing
                #region Password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion
                user.IsMailVerified = false;

                //Save data to database
                #region Save to Database

                bool registerSuccess = userRepository.Register(user);
                if (registerSuccess)
                {

                    //send Email to user
                    // sendVerificationLinkEmail(user.EmailID , user.ActivationCode.ToString());
                    message = "Register Successfully Done. Account Activation Link" +
                        "has been sent to your email" + user.EmailID;
                    Status = true;
                }
                else {
                    message = "Register fail";
                    Status = false;
                }

                #endregion


            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        //verify account
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
      /*      bool Status = false;
            using (MyDatabseEntities1 dc = new MyDatabseEntities1())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; //this line i have added to avoid 
                                                                // confirm password does not match issue on save changes
                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsMailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;*/
            return View();
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        //login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }


            string message = "";

            
            var v = userRepository.Login(login);
            if (v != null)
            {
                if (string.Compare(Crypto.Hash(login.Password), v) == 0)
                {
                    FormsAuthentication.SetAuthCookie(login.EmailID, login.RememberMe);
                    FormsAuthentication.RedirectFromLoginPage(login.EmailID, login.RememberMe);
                }
                else
                {
                    message = "Invalid Credential Provided";
                }
            }
            

            ViewBag.Message = message;
            return View();
        }


        //logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "UserManagement");
        }


        //verify Email
        [NonAction]
        public bool IsEmailExist(string emailID)
        {

            return userRepository.CheckEmailExists(emailID);
        }

        //Verify Email Link
        [NonAction]
        public void sendVerificationLinkEmail(string emailID, string activationCode)
        {

            var verifyUrl = "/User/VerifyAccount" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("benuka02@gmail.com", "Dotnet Awsome"); //reblace with your email ID
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "**********";//replace with your email password
            string subject = "Your Account is Successfully Created";

            string body = "<br/><br/>We are Excited to say that your account is Successfully Created. Please click on the below link to confirm Email"
                            + "<br/><br/><a href = '" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);

        }

    }
}