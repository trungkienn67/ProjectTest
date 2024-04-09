using JWL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace JWL.Controllers
{
    public class ProductController : Controller
    {
		private readonly JWLContext _JWLContext;

		public ProductController(JWLContext context)
		{
			_JWLContext = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Details(int Id)
		{
			if (Id == null ) return RedirectToAction("Index");

			var productsById = _JWLContext.Products.Where(c => c.Id == Id).FirstOrDefault();

			return View(productsById);
		}
	}
}
