using JWL.Models;
using JWL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWL.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]
	public class BrandController : Controller
    {
        private readonly JWLContext _JWLContext;
        public BrandController(JWLContext context)
        {
            _JWLContext = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _JWLContext.Brands.OrderByDescending(p => p.Id).ToListAsync());
        }
        public async Task<IActionResult> Edit(int Id)
        {
            BrandModel brand = await _JWLContext.Brands.FindAsync(Id);
            return View(brand);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandModel brand)
        {
            if (ModelState.IsValid)
            {
                //TempData["success"] = "Model co hieu luc";
                brand.Slug = brand.Name.Replace(" ", "-");
                var slug = await _JWLContext.Categoryes.FirstOrDefaultAsync(p => p.Slug == brand.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError(".", "Brand đã có trong database");
                    return View(brand);

                }
                _JWLContext.Add(brand);
                await _JWLContext.SaveChangesAsync();
                TempData["success"] = "Thêm Brand thành công";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Model đang có vài thứ bị lỗi";
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

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandModel brand)
        {
            if (ModelState.IsValid)
            {
                //TempData["success"] = "Model co hieu luc";
                brand.Slug = brand.Name.Replace(" ", "-");
                var slug = await _JWLContext.Categoryes.FirstOrDefaultAsync(p => p.Slug == brand.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError(".", "Brand đã có trong database");
                    return View(brand);

                }
                _JWLContext.Update(brand);
                await _JWLContext.SaveChangesAsync();
                TempData["success"] = "Update Brand thành công";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"] = "Model đang có vài thứ bị lỗi";
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

            return View(brand);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            BrandModel brand = await _JWLContext.Brands.FindAsync(Id);
            _JWLContext.Brands.Remove(brand);
            await _JWLContext.SaveChangesAsync();
            TempData["success"] = "Delete Brand successfully";
            return RedirectToAction("Index");

        }
    }
}
