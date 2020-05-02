using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoDoc.ViewModels
{
    public class RegisterVM
    {

        //email needs to be set and match the regex from labsheets
        [Required(ErrorMessage = "No email, ")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please Enter a valid Email Address")]
        [Display(Name = "email")]
        public string email { get; set; }

        //password has to be set when registering
        [Required(ErrorMessage = "No password, ")]
        [Display(Name = "password")]
        public string password { get; set; }

        //fname must be set
        [Required(ErrorMessage = "No firstname, ")]
        [Display(Name = "firstname")]
        public string firstname { get; set; }

        //lname needs to be set
        [Required(ErrorMessage = "No lastname, ")]
        [Display(Name = "lastname")]
        public string lastname { get; set; }

        //dob must be set
        [Required(ErrorMessage = "No date of birth ")]
        [Display(Name = "dateofbirth")]
        public string dob { get; set; }

    }
}