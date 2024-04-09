using Microsoft.AspNetCore.Mvc;

namespace JWL.Controllers
{
	public class CheckoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
