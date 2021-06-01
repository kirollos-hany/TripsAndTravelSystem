using System;
using System.Web.Mvc;
using TripsAndTravelSystem.Models;
using TripsAndTravelSystem.Services;
using System.Threading.Tasks;
using System.Linq;

namespace TripsAndTravelSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly TripsAndTravelDatabaseEntities dbContext = new TripsAndTravelDatabaseEntities();
        private readonly AuthorizationServices authServices = new AuthorizationServices();
        private readonly ImageServices imageServices = new ImageServices();
        public async Task<ActionResult> Index()
        {
            if (Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthorizedAdmin(id))
                    {
                        var admin = await dbContext.Users.FindAsync(id);
                        return View(admin);
                    }
            }
            return RedirectToAction(actionName: "signout", controllerName: "user");
        }

        public async Task<ActionResult> UsersPage()
        {
            if (Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthorizedAdmin(id))
                    {
                        var admin = await dbContext.Users.FindAsync(id);
                        var users = await Task.Run(() =>
                        dbContext.Users.ToList()
                        );
                        var travelers = await Task.Run(() => users.Where(user => user.UserRole.Equals(Models.User.UserRoles.Traveler.ToString())).ToList());
                        var admins = await Task.Run(() => users.Where(user => user.UserRole.Equals(Models.User.UserRoles.Admin.ToString()) && user.UserId != admin.UserId).ToList());
                        var agencies = await Task.Run(() => users.Where(user => user.UserRole.Equals(Models.User.UserRoles.Agency.ToString())).ToList());
                        ViewBag.Travelers = travelers;
                        ViewBag.Admins = admins;
                        ViewBag.Agencies = agencies;
                        return View();
                    }
            }
            return RedirectToAction(actionName: "signout", controllerName: "user");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(DeleteUserModel model)
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                if (await authServices.AuthorizedAdmin(id))
                {
                    var userToDelete = await dbContext.Users.FindAsync(model.UserId);
                    await imageServices.DeleteOldPhoto(userToDelete.ProfilePhoto);
                    await Task.Run(() => dbContext.Users.Remove(userToDelete));
                    await Task.Run(() => dbContext.Posts.RemoveRange(dbContext.Posts.Where(post => post.AgencyId == model.UserId)));
                    await Task.Run(() => dbContext.LikedPosts.RemoveRange(dbContext.LikedPosts.Where(likedPost => likedPost.TravelerId == model.UserId)));
                    await Task.Run(() => dbContext.DislikedPosts.RemoveRange(dbContext.DislikedPosts.Where(DislikedPost => DislikedPost.TravelerId == model.UserId)));
                    await Task.Run(() => dbContext.FavoritePosts.RemoveRange(dbContext.FavoritePosts.Where(favPost => favPost.TravelerId == model.UserId)));
                    await dbContext.SaveChangesAsync();
                    return Json(new DeleteUserResponse() { Message = "Delete successful", Successful = true });
                }
                else
                {
                    return Json(new DeleteUserResponse() { Successful = false, Message = "Not Authorized.." });
                }
            }
            return Json(new DeleteUserResponse() { Message = "Delete failed, id not valid", Successful = false });
        }

        public async Task<ActionResult> PostsPage()
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                if (await authServices.AuthorizedAdmin(id))
                {
                    var posts = await Task.Run(() => dbContext.Posts.Include("User").Where(post => post.Status.Equals(Post.PostStatus.ACCEPTED.ToString()) || post.Status.Equals(Post.PostStatus.DENIED.ToString())).ToList());
                    ViewBag.Posts = posts;
                    return View();
                }
            }
            return RedirectToAction(actionName: "signout", controllerName: "user");
        }

        public async Task<ActionResult> PostRequests()
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                if (await authServices.AuthorizedAdmin(id))
                {
                    var posts = await Task.Run(() => dbContext.Posts.Include("User").Where(post => post.Status.Equals(Post.PostStatus.PENDING.ToString())).ToList());
                    ViewBag.PendingPosts = posts;
                    return View();
                }
            }
            return RedirectToAction(actionName: "signout", controllerName: "user");
        }

        [HttpPost]
        public async Task<ActionResult> AcceptPost(PostRequestModel acceptModel)
        {
            if(acceptModel != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if(await authServices.AuthorizedAdmin(id))
                    {
                        var post = await dbContext.Posts.FindAsync(acceptModel.PostId);
                        post.Status = Post.PostStatus.ACCEPTED.ToString();
                        await dbContext.SaveChangesAsync();
                        return Json(new PostRequestResponse() { ErrorMessage = "Successful", PostId = post.PostId });
                    }
                    else
                    {
                        return Json(new PostRequestResponse() { ErrorMessage = "Unauthorized", PostId = 0 });
                    }
            }
            return Json(new PostRequestResponse() { ErrorMessage = "Failed to accept post, try again", PostId = 0 });
        }

        [HttpPost]
        public async Task<ActionResult> DenyPost(PostRequestModel denyModel)
        {
            if (denyModel != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthorizedAdmin(id))
                    {
                        var post = await dbContext.Posts.FindAsync(denyModel.PostId);
                        post.Status = Post.PostStatus.DENIED.ToString();
                        await dbContext.SaveChangesAsync();
                        return Json(new PostRequestResponse() { ErrorMessage = "Successful", PostId = post.PostId });
                    }
                    else
                    {
                        return Json(new PostRequestResponse() { ErrorMessage = "Unauthorized", PostId = 0 });
                    }
            }
            return Json(new PostRequestResponse() { ErrorMessage = "Failed to accept post, try again", PostId = 0 });
        }
    }
}