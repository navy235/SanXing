using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Mt.Core;
using SanXing.Data.Service;
using SanXing.Data.Models;
using SanXing.ViewModels;
using System.IO;
using System.Text;
using SanXing.Web.Framework.Helpers;

namespace PadCRM.Controllers
{
    public class LoginController : Controller
    {
        private IUserService UserService;

        public LoginController(
         IUserService UserService)
        {
            this.UserService = UserService;
        }

        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model, string ReturnUrl = null, bool Remember = false)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(ReturnUrl))
                    {
                        ReturnUrl = Request.QueryString["returnurl"] ?? Url.Action("index", "home");
                    }
                    string Md5Password = CheckHelper.StrToMd5(model.Password);
                    var user = UserService.GetAll()
                        .SingleOrDefault(x => x.UserName.Equals(model.UserName, StringComparison.CurrentCultureIgnoreCase)
                        && x.Password.Equals(Md5Password, StringComparison.CurrentCultureIgnoreCase));
                    if (user != null)
                    {
                        var status = user.ActiveStatus;
                        if (status == 1)
                        {
                            ViewBag.Message = null;
                            CookieHelper.SetLoginCookie(user);
                            return Redirect(ReturnUrl);
                        }
                        else
                        {

                            ViewBag.Message = "您的用户名已经被禁用如要恢复请联系你的主管";

                        }
                    }
                    else
                    {
                        ViewBag.Message = "您的用户名和密码不匹配";
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("用户:" + model.UserName + "登录失败!", ex);
                    ViewBag.Message = "服务器错误，请刷新页面重新登录";
                }
            }
            else
            {
                ViewBag.Message = "您的输入有误请确认后提交";
            }
            return View(model);
        }

        public ActionResult LogOut(string returnUrl = null)
        {
            CookieHelper.ClearCookie();
            Session["Login"] = null;
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("index", "login");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }
    }
}
