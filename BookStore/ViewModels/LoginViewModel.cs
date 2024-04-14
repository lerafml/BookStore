using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(maximumLength:15, MinimumLength = 4)]
        [DisplayName("Логин")]
        public string login { get; set; }

        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 4)]
        [DisplayName("Пароль")]
        public string password { get; set; }
    }
}