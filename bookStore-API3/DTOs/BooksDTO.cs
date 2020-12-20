using bookStore_API3.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore_API3.DTOs
{
    //using DTO for user to interact with data, using DTO to validate input from user
    public class BooksDTO
    {
        public int id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public string Isbn { get; set; }

        public string Summary { get; set; }
        public string Image { get; set; }

        public decimal? Price { get; set; }

        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }

    public class BooksCreateDTO
    {
        [Required]
        public string Title { get; set; }
        public int? Year { get; set; }
        [Required]
        public string Isbn { get; set; }
        [StringLength(500)]
        public string Summary { get; set; }
        public string Image { get; set; }

        public decimal? Price { get; set; }
        [Required]
        public int? AuthorId { get; set; }

    }
    public class BooksUpdateDTO
    {
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? Year { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }
        public string Image { get; set; }

        public decimal? Price { get; set; }

    }
}
