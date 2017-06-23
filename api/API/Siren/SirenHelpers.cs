using Microsoft.AspNetCore.Mvc;

namespace API.Siren
{
    public static class SirenHelpers
    {
        
        public static string ToCourse(this IUrlHelper Url, string route, int id)
        {
            return Url.AbsoluteRouteUrl(route, new {id = id});
        }

        public static string ToClass(this IUrlHelper Url, string route, int id)
        {
            return Url.AbsoluteRouteUrl(route, new {id = id});
        }

        public static string ToStudent(this IUrlHelper Url, string route, int number)
        {
            return Url.AbsoluteRouteUrl(route, new {number = number});
        }

        public static string ToTeacher(this IUrlHelper Url, string route, int number)
        {
            return Url.AbsoluteRouteUrl(route, new {number = number});
        }

        public static string ToGroup(this IUrlHelper Url, string route, int id, int classId)
        {
            return Url.AbsoluteRouteUrl(route, new {id = id, classId = classId});
        }
    }
}