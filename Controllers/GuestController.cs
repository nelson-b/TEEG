using Microsoft.AspNetCore.Mvc;

namespace TestAPI_TEE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;

        public GuestController(ILogger<GuestController> logger)
        {
            _logger = logger;
        }

        public ActionResult AddGuest() 
        {
            return null;

        }

        public ActionResult UpdateGuestPhoneNo()
        {
            return null;
        }

        public ActionResult GetGuestById()
        {
            return null;
        }

        public ActionResult GetAllGuests()
        {
            return null;
        }
    }
}