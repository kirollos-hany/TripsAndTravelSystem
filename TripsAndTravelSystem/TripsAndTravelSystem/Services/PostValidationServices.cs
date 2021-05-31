using TripsAndTravelSystem.Models;
namespace TripsAndTravelSystem.Services
{
    public class PostValidationServices
    {
        private readonly string invalidTitle = "Invalid title must be less than or equal 50 characters";
        public string InvalidTitle { get { return invalidTitle; } }

        private readonly string invalidDetails = "Details can't be empty";

        public string InvalidDetails { get { return invalidDetails; } }

        private readonly string invalidDestination = "Destination can't be empty";

        public string InvalidDestination { get { return invalidDestination; } }

        private readonly string invalidPrice = "Price must be greater than 0";
        
        public string InvalidPrice { get { return invalidPrice; } }

        public AddPostResponse ValidatePost(AddPostModel postInfo)
        {
            if (!ValidateTitle(postInfo.Title))
            {
                return new AddPostResponse() { ErrorMessage = InvalidTitle, UserId = 0 };
            }
            if (!ValidateDestination(postInfo.Destination))
            {
                return new AddPostResponse() { ErrorMessage = InvalidDestination, UserId = 0 };
            }
            if (!ValidateDetails(postInfo.Details))
            {
                return new AddPostResponse() { ErrorMessage = InvalidDetails, UserId = 0 };
            }
            if (!ValidatePrice(postInfo.Price))
            {
                return new AddPostResponse() { ErrorMessage = InvalidPrice, UserId = 0 };
            }
            return new AddPostResponse() { ErrorMessage = null, UserId = 0 };
        }

        public bool ValidatePrice(decimal price)
        {
            return price > 0;
        }
        public bool ValidateTitle(string title)
        {
            return title != null ? title.Length > 0 && title.Length <= 50 : false;
        }

        public bool ValidateDetails(string details)
        {
            return details != null ? details.Length > 0 : false;
        }

        public bool ValidateDestination(string destination)
        {
            return destination != null ? destination.Length > 0 : false;
        }
    }
}