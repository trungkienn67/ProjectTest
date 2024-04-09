using JWL.Models;
using JWL.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JWL.Areas.Admin.Controllers
{	
	[Area("Admin")]

	public class CategoryController : Controller
	{
		private readonly JWLContext _JWLContext;
		public CategoryController(JWLContext context)
		{
			_JWLContext = context;
			
		}
		public async Task<IActionResult> Index()
		{           
            return View(await _JWLContext.Categoryes.OrderByDescending(p => p.Id).ToListAsync());
		}
        public async Task<IActionResult> Edit(int Id)
        {
            CategoryModel category = await _JWLContext.Categoryes.FindAsync(Id);
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                //TempData["success"] = "Model co hieu luc";
                category.Slug = category.Name.Replace(" ", "-");
                var slug = await _JWLContext.Categoryes.FirstOrDefaultAsync(p => p.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError(".", "Danh muc da co trong database");
                    return View(category);

                }
                _JWLContext.Add(category);
                await _JWLContext.SaveChangesAsync();
                TempData["success"] = "Them danh muc thanh cong";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Model co mot vai thu dang bi loi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                //TempData["success"] = "Model co hieu luc";
                category.Slug = category.Name.Replace(" ", "-");
                var slug = await _JWLContext.Categoryes.FirstOrDefaultAsync(p => p.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError(".", "Danh muc da co trong database");
                    return View(category);

                }
                _JWLContext.Update(category);
                await _JWLContext.SaveChangesAsync();
                TempData["success"] = "Cap nhat danh muc thanh cong";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Model co mot vai thu dang bi loi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            CategoryModel category = await _JWLContext.Categoryes.FindAsync(Id);
            _JWLContext.Categoryes.Remove(category);
            await _JWLContext.SaveChangesAsync();
            TempData["success"] = "Da xoa danh muc";
            return RedirectToAction("Index");

        }
    }
}
