using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using TripsAndTravelSystem.Models;
using TripsAndTravelSystem.Services;
using System.Threading.Tasks;

namespace TripsAndTravelSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserValidationServices validateUser = new UserValidationServices();
        private readonly ImageServices imageServices = new ImageServices();
        private readonly TripsAndTravelDatabaseEntities dbContext = new TripsAndTravelDatabaseEntities();
        private readonly AuthorizationServices authServices = new AuthorizationServices();
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerInfo)
        {
            if (registerInfo != null)
            {
                var response = validateUser.ValidateUser(registerInfo);
                if (response.ErrorMessage == null)
                {
                    using (var dbContext = new TripsAndTravelDatabaseEntities())
                    {
                        User user = new User();
                        user.FirstName = registerInfo.FirstName;
                        user.LastName = registerInfo.LastName;
                        user.Email = registerInfo.Email;
                        user.Password = Crypto.HashPassword(registerInfo.Password);
                        user.PhoneNumber = registerInfo.PhoneNumber;
                        user.ProfilePhoto = await imageServices.SavePhoto(registerInfo.ProfilePhoto, registerInfo.PhotoExtension, false, registerInfo.UserRole, Server);
                        user.UserRole = registerInfo.UserRole;
                        dbContext.Users.Add(user);
                        await dbContext.SaveChangesAsync();
                        var successResponse = new RegisterResponse() { ErrorMessage = "successful", UserId = user.UserId };
                        return Json(successResponse);
                    }
                }
                else
                {
                    return Json(response);
                }
            }
            var errorResponse = new RegisterResponse()
            {
                ErrorMessage = "Register data not received, try again",
                UserId = 0
            };
            return Json(errorResponse);
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginInfo)
        {
            if (loginInfo != null)
            {
                var u = await Task.Run(() => dbContext.Users.Where(user => user.Email.Equals(loginInfo.Email)).FirstOrDefault());
                if (u != null && Crypto.VerifyHashedPassword(u.Password, loginInfo.Password))
                {
                    string url = "";
                    if (await authServices.AuthorizedAdmin(u.UserId))
                    {
                        url = $"http://localhost:59738/admin/?id={u.UserId}";
                    }
                    else if (await authServices.AuthroizedAgency(u.UserId))
                    {
                        url = $"http://localhost:59738/agency/?id={u.UserId}";
                    }
                    else if (await authServices.AuthroizedTraveler(u.UserId))
                    {
                        url = "http://localhost:59738/";
                    }
                    return Json(new LoginResponse()
                    {
                        UserId = u.UserId,
                        ErrorMessage = null,
                        RedirectUrl = url
                    });
                }
                else
                {
                    return Json(new LoginResponse() { ErrorMessage = "Email or password is invalid, try again", UserId = 0 });
                }
            }
            var errorResponse = new LoginResponse()
            {
                UserId = 0,
                ErrorMessage = "Login data not received, try again"
            };
            return Json(errorResponse);
        }

        public ActionResult SignOut()
        {
            return RedirectToAction(actionName: "Index", controllerName: "Index");
        }

    }
}