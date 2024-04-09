using JWL.Models;
using JWL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace JWL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {


        private readonly JWLContext _JWLContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(JWLContext context, IWebHostEnvironment webHostEnvironment)
        {
            _JWLContext = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {

            return View(await _JWLContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categoryes = new SelectList(_JWLContext.Categoryes, "Id", "Name");
            ViewBag.Brands = new SelectList(_JWLContext.Brands, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            ViewBag.Categoryes = new SelectList(_JWLContext.Categoryes, "Id", "Name", product.CategoryID);
            ViewBag.Brands = new SelectList(_JWLContext.Brands, "Id", "Name", product.BrandID);

            if (ModelState.IsValid)
            {
                //TempData["success"] = "Model co hieu luc";
                product.Slug = product.Name.Replace(" ", "-");
                var slug = await _JWLContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError(".", "San pham da co trong database");
                    return View(product);

                }

                if (product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "-" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imageName;
                }

                _JWLContext.Add(product);
                await _JWLContext.SaveChangesAsync();
                TempData["success"] = "Them san pham thanh cong";
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

            return View(product);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            ProductModel product = await _JWLContext.Products.FindAsync(Id);
            ViewBag.Categoryes = new SelectList(_JWLContext.Categoryes, "Id", "Name", product.CategoryID);
            ViewBag.Brands = new SelectList(_JWLContext.Brands, "Id", "Name", product.BrandID);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, ProductModel product)
        {
            ViewBag.Categoryes = new SelectList(_JWLContext.Categoryes, "Id", "Name", product.CategoryID);
            ViewBag.Brands = new SelectList(_JWLContext.Brands, "Id", "Name", product.BrandID);

            if (ModelState.IsValid)
            {
                //TempData["success"] = "Model co hieu luc";
                product.Slug = product.Name.Replace(" ", "-");
                var slug = await _JWLContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError(".", "San pham da co trong database");
                    return View(product);

                }

                if (product.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "-" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imageName;
                }

                _JWLContext.Update(product);
                await _JWLContext.SaveChangesAsync();
                TempData["success"] = "Cap nhat san pham thanh cong";
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

            return View(product);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            ProductModel product = await _JWLContext.Products.FindAsync(Id);
            if(!string.Equals(product.Image, "noname.jpg"))
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                string oldfilePath = Path.Combine(uploadDir, product.Image);
                if (System.IO.File.Exists(oldfilePath))
                {
                    System.IO.File.Delete(oldfilePath);
                }

            }
            _JWLContext.Products.Remove(product);
            await _JWLContext.SaveChangesAsync();
            TempData["error"] = "Da xoa san pham";
            return RedirectToAction("Index");

        }
    }
}
