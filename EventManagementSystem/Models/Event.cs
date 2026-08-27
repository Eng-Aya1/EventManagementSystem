using System;

namespace EventManagementSystem.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public decimal PriceVip { get; set; }
        public decimal PriceMiddle { get; set; }
        public decimal PriceStandard { get; set; }
        public int AvailableTickets { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}