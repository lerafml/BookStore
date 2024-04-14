using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BOOK
    {
        public long ID { get; set; }
        public string NAME { get; set; }
        public string CAPTION { get; set; }
        public string AUTHOR { get; set; }
        public int COUNT { get; set; }

        public BookViewModel ToViewModel(BookViewModel view)
        {
            view.id = ID;
            view.author = AUTHOR;
            view.count = COUNT;
            view.name = NAME;
            view.caption = CAPTION;

            return view;
        }
    }
}