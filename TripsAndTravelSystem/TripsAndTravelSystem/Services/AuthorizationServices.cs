using System.Threading.Tasks;
using TripsAndTravelSystem.Models;

namespace TripsAndTravelSystem.Services
{
    public class AuthorizationServices
    {
        public async Task<bool> AuthroizedTraveler(int userId)
        {
            using (var dbContext = new TripsAndTravelDatabaseEntities())
            {
                var user = await dbContext.Users.FindAsync(userId);
                return user.UserRole.Equals(User.UserRoles.Traveler.ToString());
            }
        }

        public async Task<bool> AuthorizedAdmin(int userId)
        {
            using (var dbContext = new TripsAndTravelDatabaseEntities())
            {
                var user = await dbContext.Users.FindAsync(userId);
                return user.UserRole.Equals(User.UserRoles.Admin.ToString());
            }
        }

        public async Task<bool> AuthroizedAgency(int userId)
        {
            using (var dbContext = new TripsAndTravelDatabaseEntities())
            {
                var user = await dbContext.Users.FindAsync(userId);
                return user.UserRole.Equals(User.UserRoles.Agency.ToString());
            }
        }

       
    }
}