using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bookstore.Models
{
    public class AuthorEntity
    {
        public AuthorEntity()
        {
            Books = new Collection<BookEntity>();
        }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public bool CoAuthor { get; set; }
        public DateTime DateIncluded { get; set; }
        public DateTime DateUpdated { get; set; }
        public virtual ICollection<BookEntity> Books { get; set; }

    }
}
