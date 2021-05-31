using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TripsAndTravelSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //increase max json length
            foreach (var factory in ValueProviderFactories.Factories)
            {
                if (factory is JsonValueProviderFactory)
                {
                    ValueProviderFactories.Factories.Remove(factory as JsonValueProviderFactory);
                    break;
                }
            }
            ValueProviderFactories.Factories.Add(new CustomJsonValueProviderFactory());
        }
    }
}
