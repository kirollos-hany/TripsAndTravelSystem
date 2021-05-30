

namespace TripsAndTravelSystem.Services
{
    public class PostValidationServices
    {
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