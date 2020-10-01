namespace OnlineShopping
{
    public class AppEnv
    {
        public static readonly string AppName = "Online Electronics Store";

        public static readonly bool Production = true;

        public static readonly bool AdminAuthByPass = false;

        public static readonly string DBName = "ecommerce";

        public static readonly string AdminSessionKey = "AdminID";

        public static readonly string UserSessionKey = "UserID";
    }
}