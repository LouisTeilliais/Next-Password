using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM._utils
{
    public static class CookieManager
    {
        private static List<string> _cookies = new List<string>();

        public static void SetCookies(IEnumerable<string> cookies)
        {
            _cookies.AddRange(cookies);
        }

        public static List<string> GetCookies()
        {
            return _cookies;
        }

        public static void ClearCookies()
        {
            _cookies.Clear();
        }
    }

}
