using AccountWcfV2Client.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace AccountWcfV2Client.Controllers
{
    public class HomeController : Controller
    {
        AccountServiceClient svcClient;
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {           
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string userId, string password)
        {
            AccountInfo info = new AccountInfo();
            if (ModelState.IsValid)
            {
                info.userid = userId;
                info.password = password;
                try
                {
                    using (svcClient = new AccountServiceClient())
                    {
                        if (svcClient.AuthAccount(ref info))
                        {
                            return View("LoggedIn", info);
                        }
                    }
                }
                catch (FaultException<AccountServiceFault> ex)
                {
                    HandleErrorInfo errorInfo = new HandleErrorInfo(new Exception(ex.Detail.Message), "Home", "Login");
                    return View("Error", errorInfo);
                }
            }
            ViewBag.LoginFailed = "Oops... user credential is not matched, please try again!";
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(string userId, string password, string firstName, string lastName)
        {
            AccountInfo info = new AccountInfo();
            if (ModelState.IsValid)
            {
                info.userid = userId;
                info.password = password;
                info.firstname = firstName;
                info.lastname = lastName;
                try {
                    using (svcClient = new AccountServiceClient())
                    {
                        if (svcClient.CreateAccount(info))
                        {
                            return View("Login");
                        }
                    }                   
                }
                catch (FaultException<AccountServiceFault> ex)
                {
                    HandleErrorInfo errorInfo = new HandleErrorInfo(ex, "Home", "Register");
                    return View("Error", errorInfo);
                }
            }
            ViewBag.RegisterFailed =  String.Format("Oops... user id: {0}  already exists, please choose another one!", userId);
            return View();
        }
    }
}