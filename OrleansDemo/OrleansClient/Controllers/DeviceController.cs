using Microsoft.AspNetCore.Mvc;

namespace OrleansClient.Controllers
{
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
