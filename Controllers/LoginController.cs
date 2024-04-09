using Microsoft.AspNetCore.Mvc;

namespace JWL.Controllers
{
    public class LoginController : Controller
    {
		public IActionResult Index()
		{
			return View();
		}
	}
}
