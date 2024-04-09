using JWL.Models;
using JWL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWL.Controllers
{
	public class BrandController : Controller
	{
		private readonly JWLContext _JWLContext;

		public BrandController(JWLContext context)
		{
			_JWLContext = context;
		}

		public async Task<IActionResult> Index(string Slug = "")
		{
			BrandModel brand = _JWLContext.Brands.Where(p => p.Slug == Slug).FirstOrDefault();
			if (brand == null) return RedirectToAction("Index");
			var productsByBrand = _JWLContext.Products.Where(c => c.BrandID == brand.Id);

			return View(await productsByBrand.OrderByDescending(p => p.Id).ToListAsync());
		}
	}
}
