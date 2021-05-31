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
                    Session["id"] = u.UserId;
                    string url = "";
                    if (await authServices.AuthorizedAdmin(u.UserId))
                    {
                        url = $"{UrlServices.BaseUrl}/admin/";
                    }
                    else if (await authServices.AuthroizedAgency(u.UserId))
                    {
                        url = $"{UrlServices.BaseUrl}/agency/";
                    }
                    else if (await authServices.AuthroizedTraveler(u.UserId))
                    {
                        url = $"{UrlServices.BaseUrl}/";
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

        [HttpPost]
        public async Task<ActionResult> EditFirstName(EditNameModel editModel)
        {
            if (editModel != null) 
            {
                if (validateUser.ValidateName(editModel.Name))
                {
                    var user = await dbContext.Users.FindAsync(editModel.UserId);
                    user.FirstName = editModel.Name;
                    await dbContext.SaveChangesAsync();
                    return Json(new EditResponse() { ErrorMessage = "Successful", UserId = user.UserId});
                }
                else
                {
                    return Json(new EditResponse() { ErrorMessage = validateUser.InvalidNameError, UserId = 0});
                }
            }
            return Json(new EditResponse()
            {
                ErrorMessage = "Data not received, try again",
                UserId = 0
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditLastName(EditNameModel editModel)
        {
            if (editModel != null)
            {
                if (validateUser.ValidateName(editModel.Name))
                {
                    var user = await dbContext.Users.FindAsync(editModel.UserId);
                    user.LastName = editModel.Name;
                    await dbContext.SaveChangesAsync();
                    return Json(new EditResponse() { ErrorMessage = "Successful", UserId = user.UserId });
                }
                else
                {
                    return Json(new EditResponse() { ErrorMessage = validateUser.InvalidNameError, UserId = 0 });
                }
            }
            return Json(new EditResponse()
            {
                ErrorMessage = "Data not received, try again",
                UserId = 0
            });
        }


        [HttpPost]
        public async Task<ActionResult> EditPhoto(EditPhotoModel editModel)
        {
            if (editModel != null)
            {
                    var user = await dbContext.Users.FindAsync(editModel.UserId);
                    await imageServices.DeleteOldPhoto(user.ProfilePhoto);
                    user.ProfilePhoto = await imageServices.SavePhoto(editModel.Photo, editModel.PhotoExtension, false, user.UserRole, Server);
                    await dbContext.SaveChangesAsync();
                    return Json(new EditResponse() { ErrorMessage = "Successful", UserId = user.UserId });
            }
            return Json(new EditResponse()
            {
                ErrorMessage = "Data not received, try again",
                UserId = 0
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditEmail(EditEmailModel editModel)
        {
            if (editModel != null)
            {
                if (validateUser.ValidateEmail(editModel.Email))
                {
                    var user = await dbContext.Users.FindAsync(editModel.UserId);
                    user.Email = editModel.Email;
                    await dbContext.SaveChangesAsync();
                    return Json(new EditResponse() { ErrorMessage = "Successful", UserId = user.UserId });
                }
                else
                {
                    return Json(new EditResponse() { ErrorMessage = validateUser.InvalidNameError, UserId = 0 });
                }
            }
            return Json(new EditResponse()
            {
                ErrorMessage = "Data not received, try again",
                UserId = 0
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditPhoneNumber(EditPhoneNumberModel editModel)
        {
            if (editModel != null)
            {
                if (validateUser.ValidatePhoneNumber(editModel.PhoneNumber))
                {
                    var user = await dbContext.Users.FindAsync(editModel.UserId);
                    user.PhoneNumber = editModel.PhoneNumber;
                    await dbContext.SaveChangesAsync();
                    return Json(new EditResponse() { ErrorMessage = "Successful", UserId = user.UserId });
                }
                else
                {
                    return Json(new EditResponse() { ErrorMessage = validateUser.InvalidNameError, UserId = 0 });
                }
            }
            return Json(new EditResponse()
            {
                ErrorMessage = "Data not received, try again",
                UserId = 0
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditPassword(EditPasswordModel editModel)
        {
            if (editModel != null)
            {
                if (validateUser.ValidatePassword(editModel.Password))
                {
                    var user = await dbContext.Users.FindAsync(editModel.UserId);
                    user.Password = Crypto.HashPassword(editModel.Password);
                    await dbContext.SaveChangesAsync();
                    return Json(new EditResponse() { ErrorMessage = "Successful", UserId = user.UserId });
                }
                else
                {
                    return Json(new EditResponse() { ErrorMessage = validateUser.InvalidNameError, UserId = 0 });
                }
            }
            return Json(new EditResponse()
            {
                ErrorMessage = "Data not received, try again",
                UserId = 0
            });

        }

        public ActionResult SignOut()
        {
            Session.Clear();
            return RedirectToAction(actionName: "Index", controllerName: "Index");
        }
    }
}