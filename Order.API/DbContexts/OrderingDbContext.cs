using System.Reflection;
using Order.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.DbContexts
{
    public partial class OrderingDbContext : DbContext
    {
        public virtual DbSet<Purchase>? Purchases { get; set; }
        public OrderingDbContext(DbContextOptions<OrderingDbContext> options):base(options)
        {
           
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder )
        {
            
        }
    }   
}