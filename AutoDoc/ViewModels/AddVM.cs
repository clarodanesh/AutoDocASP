using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoDoc.ViewModels
{
    public class AddVM
    {
        //check if email is set
        [Required(ErrorMessage = "no email, ")]
        [Display(Name = "email")]
        public string email { get; set; }

        //check if the dob is set
        [Required(ErrorMessage = "no date, ")]
        [Display(Name = "date")]
        public string dob { get; set; }

        //check if fname set
        [Required(ErrorMessage = "no firstname, ")]
        [Display(Name = "firstname")]
        public string firstname { get; set; }

        //lname needs to be set
        [Required(ErrorMessage = "no lastname ")]
        [Display(Name = "lastname")]
        public string lastname { get; set; }

    }
}