using System.Drawing.Printing;

namespace UC.LADI.MAG.WEB.Common
{
    public class AdminViewPaths
    {
        public static string admin_home = "Views/Admin/Home/";
        public static string admin_auth = "Views/Admin/Auth/";
        public static string events = "Views/Admin/Events/";
        public static string collections = "Views/Admin/Collection/";
        public static string images = "Views/Admin/Images/";
        public static string items = "Views/Admin/Items/";

        public static List<string> GetPathsAsList()
        {
            return new List<string>
            {
                admin_home,
                events,
                collections,
                images,
                items
            };
        }
    }
}




