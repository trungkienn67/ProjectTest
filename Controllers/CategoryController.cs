using JWL.Models;
using JWL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly JWLContext _JWLContext;
        public CategoryController(JWLContext context)
		{
			_JWLContext = context;
		}

		public async Task<IActionResult> Index(string Slug = "")
        {
            CategoryModel category = _JWLContext.Categoryes.Where(p => p.Slug == Slug).FirstOrDefault();
            if (category == null) return RedirectToAction("Index");
            var productsByCategory = _JWLContext.Products.Where(c => c.CategoryID == category.Id);

			return View(await productsByCategory.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
