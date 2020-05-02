using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoDoc.ViewModels
{
    public class UpdateVM
    {

        //all of these must be set when doing an update
        [Required(ErrorMessage = "no date, ")]
        [Display(Name = "date")]
        public string dob { get; set; }

        [Required(ErrorMessage = "no firstname, ")]
        [Display(Name = "firstname")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "no lastname ")]
        [Display(Name = "lastname")]
        public string lastname { get; set; }

    }
}