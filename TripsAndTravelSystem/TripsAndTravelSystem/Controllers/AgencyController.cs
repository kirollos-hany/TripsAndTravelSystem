using System;
using System.Web.Mvc;
using TripsAndTravelSystem.Models;
using TripsAndTravelSystem.Services;
using System.Threading.Tasks;
using System.Linq;
namespace TripsAndTravelSystem.Controllers
{
    public class AgencyController : Controller
    {
        private readonly TripsAndTravelDatabaseEntities dbContext = new TripsAndTravelDatabaseEntities();
        private readonly AuthorizationServices authServices = new AuthorizationServices();
        private readonly PostValidationServices postValidation = new PostValidationServices();
        private readonly ImageServices imageService = new ImageServices();
        public async Task<ActionResult> Index()
        {
            if(Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                if(await authServices.AuthroizedAgency(id))
                {
                    var agency = await dbContext.Users.FindAsync(id);
                    return View(agency);
                }
            }
            return RedirectToAction(actionName: "signout", controllerName: "user");
        }

        public async Task<ActionResult> NewPost()
        {
            if(Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if(await authServices.AuthroizedAgency(id))
                    {
                        return View();
                    }
            }
            return RedirectToAction(actionName: "signout", controllerName: "user");
        }

        [HttpPost]
        
        public async Task<ActionResult> AddNewPost(AddPostModel postInfo)
        {
            if(postInfo != null && Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                var response = postValidation.ValidatePost(postInfo);
                if(response.ErrorMessage == null)
                {
                    var post = new Post();
                    post.Destination = postInfo.Destination;
                    post.Details = postInfo.Details;
                    post.Title = postInfo.Title;
                    post.Price = postInfo.Price;
                    post.NumOfLikes = 0;
                    post.NumOfDislikes = 0;
                    post.PostDate = DateTime.Now;
                    post.TripDate = postInfo.TripDate;
                    post.TripPhoto = await imageService.SavePhoto(postInfo.TripPhoto, postInfo.PhotoExtension, true, Models.User.UserRoles.Agency.ToString(), Server);
                    post.Status = Post.PostStatus.PENDING.ToString();
                    post.AgencyId = id;
                    dbContext.Posts.Add(post);
                    await dbContext.SaveChangesAsync();
                    return Json(new AddPostResponse() { ErrorMessage="Successful", UserId = id});
                }
            }
            return Json(new AddPostResponse() { ErrorMessage = "Data not received, try again", UserId = 0 });
        }

        public async Task<ActionResult> History()
        {
            if(Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if(await authServices.AuthroizedAgency(id))
                    {
                        var posts = await Task.Run(() => dbContext.Posts.Include("User").Where(post => post.AgencyId == id).ToList());
                        return View(posts);
                    }
            }
            return RedirectToAction(actionName: "signout", controllerName: "user");
        }

        [HttpPost]
        public async Task<ActionResult> DeletePost(DeletePostModel deleteModel)
        {
            if(Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if(await authServices.AuthroizedAgency(id) || await authServices.AuthorizedAdmin(id))
                    {
                        var post = await dbContext.Posts.FindAsync(deleteModel.PostId);
                        dbContext.Posts.Remove(post);
                        await dbContext.SaveChangesAsync();
                        return Json(new EditPostResponse() { PostId = post.PostId, ErrorMessage = "Successful" });
                    }
            }
            return Json(new EditPostResponse() { PostId = 0, ErrorMessage = "Failed to delete, try again" });
        }

        [HttpPost]
        public async Task<ActionResult> EditPostTitle(EditTitleModel titleModel)
        {
            if(Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthroizedAgency(id) || await authServices.AuthorizedAdmin(id))
                    {
                        if (postValidation.ValidateTitle(titleModel.NewTitle))
                        {
                            var post = await dbContext.Posts.FindAsync(titleModel.PostId);
                            post.Title = titleModel.NewTitle;
                            await dbContext.SaveChangesAsync();
                            return Json(new EditPostResponse() { ErrorMessage ="Successful", PostId = post.PostId });
                        }
                        else
                        {
                            return Json(new EditPostResponse() { ErrorMessage = postValidation.InvalidTitle, PostId = 0 });
                        }
                    }
            }
            return Json(new EditPostResponse() { ErrorMessage = "Failed to edit title, try again", PostId = 0 });
        }
        [HttpPost]
        public async Task<ActionResult> EditPhoto(EditTripPhotoModel photoModel)
        {
            if (Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthroizedAgency(id) || await authServices.AuthorizedAdmin(id))
                    {
                           var post = await dbContext.Posts.FindAsync(photoModel.PostId);
                           await imageService.DeleteOldPhoto(post.TripPhoto);
                           post.TripPhoto = await imageService.SavePhoto(photoModel.Photo, photoModel.PhotoExtension, true, Models.User.UserRoles.Agency.ToString(), Server) ;
                            await dbContext.SaveChangesAsync();
                            return Json(new EditPostResponse() { ErrorMessage = "Successful", PostId = post.PostId });
                    }
            }
            return Json(new EditPostResponse() { ErrorMessage = "Failed to edit photo, try again", PostId = 0 });
        }

        [HttpPost]
        public async Task<ActionResult> EditPostDetails(EditDetailsModel detailsModel)
        {
            if (Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthroizedAgency(id) || await authServices.AuthorizedAdmin(id))
                    {
                        if (postValidation.ValidateDetails(detailsModel.Details))
                        {
                            var post = await dbContext.Posts.FindAsync(detailsModel.PostId);
                            post.Details = detailsModel.Details;
                            await dbContext.SaveChangesAsync();
                            return Json(new EditPostResponse() { ErrorMessage = "Successful", PostId = post.PostId });
                        }
                        else
                        {
                            return Json(new EditPostResponse() { ErrorMessage = postValidation.InvalidDetails, PostId = 0 });
                        }
                    }            
            }
            return Json(new EditPostResponse() { ErrorMessage = "Failed to edit details, try again", PostId = 0 });
        }

        [HttpPost]
        public async Task<ActionResult> EditTripDate(EditTripDateModel dateModel)
        {
            if (Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthroizedAgency(id) || await authServices.AuthorizedAdmin(id))
                    {
                            var post = await dbContext.Posts.FindAsync(dateModel.PostId);
                            post.TripDate = dateModel.TripDate;
                            await dbContext.SaveChangesAsync();
                            return Json(new EditPostResponse() { ErrorMessage = "Successful", PostId = post.PostId });
                    }
            }
            return Json(new EditPostResponse() { ErrorMessage = "Failed to edit date, try again", PostId = 0 });
        }
        [HttpPost]
        public async Task<ActionResult> EditDestination(EditDestinationModel destinationModel)
        {
            if (Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthroizedAgency(id) || await authServices.AuthorizedAdmin(id))
                    {
                        if (postValidation.ValidateDestination(destinationModel.Destination))
                        {
                            var post = await dbContext.Posts.FindAsync(destinationModel.PostId);
                            post.Destination = destinationModel.Destination;
                            await dbContext.SaveChangesAsync();
                            return Json(new EditPostResponse() { ErrorMessage = "Successful", PostId = post.PostId });
                        }
                        else
                        {
                            return Json(new EditPostResponse() { ErrorMessage = postValidation.InvalidDestination, PostId = 0 });
                        }

                    }
            }
            return Json(new EditPostResponse() { ErrorMessage = "Failed to edit destination, try again", PostId = 0 });
        }

        [HttpPost]
        public async Task<ActionResult> EditPrice(EditPriceModel priceModel)
        {
            if (Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthroizedAgency(id) || await authServices.AuthorizedAdmin(id))
                    {
                        if (postValidation.ValidatePrice(priceModel.Price))
                        {
                            var post = await dbContext.Posts.FindAsync(priceModel.PostId);
                            post.Price = priceModel.Price;
                            await dbContext.SaveChangesAsync();
                            return Json(new EditPostResponse() { ErrorMessage = "Successful", PostId = post.PostId });
                        }
                        else
                        {
                            return Json(new EditPostResponse() { ErrorMessage = postValidation.InvalidPrice, PostId = 0 });
                        }
                    }
            }
            return Json(new EditPostResponse() { ErrorMessage = "Failed to edit price, try again", PostId = 0 });
        }
    }
}