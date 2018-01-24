using Hercules.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Hercules.Controllers
{
    public class DashboardController : Controller
    {

        hwmdbEntities db = new hwmdbEntities();
        //string SMSnumber = "56911153752";
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

        public JsonResult jsonTable()
        {


            var data = from l in db.loggers
                       join s in db.sites on l.ID equals s.LoggerID
                       join sof in db.softwarelookup on l.LoggerType.Substring(-1, 11) equals sof.firmware
                       where s.OwnerAccount == 43
                       select new
                       {
                           s.ID,
                           s.Address,
                           sof.friendlyname,
                           lastCallIn = l.LastCallIn,
                           l.LoggerSMSNumber,
                           l.LoggerSerialNumber,
                           LoggerType = l.LoggerType.Substring(-1, 11)
                       };


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Table()
        {

            return View();
        }

        //public string URI(string SMS)
        //{
        //    var url = "https://dnkenlinea.com/HWMOnline/hwmcarcgi.cgi?user=soporteandinas&pass=soporte2017&logger=" + SMS + "&period=4&export=json";
        //    return url;
        //}

        //public class temp{
        //    public String Lat { get; set; }
        //    public String Long { get; set; }
        //    public String Sms { get; set; }
        //    public String Call { get; set; }
        //    public String MLat { get; set; }
        //    public String MLong { get; set; }
        //}

        public JsonResult jsonPrueba()
        {
            var query = from s in db.sites
                        join l in db.loggers
                            on s.LoggerID equals l.ID
                        where s.ID == 305
                        select new
                        {
                            Lat = s.LatEast.ToString(),
                            Long = s.LongNorth.ToString(),
                            Sms = l.LoggerSMSNumber.ToString(),
                            Call = l.LastCallIn.ToString(),
                            MLat = l.MastLatitude.ToString(),
                            MLong = l.MastLongitude.ToString()
                        };
            return Json(query, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Detalles(int IDs)
        {
            //var query = (from s in db.sites
            //             join l in db.loggers
            //                 on s.LoggerID equals l.ID
            //             where s.ID == IDs
            //             select new 
            //             {
            //                 Lat = s.LatEast.ToString(),
            //                 Long = s.LongNorth.ToString(),
            //                 Sms = l.LoggerSMSNumber.ToString(),
            //                 Call = l.LastCallIn.ToString(),
            //                 MLat = l.MastLatitude.ToString(),
            //                 MLong = l.MastLongitude.ToString()
            //             }).ToList();

            //query.Select(l => new { lat = l.Lat });


            //Debug.WriteLine("ID " + IDs);
            //ViewData["LatEast"] = "";
            //ViewData["LongNorth"] = "";


            //ViewData["last"] = query.Select(l => new { lat = l.Lat });
            //ViewData["sms"] = query.Select(l => new { sms = l.Sms });
            ////var sms = db.loggers.Find(ID).LoggerSMSNumber;

            //Debug.WriteLine("sms bd " + ViewData["sms"]);
            //Debug.WriteLine("Latitud : " + latitud);

            //if (temp.Lat.ToString() != null && temp.Long.ToString() != null)
            //    {
            //        ViewData["LatEast"] = temp.Lat.ToString();
            //        ViewData["LongNorth"] = temp.Long.ToString();
            //    }
            //    else
            //    {
            //        ViewData["LatEast"] = temp.MLat.ToString();
            //        ViewData["LongNorth"] = temp.MLong.ToString();
            //    }


            jsonPrueba();
            

            return View();
        }

        public JsonResult jsonGrafico(string SMS)
        {
            //string sm = "56911153752";
            Debug.WriteLine("entrante " + SMS);
            var url = "https://dnkenlinea.com/HWMOnline/hwmcarcgi.cgi?user=soporteandinas&pass=soporte2017&logger=" + SMS + "&period=4&export=json";
            Debug.WriteLine("url " + url);
            string json;
                using (var wc = new WebClient())
                {
                    json = wc.DownloadString(url);


                    json.Trim();


                }

            
            return Json(json, JsonRequestBehavior.AllowGet);
        }


    }
}