using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWL.Repository.Components
{
	public class BrandsViewComponent : ViewComponent
	{
		private readonly JWLContext _JWLcontext;

		public BrandsViewComponent(JWLContext _context)
		{
			_JWLcontext = _context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _JWLcontext.Brands.ToListAsync());
	}
}
