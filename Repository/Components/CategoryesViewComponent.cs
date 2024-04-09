using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWL.Repository.Components
{
	public class CategoryesViewComponent : ViewComponent
	{
		private readonly JWLContext _JWLcontext;

		public CategoryesViewComponent(JWLContext _context)
		{
			_JWLcontext = _context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _JWLcontext.Categoryes.ToListAsync());
	}
}
