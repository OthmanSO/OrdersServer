using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.API.Models;
using Refit;
namespace Order.API.Repositories
{
    public interface IBookSDataAccessRepository
    {
        [Get("books/{id}")]
        Task<BookDto> GetBook(int id);
        [Put("books/{id}")]
        Task<BookDto> PutBook([Body] BookDto book ,int id);
    }
}