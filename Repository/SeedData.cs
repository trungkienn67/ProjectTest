using JWL.Models;
using Microsoft.EntityFrameworkCore;


namespace JWL.Repository

{
	public class SeedData
	{
		public static void SeedingData(JWLContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CategoryModel macbook = new CategoryModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is Large Product in the world", Status = "1" };
				CategoryModel pc = new CategoryModel { Name = "Pc", Slug = "pc", Description = "Pc is Large Product in the world", Status = "1" };

				BrandModel apple = new BrandModel { Name = "Apple", Slug = "apple", Description = "Pc is Large Product in the world", Status = "1" };
				BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "samsung", Description = "Samsung is Large Product in the world", Status = "1" };


				_context.Products.AddRange(

					new ProductModel { Name = "Macbook", Slug = "macbook", Description = "Macbook is best", Image = "1.jpg", Category = macbook, Brand = apple, Price = 1234 },
					new ProductModel { Name = "Pc", Slug = "pc", Description = "Pc is best", Image = "1.jpg", Category = pc, Brand = samsung, Price = 5678 }


					);
				_context.SaveChanges();
			}
		}
	}
}
