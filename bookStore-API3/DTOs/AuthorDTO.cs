using bookStore_API3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore_API3.DTOs
{
    
    //using DTO for user to interact with data, using DTO to validate input from user
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Fristname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }


        public virtual IList<Book> Books { get; set; }
    }
}
