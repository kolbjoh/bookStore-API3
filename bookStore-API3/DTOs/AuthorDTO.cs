using bookStore_API3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore_API3.DTOs
{
    
    //using DTO for user to interact with data, using DTO to validate input from user
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }


        public virtual IList<Book> Books { get; set; }
    }
    //new class for using in the controller, input fields required
    public class AuthorCreateDTO
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string Bio { get; set; }
    }
    public class AuthorUpdateDTO
    {

        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string Bio { get; set; }
    }

    }
