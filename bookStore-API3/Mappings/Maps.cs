using AutoMapper;
using bookStore_API3.Data;
using bookStore_API3.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore_API3.Mappings
{

    //
    public class Maps : Profile

    {
        //Constructor
        
        public Maps()
        {
            //reversemap for going both ways
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorCreateDTO>().ReverseMap();
            CreateMap<Author, AuthorUpdateDTO>().ReverseMap();
            CreateMap<Book, BooksDTO>().ReverseMap();
            CreateMap<Book, BooksCreateDTO>().ReverseMap();
            CreateMap<Book, BooksUpdateDTO>().ReverseMap();
            
        }
    }
}
