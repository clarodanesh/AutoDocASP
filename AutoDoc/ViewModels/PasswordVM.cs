using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoDoc.ViewModels
{
    public class PasswordVM
    {

        [Required(ErrorMessage = "Please enter Password")]
        [Display(Name = "password")]
        public string password { get; set; }

    }
}