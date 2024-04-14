using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class BookViewModel
    {
        public long id { get; set; }
        [Required]
        [DisplayName("Наименование")]
        public string name { get; set; }
        [DisplayName("Описание")]
        public string caption { get; set; }
        [Required]
        [DisplayName("Автор")]
        public string author { get; set; }
        [Required]
        [DisplayName("Количество")]
        public int count { get; set; }
        public bool? taken { get; set; }

        public BOOK ToEntity(BOOK entity)
        {
            entity.ID = id;
            entity.AUTHOR = author;
            entity.NAME = name;
            entity.CAPTION = caption;
            entity.COUNT = count;

            return entity;
        }
    }
}