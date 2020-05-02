using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoDoc.ViewModels
{
    public class LoginVM
    {

        //email needs to be set and match email regex pattern (regex from labsheet)
        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please Enter a valid Email Address")]
        [Display(Name = "email")]
        public string email { get; set; }

        //password needs to be set
        [Required(ErrorMessage = "Please enter Password")]
        [Display(Name = "password")]
        public string password { get; set; }

    }
}