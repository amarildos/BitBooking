using BitBooking.DAL.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BitBooking.MVC.Models
{




   








    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class CreatAccomodationInformation
    {
       [Required]
        [DisplayName("Accomodation Name")]
        public string AccomodationName { get; set; }

        [Range(1,5)]
        [DisplayName("Star Rating")]
        public int StarRating { get; set; }
        [Required]
        [DisplayName("Number of Rooms")]
        public int NumberOfRooms { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public string Description { get; set; }
        
        public virtual AccomodationType AccomodationType { get; set; }
 
               [DisplayName("Accomodation Type")]
        [Required]
        public int AccomodationTypeId { get; set; }


               [Required]
               public string Address { get; set; }
               [Required]
               public string City { get; set; }
               [Required]
               [DisplayName("Postal Code")]
               public string PostalCode { get; set; }
               public string Country { get; set; }
               [DisplayName("Telephone Number")]
               public string Phone { get; set; }
               [DisplayName("Email Address")]
               [Required]
               public string Email { get; set; }



    }




    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public int AccomodationId { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
