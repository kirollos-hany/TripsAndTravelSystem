using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripsAndTravelSystem.Models
{
    public class RegisterModel
    {
        public string PhotoExtension { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string UserRole { get ; set; }

        public string ProfilePhoto { get; set; }
    }
}