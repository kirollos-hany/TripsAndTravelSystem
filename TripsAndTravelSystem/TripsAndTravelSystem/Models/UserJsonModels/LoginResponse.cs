using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripsAndTravelSystem.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }

        public string ErrorMessage { get; set; }

        public string RedirectUrl { get; set; }

    }
}