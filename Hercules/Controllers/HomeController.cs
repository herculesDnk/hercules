using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Hercules.Models;

namespace Hercules.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            if (ValidateLoginUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(users u)
        {
            if (ValidateLoginUser())
            {
                if (ModelState.IsValid)
                {
                    users user = new users();

                    if (user.ConfirmLogin(u.Username, u.Password))
                    {
                        //Open Session
                        CreateSession(u);
                        int userId = Convert.ToInt32(Session["Id"]);
                        int accountId = Convert.ToInt32(Session["CuentaPadre"]);

                        accounts ac = new accounts();
                        sites si = new sites();

                        Session["AccountId"] = ac.GetAccount(accountId);
                        Session["AccountsList"] = ac.GetAccounts(userId);
                        Session["SitesList"] = si.GetSites(userId);

                        if (GetCheckBoxForm())
                        {
                            CreateUserCookie();
                        }
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error en la identificación.");
                        return View();
                    }
                }
                else
                {
                    return View(u);
                }
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }

        //Traductor e interpretador del CheckBox del Login
        private bool GetCheckBoxForm()
        {
            var strArray = Request.Form["chkUserSession"];
            char[] delimiterChars = { ',' };
            string[] checkedvalue = strArray.Split(delimiterChars);
            var returnCheck = checkedvalue.Length > 1;
            return returnCheck;
        }

        //Verificación de las Cookies y Session del usuario para cualquier Vista.
        private bool ValidateUser()
        {
            HttpCookie userIdCookie = Request.Cookies["userIdCookie"];

            if (userIdCookie != null)
            {
                CreateSession(Convert.ToInt32(userIdCookie.Value));
                return true;
            }
            else
            {
                if (Session["Id"] != null)
                {
                    CreateSession(Convert.ToInt32(Session["Id"]));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Verificación de las Cookies y Session del usuario para el Login.
        private bool ValidateLoginUser()
        {
            HttpCookie userIdCookie = Request.Cookies["userIdCookie"];

            if (userIdCookie != null || Session["Id"] != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CreateSession(users u)
        {
            hwmdbEntities db = new hwmdbEntities();

            var query = (from dbu in db.users
                         join dba in db.accounts
                         on dbu.ParentAccount equals dba.Parent
                         where dbu.Username.Equals(u.Username) && dbu.Password.Equals(u.Password)
                         select new
                         {
                             dbu.Id,
                             dbu.Username,
                             dbu.FirstName,
                             dbu.LastName,
                             dbu.ParentAccount,
                             dba.AccountUserName
                         }).FirstOrDefault();

            if (query != null)
            {
                Session["Id"] = query.Id.ToString();
                Session["Username"] = query.Username.ToString();
                Session["Nombre"] = query.FirstName.ToString();
                Session["Apellido"] = query.LastName.ToString();
                Session["CuentaPadre"] = query.ParentAccount.ToString();
                Session["NombreCuenta"] = query.AccountUserName.ToString();
            }
        }

        private bool CreateSession(int id)
        {
            hwmdbEntities db = new hwmdbEntities();

            try
            {
                var query = (from dbu in db.users
                             join dba in db.accounts
                             on dbu.ParentAccount equals dba.Parent
                             where dbu.Id.Equals(id)
                             select new
                             {
                                 dbu.Id,
                                 dbu.Username,
                                 dbu.FirstName,
                                 dbu.LastName,
                                 dbu.ParentAccount,
                                 dba.AccountUserName
                             }).FirstOrDefault();

                if (query != null)
                {
                    Session["Id"] = query.Id.ToString();
                    Session["Username"] = query.Username.ToString();
                    Session["Nombre"] = query.FirstName.ToString();
                    Session["Apellido"] = query.LastName.ToString();
                    Session["CuentaPadre"] = query.ParentAccount.ToString();
                    Session["NombreCuenta"] = query.AccountUserName.ToString();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void CreateUserCookie()
        {
            HttpCookie userIdCookie = new HttpCookie("userIdCookie")
            {
                Value = Session["Id"].ToString(),
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Add(userIdCookie);
        }
    }
}
