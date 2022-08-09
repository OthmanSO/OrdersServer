using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.API.Models;
using Order.API.Entities;
using Refit;
namespace Order.API.Repositories
{
    public interface IPurchaseSyncRepository
    {
        [Post("/purchase/1")]
        Task SyncOut([Body] Purchase pur);
    }
}