// 源文件头信息：
// <copyright file="CookieHelper.cs">
// Copyright(c)2011-2014 Maitonn.All rights reserved.
// CLR版本： 4.0.30319.18408
// 开发组织：Navy.shen
// 公司网站：http://www.dotaeye.com
// 所属工程：Mt.Web.Framework.Helpers
// 最后修改：Navy.shen
// 最后修改：2014/9/2 16:37:28
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Configuration;
using SanXing.Data.Models;

namespace SanXing.Web.Framework.Helpers
{
    public class CookieHelper
    {
        /// <summary>
        /// 清除常驻的COOKIE
        /// </summary>
        public static void ClearCookie()
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(ConfigurationManager.AppSettings["CookieName"]);
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Domain = ConfigurationManager.AppSettings["LocalDomain"];
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName, string key)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null || HttpContext.Current.Request.Cookies[cookieName].Values[key] == null)
            {
                return string.Empty;
            }
            return CheckHelper.StrUrlDecode(HttpContext.Current.Request.Cookies[cookieName].Values[key]);
        }

        public static void SetLoginCookie(User user)
        {
            HttpCookie cookie = new HttpCookie(ConfigurationManager.AppSettings["CookieName"]);
            cookie.Values.Add("ID", user.ID.ToString());
            cookie.Values.Add("Email", CheckHelper.Escape(user.Email));
            cookie.Values.Add("UserName", CheckHelper.Escape(user.UserName));
            cookie.Values.Add("Password", CheckHelper.StrToSHA1(user.Password));
            var valueStr = "ID=" + user.ID
                     + "Email=" + user.Email
                     + "UserName=" + user.UserName
                     + "Password=" + CheckHelper.StrToSHA1(user.Password);

            valueStr = valueStr.ToLower();
            var secretStr = CheckHelper.StrToSHA1(valueStr);
            cookie.Values.Add("Secret", secretStr);
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Domain = ConfigurationManager.AppSettings["LocalDomain"];
            HttpContext.Current.Response.AppendHeader("P3P: CP", "CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR");
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static bool notModify()
        {
            var cookieName = ConfigurationManager.AppSettings["CookieName"];
            string ID = GetCookie(cookieName, "ID");
            string Email = GetCookie(cookieName, "Email");
            string UserName = GetCookie(cookieName, "UserName");
            string Password = GetCookie(cookieName, "Password");
            string Secret = GetCookie(cookieName, "Secret");
            var valueStr = "ID=" + ID
                      + "Email=" + Email
                      + "UserName=" + UserName
                      + "Password=" + Password;
            valueStr = valueStr.ToLower();
            var secretStr = CheckHelper.StrToSHA1(valueStr);
            return Secret == secretStr;
        }

        public static string UserName
        {
            get
            {
                string _uid = GetCookie(ConfigurationManager.AppSettings["CookieName"], "UserName");
                if (_uid == "") { return string.Empty; }
                if (_uid.Contains(",")) { ClearCookie(); return string.Empty; }
                if (!notModify()) { return string.Empty; }
                return CheckHelper.UnEscape(_uid).Replace("'", "").ToLower();
            }
        }

        public static int UID
        {
            get
            {
                string _uid = GetCookie(ConfigurationManager.AppSettings["CookieName"], "ID");
                if (_uid == "") { return 0; }
                if (_uid.Contains(",")) { ClearCookie(); return 0; }
                if (!notModify()) { return 0; }
                int Uid;
                Int32.TryParse(_uid, out Uid);
                return Uid;
            }
        }

        public static bool IsLogin
        {
            get
            {
                return UID > 0;
            }
        }

        public static User UserInfo
        {
            get
            {
                var user = new User();
                var cookieName = ConfigurationManager.AppSettings["CookieName"];
                if (!notModify())
                {
                    return null;
                }
                user.ID = Convert.ToInt32(UID);
                user.UserName = CheckHelper.UnEscape(GetCookie(cookieName, "UserName"));
                user.Email = CheckHelper.UnEscape(GetCookie(cookieName, "Email"));
                user.Password = CheckHelper.UnEscape(GetCookie(cookieName, "Password"));
                return user;
            }
        }


    }
}
