using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripsAndTravelSystem.Models
{
    public class EditPhotoModel
    {
        public string Photo { get; set; }

        public string PhotoExtension { get; set; }

        public int UserId { get; set; }
    }
}