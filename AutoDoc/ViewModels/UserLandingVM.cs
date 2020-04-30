using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoDoc.ViewModels
{
    public class UserLandingVM
    {

        [Required(ErrorMessage = "no doctor, ")]
        [Display(Name = "doctor")]
        public string doctor { get; set; }

        [Required(ErrorMessage = "no date, ")]
        [Display(Name = "date")]
        public string date { get; set; }

        [Required(ErrorMessage = "no time ")]
        [Display(Name = "time")]
        public string time { get; set; }

    }
}