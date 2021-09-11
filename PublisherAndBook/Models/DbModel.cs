using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PublisherAndBook.Models
{

    public class Publisher
    {
        public Publisher() { this.Books = new List<Book>(); }
        public int PublisherId { get; set; }
        [Required, StringLength(50), Display(Name = "Publisher Name")]
        public string PublisherName { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Establish { get; set; }
        //Navigation
        public virtual ICollection<Book> Books { get; set; }
    }
    public class Book
    {
        public int BookId { get; set; }
        [Required, StringLength(50), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool Discontinued { get; set; }
        [Required,StringLength(150)]
        public string Picture { get; set; }
        //FK
        public int PublisherId { get; set; }
        //Navigation
        public virtual Publisher Publisher { get; set; }
    }
    public class PlaceDbContext : DbContext
    {
        public PlaceDbContext()
        {
            Database.SetInitializer(new Seeder());
        }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
    }
    public class Seeder : DropCreateDatabaseIfModelChanges<PlaceDbContext>
    {
        protected override void Seed(PlaceDbContext context)
        {
            Publisher p = new Publisher { PublisherName = "Kotha Mala", Establish = new DateTime(2020, 10, 10) };
            p.Books.Add(new Book { BookName = "C#", Price = 500.00M, Discontinued = false, Picture = "demo.jpg" });
            context.Publishers.Add(p);
            context.SaveChanges();
        }

    }
}