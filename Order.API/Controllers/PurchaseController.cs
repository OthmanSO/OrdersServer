using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.Entities;
using Order.API.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/purchase")]
    public class PurchaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PurchaseRepository _purchaseRepo;
        private readonly IBookSDataAccessRepository _bookRepo;
        public PurchaseController(PurchaseRepository pr, IMapper map, IBookSDataAccessRepository bk)
        {
            _mapper = map ?? throw new ArgumentNullException(nameof(IMapper));
            _purchaseRepo = pr ?? throw new ArgumentNullException(nameof(PurchaseRepository));
            _bookRepo = bk ?? throw new ArgumentNullException(nameof(IBookSDataAccessRepository));
        }

        [HttpPost("{bookId}")]
        public async Task<ActionResult> PlaceOrder(int? bookId)
        {
            if (bookId == null)
                return NotFound("Please Provide BookId that you wonna Purchase");

            // call book server api
            var book = await _bookRepo.GetBook(bookId.Value);
            if( book == null)
                return NotFound($"Book with id = {bookId} not found");

            var pur = new PurchaseForCreationDto();
            pur.price = book.price;
            pur.bookId = bookId.Value;

            if(book.quantity == 0)
                return Conflict($"Sorry,Book {book.title} is out of stock!");
            
            //update quantity of books
            book.quantity -= 1;
            await _bookRepo.PutBook(book);

            //save purchase to db
            var purchaseEntity = _mapper.Map<Purchase>(pur);
            _purchaseRepo.AddPurchase(purchaseEntity);

            return Ok($"Book {book.title} purchased");
        }
    }
}