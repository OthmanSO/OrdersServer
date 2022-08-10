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
        private readonly IPurchaseSyncRepository _syncRepo;
        public PurchaseController(PurchaseRepository pr, IMapper map,
                 IBookSDataAccessRepository bk, IPurchaseSyncRepository sr)
        {
            _mapper = map ?? throw new ArgumentNullException(nameof(IMapper));
            _purchaseRepo = pr ?? throw new ArgumentNullException(nameof(PurchaseRepository));
            _bookRepo = bk ?? throw new ArgumentNullException(nameof(IBookSDataAccessRepository));
            _syncRepo = sr ?? throw new ArgumentNullException(nameof(IPurchaseSyncRepository));
        }

        [HttpPost("{bookId}")]
        public async Task<IActionResult> PlaceOrder(int bookId)
        {
            // call book server api
            try
            {
                var book = await _bookRepo.GetBook(bookId);

                var pur = new PurchaseForCreationDto();
                pur.price = book.price;
                pur.bookId = bookId;

                if (book.quantity == 0)
                {
                    var err = new ErrorCode();
                    err.MessageError = $"Sorry,Book {book.title} is out of stock!";
                    return Conflict(err);
                }
                //update quantity of books
                book.quantity -= 1;
                await _bookRepo.PutBook(book, bookId);

                //save purchase to db
                var purchaseEntity = _mapper.Map<Purchase>(pur);
                _purchaseRepo.AddPurchase(purchaseEntity);
                // sync
                await _syncRepo.SyncOut(purchaseEntity);
                
                var okCode = new OkCode();
                okCode.okMessage = $"Book {book.title} purchased";
                return Ok(okCode);
            }
            catch (Exception e)
            {
                var err = new ErrorCode();
                err.MessageError = $" Book with id = {bookId} not found";
                return Conflict(err);
            }

        }
    }
}