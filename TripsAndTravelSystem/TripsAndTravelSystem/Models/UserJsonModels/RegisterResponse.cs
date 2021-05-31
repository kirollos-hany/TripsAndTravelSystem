using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripsAndTravelSystem.Models
{
    public class RegisterResponse
    {
        public int UserId { get; set; }

        public string ErrorMessage { get; set; }
    }
}