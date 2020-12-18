﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookStore_API3.Data
{
    [Table("Authors")]
    public partial class Author
    {
        public int Id { get; set; }
        public string Fristname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }


        public virtual IList<Book> Books { get; set; }
    }
}