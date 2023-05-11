namespace Food_Ordering.Auth
{
    public class CV
    {
        public static IHttpContextAccessor _HttpContextAccessor;

        static CV()
        {
            _HttpContextAccessor = new HttpContextAccessor();
        }

        public static int? UserID()
        {
            int? UserId = null;

            if (_HttpContextAccessor.HttpContext.Session.GetString("UserId") != null)
            {
                UserId = Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetInt32("UserId"));
            }
            return UserId;
        }

        public static string? Name()
        {
            string? Name = null;

            if (_HttpContextAccessor.HttpContext.Session.GetString("Name") != null)
            {
                Name = _HttpContextAccessor.HttpContext.Session.GetString("Name").ToString();
            }
            return Name;
        }

        public static string? Email()
        {
            string? Email = null;

            if (_HttpContextAccessor.HttpContext.Session.GetString("Email") != null)
            {
                Email = _HttpContextAccessor.HttpContext.Session.GetString("Email").ToString();
            }
            return Email;
        }

        public static string? Password()
        {
            string? Password = null;

            if (_HttpContextAccessor.HttpContext.Session.GetString("Password") != null)
            {
                Password = _HttpContextAccessor.HttpContext.Session.GetString("Password").ToString();
            }
            return Password;
        }

        public static string? RoleType()
        {
            string? RoleType = null;

            if (_HttpContextAccessor.HttpContext.Session.GetString("RoleType") != null)
            {
                RoleType = _HttpContextAccessor.HttpContext.Session.GetString("RoleType").ToString();
            }
            return RoleType;
        }

        public static string? ImageUrl()
        {
            string? ImageUrl = null;

            if (_HttpContextAccessor.HttpContext.Session.GetString("ImageUrl") != null)
            {
                ImageUrl = _HttpContextAccessor.HttpContext.Session.GetString("ImageUrl").ToString();
            }
            return ImageUrl;
        }

    }
}
