using System.Drawing.Printing;

namespace UC.LADI.MAG.WEB.Common
{
    public class PublishViewPaths
    {
        public static string home = "Views/Home/";
        public static string events = "Views/Events/";
        public static string collections = "Views/Admin/Collection/";
        public static List<string> GetPathsAsList()
        {
            return new List<string>
            {
                home,
                events,
                collections
            };
        }
    }
}




