using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSchool.Models
{
    public class CookieMaster
    {
        public static string getCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                string _undefined = "0";
                string CookieVal = HttpContext.Current.Request.Cookies[cookieName].Value;

                if (cookieName == "find_country")
                {
                    _undefined = "99";
                    if (CookieVal == "undefined")
                    {
                        CookieVal = _undefined;
                        setCookie("find_country", "99");
                    }
                }
                if (cookieName == "find_cityValue")
                {
                    _undefined = "1";
                    if (CookieVal == "undefine")
                    {
                        CookieVal = _undefined;
                        setCookie("find_cityValue", "1");
                    }
                }
                if (cookieName == "find_cityname")
                {
                    _undefined = "chandigarh";
                }
                return CookieVal;
            }

            if (cookieName == "find_country")
            {
                return "99";
            }
            if (cookieName == "find_cityValue")
            {
                return "1";
            }
            if (cookieName == "find_cityname")
            {
                return "chandigarh";
            }
            return "0";
        }

        public static void setCookie(string cookieName, string cookieValue)
        {
            HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
            // set the cookie's expiration date
            cookie.Expires = DateTime.Now.AddDays(10);
            // set the cookie on client's browser
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

    }
}