using Newtonsoft.Json;

namespace WebBanHang.Helpers
{
    public class SessionHelpers
    {
        public static void SetUserId(HttpContext context, int userId)
        {
            SessionHelpers.SetInt(context, "UserId", userId);
        }
        public static int? GetUserId(HttpContext context)
        {
            return GetInt(context, "UserId");
        }
        public static void SetRoleName(HttpContext context, string roleName)
        {
            SessionHelpers.SetString(context, "RoleName", roleName);
        }
        public static string? GetRoleName(HttpContext context)
        {
            return GetString(context, "RoleName");
        }










        public static void SetString(HttpContext context, string key, string value)
        {
            context.Session.SetString(key, value);
        }

        public static void SetInt(HttpContext context, string key, int value)
        {
            context.Session.SetInt32(key, value);
        }
        public static int ?GetInt(HttpContext context, string key)
        {
            return context.Session.GetInt32(key);
        }

        public static string? GetString(HttpContext context, string key)
        {
            return context.Session.GetString(key);
        }
        public static void Remove(HttpContext context, string key)
        {
            context.Session.Remove(key);
        }
        public static void Clear(HttpContext context)
        {
            context.Session.Clear();
        }
    }
}
