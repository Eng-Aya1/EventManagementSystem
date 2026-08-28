using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private static List<Event> _events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Title = "Global Tech Summit 2026",
                Description = "Join international tech leaders discussing AI, Cloud Infrastructure, and .NET ecosystem.",
                Category = "Tech",
                Location = "Cairo International Convention Center",
                StartDate = DateTime.Now.AddDays(7),
                PriceVip = 350.00m,
                PriceMiddle = 250.00m,
                PriceStandard = 150.00m,
                AvailableTickets = 15,
                ImageUrl = "https://images.unsplash.com/photo-1540575467063-178a50c2df87?w=600"
            },
            new Event
            {
                Id = 2,
                Title = "UI/UX & Product Design Masterclass",
                Description = "A hands-on practical workshop covering Figma component systems and user research.",
                Category = "Workshops",
                Location = "Online (Zoom Meeting)",
                StartDate = DateTime.Now.AddDays(12),
                PriceVip = 120.00m,
                PriceMiddle = 80.00m,
                PriceStandard = 0.00m,
                AvailableTickets = 80,
                ImageUrl = "https://images.unsplash.com/photo-1531403009284-440f080d1e12?w=600"
            },
            new Event
            {
                Id = 3,
                Title = "Startup Founders Networking Night",
                Description = "Connect with investors, co-founders, and industry leaders in an exclusive evening setting.",
                Category = "Business",
                Location = "The Greek Campus, Cairo",
                StartDate = DateTime.Now.AddDays(18),
                PriceVip = 200.00m,
                PriceMiddle = 120.00m,
                PriceStandard = 70.00m,
                AvailableTickets = 8,
                ImageUrl = "https://images.unsplash.com/photo-1511578314322-379afb476865?w=600"
            },
            new Event
            {
                Id = 4,
                Title = "Full-Stack Web Dev Bootcamp Showcase",
                Description = "Graduation projects presentation of top developers building modern high-scalability applications.",
                Category = "Tech",
                Location = "Smart Village, Giza",
                StartDate = DateTime.Now.AddDays(25),
                PriceVip = 100.00m,
                PriceMiddle = 50.00m,
                PriceStandard = 0.00m,
                AvailableTickets = 40,
                ImageUrl = "https://images.unsplash.com/photo-1522071820081-009f0129c71c?w=600"
            },
            new Event
            {
                Id = 5,
                Title = "AI & Machine Learning Expo",
                Description = "Explore live neural network demos, robotics showcases, and real-world enterprise AI cases.",
                Category = "Tech",
                Location = "AUC New Cairo Campus",
                StartDate = DateTime.Now.AddDays(30),
                PriceVip = 400.00m,
                PriceMiddle = 280.00m,
                PriceStandard = 180.00m,
                AvailableTickets = 25,
                ImageUrl = "https://images.unsplash.com/photo-1485827404703-89b55fcc595e?w=600"
            },
            new Event
            {
                Id = 6,
                Title = "Digital Marketing & Growth Hacking",
                Description = "Master modern SEO, social media strategies, funnel optimization, and paid advertising.",
                Category = "Workshops",
                Location = "Zamalek Hub, Cairo",
                StartDate = DateTime.Now.AddDays(35),
                PriceVip = 180.00m,
                PriceMiddle = 110.00m,
                PriceStandard = 60.00m,
                AvailableTickets = 18,
                ImageUrl = "https://images.unsplash.com/photo-1557804506-669a67965ba0?w=600"
            }
        };

        public IActionResult Index(string searchString, string category)
        {
            var events = _events.AsEnumerable();

            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                        || e.Location.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                events = events.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            return View(events.ToList());
        }

        // Step 1: Shows confirmation alert with pending booking info WITHOUT deducting tickets
        [HttpPost]
        public IActionResult ConfirmBooking(int id, string seatCategory, decimal seatPrice)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == id);
            if (eventItem != null && eventItem.AvailableTickets > 0)
            {
                TempData["Message"] = $"Please confirm: Would you like to reserve 1 [{seatCategory}] seat for '{eventItem.Title}' at ${seatPrice}?";
                TempData["PendingBookingId"] = id;
                TempData["PendingSeatCategory"] = seatCategory;
            }
            else
            {
                TempData["Error"] = "Sorry, no tickets available for this event!";
            }
            return RedirectToAction("Index");
        }

        // Step 2: Executed ONLY when the user clicks "? Yes" in the alert banner
        [HttpPost]
        public IActionResult FinalizeBooking(int id, string seatCategory)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == id);
            if (eventItem != null && eventItem.AvailableTickets > 0)
            {
                // Deduct ticket ONLY here after explicit Yes confirmation
                eventItem.AvailableTickets--;
                TempData["Message"] = $"?? Success! Your seat for '{eventItem.Title}' ({seatCategory}) is officially confirmed and 1 ticket was deducted!";
            }
            else
            {
                TempData["Error"] = "Booking failed! The event might be sold out.";
            }
            return RedirectToAction("Index");
        }
    }
}