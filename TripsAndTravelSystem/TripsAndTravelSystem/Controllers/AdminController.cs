using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripsAndTravelSystem.Models;
using TripsAndTravelSystem.Services;
using System.Threading.Tasks;
namespace TripsAndTravelSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly TripsAndTravelDatabaseEntities dbContext = new TripsAndTravelDatabaseEntities();
        private readonly AuthorizationServices authServices = new AuthorizationServices();
        public async Task<ActionResult> Index()
        {
            if (Request.Params["id"] != null)
            {
                try
                {
                    int id = Convert.ToInt32(Request.Params["id"]);
                    if(await authServices.AuthorizedAdmin(id))
                    {
                        var admin = await dbContext.Users.FindAsync(id);
                        return View(admin);
                    }
                }
                catch (FormatException)
                {
                    return RedirectToAction(actionName: "index", controllerName: "index");
                }
            }
            return RedirectToAction(actionName: "index", controllerName: "index");
        }
    }
}