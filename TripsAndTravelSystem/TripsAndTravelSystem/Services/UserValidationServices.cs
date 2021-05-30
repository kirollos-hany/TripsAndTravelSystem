using System;
using System.Net.Mail;
using TripsAndTravelSystem.Models;
namespace TripsAndTravelSystem.Services
{
    public class UserValidationServices
    {
        public RegisterResponse ValidateUser(RegisterModel registerInfo)
        {
            if (!(ValidateName(registerInfo.FirstName) && ValidateName(registerInfo.LastName)))
            {
                return new RegisterResponse() { ErrorMessage = "Invalid name must be less than or equal 10 characters", UserId = 0 };
            }
            if (!(ValidatePhoneNumber(registerInfo.PhoneNumber)))
            {
                return new RegisterResponse() { ErrorMessage = "Invalid phone number must be in format: +xx xxxxxxxxxx", UserId = 0 };
            }
            if (!ValidateEmail(registerInfo.Email))
            {
                return new RegisterResponse() { ErrorMessage = "Invalid email", UserId= 0 };
            }
            if (!ValidatePassword(registerInfo.Password))
            {
                return new RegisterResponse() { ErrorMessage = "Invalid password must be less than or equal 20 characters", UserId = 0 };
            }
            if (!ValidateRole(registerInfo.UserRole))
            {
                return new RegisterResponse() { ErrorMessage = "Please select a role" , UserId = 0};
            }
            return new RegisterResponse() { ErrorMessage = null, UserId = 0 };
        }

        public bool ValidateName(string name)
        {
            return name != null ? name.Length > 0 && name.Length <= 10 : false;
        }
        public bool ValidatePhoneNumber(string phoneNumber)
        {
            return phoneNumber != null ? phoneNumber.Length == 14 && phoneNumber.StartsWith("+") && phoneNumber.IndexOf(' ') == 3 : false;
        }
        public bool ValidatePassword(string password)
        {
            return password != null ? password.Length > 0 && password.Length <= 20 : false;
        }

        public bool ValidateEmail(string email)
        {
            try {
                MailAddress mail = new MailAddress(email);
                return true;
            } catch (FormatException)
            {
                return false;
            }
        }
        public bool ValidateRole(string userRole)
        {
            return userRole != null ? userRole.Equals(User.UserRoles.Admin.ToString())
                || userRole.Equals(User.UserRoles.Traveler.ToString()) || userRole.Equals(User.UserRoles.Agency.ToString()) : false;
        }
    }
}