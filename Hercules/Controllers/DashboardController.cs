using Hercules.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Hercules.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (ValidateUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

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

        public ActionResult GetJSON()
        {
            hwmdbEntities db = new hwmdbEntities();

            var query = (from dbs in db.sites
                         join dbl in db.loggers
                         on dbs.LoggerID equals dbl.ID
                         select new
                         {
                             dbl.ID,
                             dbs.Address
                         });

            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}