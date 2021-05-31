using System;

namespace TripsAndTravelSystem.Models
{
    public class AddPostModel
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string Destination { get; set; }
        public string TripPhoto { get; set; }
        public string PhotoExtension { get; set; }
        public DateTime TripDate { get; set; }
    }
}