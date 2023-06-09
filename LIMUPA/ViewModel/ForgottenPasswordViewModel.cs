﻿using System.ComponentModel.DataAnnotations;

namespace LIMUPA.ViewModel
{
    public class ForgottenPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Registered email address")]
        public string Email { get; set; }

        public bool EmailSent { get; set; }
    }
}
