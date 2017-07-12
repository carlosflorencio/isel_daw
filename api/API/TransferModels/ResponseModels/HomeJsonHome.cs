using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.TransferModels.ResponseModels
{
    public class HomeJsonHome
    {
        public class HomeEntity    
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public static object Home(List<HomeEntity> routes)
        {
            IDictionary<string, object> exp = new ExpandoObject();
            routes.ForEach(route => {
                exp.Add(route.Name, route.Url);
            });
            return exp;
        }
    }
}