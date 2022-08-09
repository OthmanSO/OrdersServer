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
    [Route("api/sync")]
    public class SyncController : ControllerBase
    {
        private readonly PurchaseRepository _purchaseRepo;
        public SyncController(PurchaseRepository pr)
        {
            _purchaseRepo = pr ?? throw new ArgumentNullException(nameof(PurchaseRepository));
        }

        [HttpPost()]
        public async Task<IActionResult> SyncIn([FromBody] Purchase pur)
        {
            try
            {
                _purchaseRepo.AddPurchase(pur);
                return Ok();
            }
            catch (Exception e)
            {
                return Conflict();
            }

        }
    }
}