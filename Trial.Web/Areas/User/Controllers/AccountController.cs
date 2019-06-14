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
        [Attributes.CustomAuthorize(Roles = "Default")]
        public ActionResult Index()
        {
            try
            {
                var user = (Core.Models.User.UserItem)(ViewBag.User);
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [Attributes.CustomAuthorize(Roles = "Default")]
        public ActionResult Index(Core.Models.User.UserItem user)
        {
            try
            {
                ModelState.Remove(nameof(user.PasswordAgain));
                if (ModelState.IsValid)
                {
                    var s = Core.Helpers.User.UserHelper.Save(user);
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
        [AllowAnonymous]
        public ActionResult Login()
        {
            try
            {
                var user = Core.Helpers.User.UserHelper.CurrentUser();
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
        [AllowAnonymous]
        public ActionResult Login(Core.Models.User.UserLoginItem user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var login = Core.Helpers.User.UserHelper.Login(user);
                    if (login != null)
                    {
                        if (login.Role == Core.Enums.User.UserRole.Pending)
                        {
                            TempData["Error"] = Core.Strings.InvalidRole;
                            return View();
                        }
                        var roles = Core.Helpers.User.UserHelper.GetRoles(login.Role);
                        var ticket = new FormsAuthenticationTicket(0, login.Name, DateTime.Now, DateTime.Now.AddHours(2), false, Newtonsoft.Json.JsonConvert.SerializeObject(login));
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
        [AllowAnonymous]
        public ActionResult Create()
        {
            try
            {
                var user = Core.Helpers.User.UserHelper.CurrentUser();
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
        [AllowAnonymous]
        public ActionResult Create(Core.Models.User.UserItem user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Id = Guid.NewGuid();
                    user.Role = Core.Enums.User.UserRole.Default;
                    var s = Core.Helpers.User.UserHelper.Save(user);
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