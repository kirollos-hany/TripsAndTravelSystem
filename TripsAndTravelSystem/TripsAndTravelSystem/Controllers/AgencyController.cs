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
    public class AgencyController : Controller
    {
        private readonly TripsAndTravelDatabaseEntities dbContext = new TripsAndTravelDatabaseEntities();
        private readonly AuthorizationServices authServices = new AuthorizationServices();
        public async Task<ActionResult> Index()
        {
            if(Request.Params["id"] != null)
            {
                int id = Convert.ToInt32(Request.Params["id"]);
                if(await authServices.AuthroizedAgency(id))
                {
                    var agency = await dbContext.Users.FindAsync(id);
                    return View(agency);
                }
            }
            return RedirectToAction(actionName: "Index", controllerName: "Index");
        }
    }
}