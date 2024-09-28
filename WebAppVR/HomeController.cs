// HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace WhatsAppBot.Controllers
{
    public class HomeController : Controller
    {
        private readonly WhatsAppService _whatsAppService;

        public HomeController()
        {
            _whatsAppService = new WhatsAppService();
        }

        public IActionResult Index()
        {
            // Change this line to call the ExtractMessages method
            _whatsAppService.ExtractMessages(); // Start extracting messages
            return View();
        }
    }
}
