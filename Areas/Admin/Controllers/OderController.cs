using JWL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWL.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OderController : Controller
	{
		private readonly JWLContext _JWLContext;
		public OderController(JWLContext context)
		{
			_JWLContext = context;
		}
		public async Task<IActionResult> Index()
		{

			return View(await _JWLContext.Oders.OrderByDescending(p => p.Id).ToListAsync());
		}
    }
}
