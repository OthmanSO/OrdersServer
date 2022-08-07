using System;
using System.Linq;
using Order.API.DbContexts;
using Order.API.Models;
using Order.API.Entities;
using System.Collections.Generic;

namespace Order.API.Repositories
{
    public class PurchaseRepository 
    {
         private readonly OrderingDbContext _context;
        public PurchaseRepository(OrderingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddPurchase(Purchase purchase)
        {
            if(purchase == null)
                throw new ArgumentNullException(nameof(Purchase));
            
            _context.Purchases.Add(purchase);
        }
    }
}