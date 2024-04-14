using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class USER
    {
        public long ID { get; set; }
        [DisplayName("Почта")]
        public string EMAIL { get; set; }
        [Required]
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        [Required]
        [DisplayName("Логин")]
        public string LOGIN { get; set; }
        [Required]
        [DisplayName("Пароль")]
        public string PASSWORD { get; set; }
        public ICollection<BOOK> BOOKS { get; set; }
    }
}