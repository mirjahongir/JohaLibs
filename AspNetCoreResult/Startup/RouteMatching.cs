
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AspNetCoreResult.Startup
{
    public static class RouteMatching
    {
        #region Data
        static List<string> FullRoutes = new List<string>();
        static List<Regex> PatternRoute = new List<Regex>();
        static List<string> OcelotRoute = new List<string>();
        #endregion
        public static void AddIgnoreRoute(string route)
        {
            route = route.Trim();
            route = route.TrimStart();
            if (route.StartsWith(" "))
                route = route.Substring(1);
            if (route.StartsWith("~"))
            {
                route = route.Remove(0, 1);
                PatternRoute.Add(new Regex(@route.ToLower(), RegexOptions.Compiled | RegexOptions.IgnoreCase));
            }

            else if (route.StartsWith("/"))
                if (route.Contains("{"))
                    OcelotRoute.Add(route.ToLower());
                else
                    FullRoutes.Add(route.ToLower());
        }
        public static bool CheckRoute(string route)
        {
            route = route.ToLower();
            if (CheckFullRoute(route)) return true;
            if (CheckPatternRoute(route)) return true;
            if (CheckOcelotRoute(route)) return true;
            return false;
        }

        #region Checking
        static bool CheckFullRoute(string router)
        {
            if (FullRoutes.Contains(router)) return true;
            else return false;
        }
        static bool CheckPatternRoute(string router)
        {
            foreach (var i in PatternRoute)
            {
                var match = i.Matches(router);
                if (match.Count>0) return true;
            }
            return false;
        }
        static bool CheckOcelotRoute(string router)
        {

            var firstRoute = router.Split('/')[1];
            foreach (var i in OcelotRoute)
            {
                if (i.StartsWith("/" + firstRoute)) return true;
            }
            return false;

        }
        #endregion
    }
}
