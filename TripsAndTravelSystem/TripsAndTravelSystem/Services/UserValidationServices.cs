using System;
using System.Net.Mail;
using TripsAndTravelSystem.Models;
namespace TripsAndTravelSystem.Services
{
    public class UserValidationServices
    {
        private string invalidNameError = "Invalid name must be less than or equal 10 characters";
        private string invalidEmailError = "Invalid email";
        private string invalidPhoneNumber = "Invalid phone number must be in format: +xx xxxxxxxxxx";
        private string invalidPassword = "Invalid password must be less than or equal 20 characters";
        private string invalidUserRole = "Please select a role";
        public string InvalidNameError { get
            {
                return invalidNameError;
            } }

        public string InvalidEmailError { get
            {
                return invalidEmailError;
            } }

        public string InvalidPhoneNumber { get
            {
                return invalidPhoneNumber;
            } }

        public string InvalidUserRole { get
            {
                return invalidUserRole;
            } }

        public string InvalidPassword { get
            {
                return invalidPassword;
            } }
        public RegisterResponse ValidateUser(RegisterModel registerInfo)
        {
            if (!(ValidateName(registerInfo.FirstName) && ValidateName(registerInfo.LastName)))
            {
                return new RegisterResponse() { ErrorMessage = InvalidNameError, UserId = 0 };
            }
            if (!(ValidatePhoneNumber(registerInfo.PhoneNumber)))
            {
                return new RegisterResponse() { ErrorMessage = InvalidPhoneNumber, UserId = 0 };
            }
            if (!ValidateEmail(registerInfo.Email))
            {
                return new RegisterResponse() { ErrorMessage = InvalidEmailError, UserId= 0 };
            }
            if (!ValidatePassword(registerInfo.Password))
            {
                return new RegisterResponse() { ErrorMessage = InvalidPassword, UserId = 0 };
            }
            if (!ValidateRole(registerInfo.UserRole))
            {
                return new RegisterResponse() { ErrorMessage = InvalidUserRole , UserId = 0};
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