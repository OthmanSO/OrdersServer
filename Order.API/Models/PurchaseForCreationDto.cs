using System;


namespace Order.API.Models
{
    public class PurchaseForCreationDto
    {
        public int bookId { get; set; }
        public int price { get; set; }
    }
}