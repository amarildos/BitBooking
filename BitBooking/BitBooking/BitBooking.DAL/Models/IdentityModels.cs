using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;



namespace BitBooking.DAL.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int AccomodationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<BitBooking.DAL.Models.ProductsTest> ProductsTests { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.AccomodationType> AccomodationTypes { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.Accomodation> Accomodations { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.RoomType> RoomTypes { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.AccomodationService> AccomodationServices { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.AccomodationServiceType> AccomodationServiceTypes { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.AccomodationInfo> AccomodationInfoes { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.AccomodationFacility> AccomodationFacilities { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.RoomAvailability> RoomAvailabilities { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.RoomPrice> RoomPrices { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.Token> Tokens { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.Photo> Images { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.UserComment> UserComments { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.CommentUserReport> CommentUserReports { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<BitBooking.DAL.Models.Promotion> Promotions { get; set; }

      //  public System.Data.Entity.DbSet<BitBooking.DAL.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<BitBooking.DAL.Models.ReserveRoom> ReservedRooms { get; set; }

        //public System.Data.Entity.DbSet<BitBooking.DAL.Models.ApplicationUser> ApplicationUsers { get; set; }

 
    }
}