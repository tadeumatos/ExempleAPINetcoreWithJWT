using System;

namespace Bookstore.Models
{
    public class BookEntity
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public int AuthorId { get; set; }
        public virtual AuthorEntity Author { get; set; }
        public DateTime DateIncluded { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
