using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PublisherAndBook.ViewModels
{
    public class BookInputModel
    {
        public int BookId { get; set; }
        [Required, StringLength(50), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool Discontinued { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        //FK
        public int PublisherId { get; set; }
        
    }
    public class BookEditModel
    {
        public int BookId { get; set; }
        [Required, StringLength(50), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool Discontinued { get; set; }
        
        public HttpPostedFileBase Picture { get; set; }
        //FK
        public int PublisherId { get; set; }
       
    }
}