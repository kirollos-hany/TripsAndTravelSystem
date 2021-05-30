using System;
using System.Web.Mvc;
using TripsAndTravelSystem.Models;
using System.Threading.Tasks;
namespace TripsAndTravelSystem.Controllers
{
    public class ImageController : Controller
    {
        public async Task<ActionResult> ServeImage()
        {
            if(Request.Params["id"] != null)
            {
                using (var dbContext = new TripsAndTravelDatabaseEntities())
                {
                    try
                    {
                        var user = await dbContext.Users.FindAsync(Convert.ToInt32(Request.Params["id"]));
                        return File(user.ProfilePhoto, "image/*");
                    }
                    catch (FormatException)
                    {
                        return HttpNotFound();
                    }
                }
            }
            return HttpNotFound();
        }

        public async Task<ActionResult> ServePostImage()
        {
            if (Request.Params["id"] != null)
            {
                using (var dbContext = new TripsAndTravelDatabaseEntities())
                {
                    try
                    {
                        var post = await dbContext.Posts.FindAsync(Convert.ToInt32(Request.Params["id"]));
                        return File(post.TripPhoto, "image/*");
                    }
                    catch (FormatException)
                    {
                        return HttpNotFound();
                    }
                }
            }
            return HttpNotFound();
        }
    }
}