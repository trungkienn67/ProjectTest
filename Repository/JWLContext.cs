using JWL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace JWL.Repository
{
	public class JWLContext : IdentityDbContext<AppUserModel>
	{
		public JWLContext(DbContextOptions<JWLContext> options) : base(options) 
		{ 

		}

		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categoryes { get; set; }
		public DbSet<OderModel> Oders { get; set; }
		public DbSet<OderDetails> OderDetails { get; set; }

    }
}
