using JWL.Models;
using Microsoft.EntityFrameworkCore;


namespace JWL.Repository
{
	public class JWLContext : DbContext
	{
		public JWLContext(DbContextOptions<JWLContext> options) : base(options) 
		{ 

		}

		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categoryes { get; set; }


	}
}
