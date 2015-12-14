using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountWcfV2Client.Models
{
    public class AccountInfo
    {
        [Required]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(16, ErrorMessage = "{0} can allow a max of {1} characters")]
        [MinLength(8, ErrorMessage = "{0} you must enter minimum of {1} characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [MaxLength(16, ErrorMessage = "{0} can allow a max of {1} characters")]
        [MinLength(8, ErrorMessage = "{0} you must enter minimum of {1} characters")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName{ get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}