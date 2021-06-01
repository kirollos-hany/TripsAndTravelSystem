using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using TripsAndTravelSystem.Models;
using TripsAndTravelSystem.Services;
namespace TripsAndTravelSystem.Controllers
{
    public class IndexController : Controller
    {
        private readonly TripsAndTravelDatabaseEntities dbContext = new TripsAndTravelDatabaseEntities();
        private readonly AuthorizationServices authServices = new AuthorizationServices();
        public async Task<ActionResult> Index()
        {
            var posts = await Task.Run(() => dbContext.Posts.Include("User").Where(post => post.Status.Equals(Post.PostStatus.ACCEPTED.ToString())).ToList());
            if (Request.Params["searchPrice"] != null)
            {
                decimal price = Convert.ToDecimal(Request.Params["searchPrice"]);
                posts = await Task.Run(() => dbContext.Posts.Include("User").Where(post => post.Price <= price && post.Status.Equals(Post.PostStatus.ACCEPTED.ToString())).ToList());
            }
            if(Request.Params["searchName"] != null)
            {
                string name = Request.Params["searchName"];
                if(name.Contains(" "))
                {
                    string[] fullname = name.Split(' ');
                    string firstName = fullname[0];
                    string lastName = fullname[1];
                    posts = await Task.Run(() => dbContext.Posts.Include("User").Where(post => post.User.FirstName.Equals(firstName) && post.User.LastName.Equals(lastName) && post.Status.Equals(Post.PostStatus.ACCEPTED.ToString())).ToList());
                }
                else
                {
                    posts = await Task.Run(() => dbContext.Posts.Include("User").Where(post => post.User.FirstName.Equals(name) && post.Status.Equals(Post.PostStatus.ACCEPTED.ToString())).ToList());
                }
              
            }
            if(Request.Params["searchDate"] != null)
            {
                DateTime date = Convert.ToDateTime(Request.Params["searchDate"]);
                posts = await Task.Run(() => dbContext.Posts.Include("User").Where(post => post.TripDate.CompareTo(date) == 0 || post.TripDate.CompareTo(date) > 0).ToList());
            }
            Dictionary<int, bool> IsLiked = new Dictionary<int, bool>();
            Dictionary<int, bool> IsDisliked = new Dictionary<int, bool>();
            Dictionary<int, bool> IsFavorite = new Dictionary<int, bool>();
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                var likedPosts = await Task.Run(() => dbContext.LikedPosts.Where(likedPost => likedPost.TravelerId == id).ToList());
                var dislikedPosts = await Task.Run(() => dbContext.DislikedPosts.Where(dislikedPost => dislikedPost.TravelerId == id).ToList());
                var favoritePosts = await Task.Run(() => dbContext.FavoritePosts.Where(favPost => favPost.TravelerId == id).ToList());
                foreach(var post in likedPosts)
                {
                    IsLiked.Add(post.PostId, true);
                }
                foreach(var post in dislikedPosts)
                {
                    IsDisliked.Add(post.PostId, true);
                }
                foreach(var favPost in favoritePosts)
                {
                    IsFavorite.Add(favPost.PostId, true);
                }
            }
            ViewBag.IsLiked = IsLiked;
            ViewBag.IsDisliked = IsDisliked;
            ViewBag.IsFavorite = IsFavorite;
            return View(posts);
        }

        [HttpPost]

        public async Task<ActionResult> LikePost(TravelerActionModel likeModel)
        {
            if (Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthroizedTraveler(id))
                    {
                        var postToLike = await dbContext.Posts.FindAsync(likeModel.PostId);
                        var likedPost = await Task.Run(() => dbContext.LikedPosts.Where(post => post.TravelerId == id && post.PostId == likeModel.PostId).FirstOrDefault());
                        var dislikedPost = await Task.Run(() => dbContext.DislikedPosts.Where(post => post.TravelerId == id && post.PostId == likeModel.PostId).FirstOrDefault());
                        if (dislikedPost != null)
                        {
                            dbContext.DislikedPosts.Remove(dislikedPost);
                            postToLike.NumOfDislikes--;
                        }
                        if (likedPost != null)
                        {
                            postToLike.NumOfLikes--;
                            dbContext.LikedPosts.Remove(likedPost);
                            await dbContext.SaveChangesAsync();
                            return Json(new TravelerActionResponse() { ErrorMessage = "Removed like", PostId = likeModel.PostId });
                        }
                        else
                        {
                            postToLike.NumOfLikes++;
                            var likePost = new LikedPost()
                            {
                                PostId = likeModel.PostId,
                                TravelerId = id
                            };
                            dbContext.LikedPosts.Add(likePost);
                        }
                        await dbContext.SaveChangesAsync();
                        return Json(new TravelerActionResponse() { ErrorMessage = "Successful", PostId = likeModel.PostId });
                    }

            }
            return Json(new TravelerActionResponse()
            {
                ErrorMessage = "Log in first",
                PostId = 0
            });
        }

        [HttpPost]
        public async Task<ActionResult> DislikePost(TravelerActionModel dislikeModel)
        {
            if (Session["id"] != null)
            {

                    int id = Convert.ToInt32(Session["id"]);
                    if (await authServices.AuthroizedTraveler(id))
                    {
                        var postToDislike = await dbContext.Posts.FindAsync(dislikeModel.PostId);
                        var dislikedPosts = await Task.Run(() => dbContext.DislikedPosts.Where(post => post.TravelerId == id && post.PostId == dislikeModel.PostId).FirstOrDefault());
                        var dpost = await Task.Run(() => dbContext.LikedPosts.Where(dislikedpost => dislikedpost.TravelerId == id && dislikedpost.PostId == dislikeModel.PostId).FirstOrDefault());
                        if (dpost != null)
                        {
                            dbContext.LikedPosts.Remove(dpost);
                            postToDislike.NumOfLikes--;
                        }
                        if (dislikedPosts != null)
                        {
                            postToDislike.NumOfDislikes--;
                            dbContext.DislikedPosts.Remove(dislikedPosts);
                            await dbContext.SaveChangesAsync();
                            return Json(new TravelerActionResponse() { ErrorMessage = "Removed dislike", PostId = dislikeModel.PostId });
                        }
                        else
                        {
                            postToDislike.NumOfDislikes++;
                            var dislikedPost = new DislikedPost()
                            {
                                PostId = dislikeModel.PostId,
                                TravelerId = id
                            };
                            dbContext.DislikedPosts.Add(dislikedPost);
                            await dbContext.SaveChangesAsync();
                            return Json(new TravelerActionResponse() { ErrorMessage = "Successful", PostId = dislikeModel.PostId });
                        }
                    }
            }
            return Json(new TravelerActionResponse()
            {
                ErrorMessage = "Log in first",
                PostId = 0
            });
        }

        [HttpPost]
        public async Task<ActionResult> AddFavoritePost(TravelerActionModel addFavModel)
        {
            if(Session["id"] != null)
            {
                    int id = Convert.ToInt32(Session["id"]);
                    if(await authServices.AuthroizedTraveler(id))
                    {
                        var favoritePost = await Task.Run(() => dbContext.FavoritePosts.Where(favPost => favPost.TravelerId == id && favPost.PostId == addFavModel.PostId).FirstOrDefault());
                        if(favoritePost == null)
                        {
                            FavoritePost favPost = new FavoritePost()
                            {
                                TravelerId = id,
                                PostId = addFavModel.PostId
                            };
                             dbContext.FavoritePosts.Add(favPost);
                            await dbContext.SaveChangesAsync();
                            return Json(new TravelerActionResponse() { PostId = addFavModel.PostId, ErrorMessage = "Successful" });
                        }
                        dbContext.FavoritePosts.Remove(favoritePost);
                        await dbContext.SaveChangesAsync();
                        return Json(new TravelerActionResponse() { PostId = addFavModel.PostId, ErrorMessage = "Successful remove favorite" });
                    }
            }
            return Json(new TravelerActionResponse() { ErrorMessage = "Login first..", PostId = 0});
        }

        public async Task<ActionResult> Favourite()
        {
            if(Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                if(await authServices.AuthroizedTraveler(id))
                {
                    var favoritePosts = await Task.Run(() => dbContext.FavoritePosts.Where(favPost => favPost.TravelerId == id).ToList());
                    List<Post> posts = new List<Post>();
                    Dictionary<int, bool> IsLiked = new Dictionary<int, bool>();
                    Dictionary<int, bool> IsDisliked = new Dictionary<int, bool>();
                    Dictionary<int, bool> IsFavorite = new Dictionary<int, bool>();
                    foreach (var post in favoritePosts)
                    {
                        posts.Add(post.Post);
                    }
                    var likedPosts = await Task.Run(() => dbContext.LikedPosts.Where(likedPost => likedPost.TravelerId == id).ToList());
                    var dislikedPosts = await Task.Run(() => dbContext.DislikedPosts.Where(dislikedPost => dislikedPost.TravelerId == id).ToList());
                    foreach (var post in likedPosts)
                    {
                        IsLiked.Add(post.PostId, true);
                    }
                    foreach (var post in dislikedPosts)
                    {
                        IsDisliked.Add(post.PostId, true);
                    }
                    foreach (var favPost in favoritePosts)
                    {
                        IsFavorite.Add(favPost.PostId, true);
                    }
                    ViewBag.IsLiked = IsLiked;
                    ViewBag.IsDisliked = IsDisliked;
                    ViewBag.IsFavorite = IsFavorite;
                    return View(viewName: "Favourite", model: posts);
                }
            }
            return RedirectToAction(actionName: "signout", controllerName: "user");
        }
    }
}