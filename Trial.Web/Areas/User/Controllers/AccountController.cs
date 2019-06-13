using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Trial.Web.Areas.User.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var user = Core.Helpers.Users.UserHelper.CurrentUser();
                if(user != null)
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public ActionResult Index(Core.Models.User.UserItem user)
        {
            try
            {
                ModelState.Remove(nameof(user.PasswordAgain));
                if (ModelState.IsValid)
                {
                    var s = Core.Helpers.Users.UserHelper.Save(user);
                    TempData["Message"] = Core.Strings.UpdateSuccess;
                }
                else
                {
                   TempData["Error"] = Core.Helpers.BaseHelper.ModelStateErrors(ModelState);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                var user = Core.Helpers.Users.UserHelper.CurrentUser();
                if (user != null)
                {
                    if (user.Role == Core.Enums.User.UserRole.Administrator)
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    else
                        return RedirectToAction("Index", "Home", new { area = "User" });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Core.Models.User.UserLoginItem user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var login = Core.Helpers.Users.UserHelper.Login(user);
                    if (login != null)
                    {
                        if (login.Role == Core.Enums.User.UserRole.Pending)
                        {
                            TempData["Error"] = Core.Strings.InvalidRole;
                            return View();
                        }
                        var roles = Core.Helpers.Users.UserHelper.GetRoles(login.Role);
                        var ticket = new FormsAuthenticationTicket(0, login.Name, DateTime.Now, DateTime.Now.AddHours(2), false, Newtonsoft.Json.JsonConvert.SerializeObject(roles));
                        var encyrptData = FormsAuthentication.Encrypt(ticket);
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encyrptData));
                        if (login.Role == Core.Enums.User.UserRole.Administrator)
                            return RedirectToAction("Index", "Home", new { area = "Admin" });

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Error"] = Core.Strings.UserNotFound;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                var user = Core.Helpers.Users.UserHelper.CurrentUser();
                if (user != null)
                {
                    if (user.Role == Core.Enums.User.UserRole.Administrator)
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    else
                        return RedirectToAction("Index", "Home", new { area = "User" });
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Core.Models.User.UserItem user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Id = Guid.NewGuid();
                    user.Role = Core.Enums.User.UserRole.Pending;
                    var s = Core.Helpers.Users.UserHelper.Save(user);
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    throw new Exception(Core.Strings.InvalidAreas);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return View(user);
        }

    }
}