using JWL.Models;
using JWL.Models.ViewModels;
using JWL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWL.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly JWLContext _JWLcontext;
		public CheckoutController(JWLContext context)
		{
			_JWLcontext = context;
		}
		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var odercode = Guid.NewGuid().ToString();
				var oderItem = new OderModel();
				oderItem.OderCode = odercode;	
				oderItem.UserName = userEmail;
				oderItem.CreateDate = DateTime.Now;
				oderItem.Status = 1;				
				_JWLcontext.Add(oderItem);
				_JWLcontext.SaveChanges();
				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach(var cart in cartItems)
				{
					var oderdetails = new OderDetails();
					oderdetails.UserName = userEmail;
					oderdetails.OderCode = odercode;
					oderdetails.ProductId = cart.ProductId;
					oderdetails.Price = cart.Price;
					oderdetails.Quantity = cart.Quantity;
					_JWLcontext.Add(oderdetails);
					_JWLcontext.SaveChanges();
				}
				HttpContext.Session.Remove("Cart");
				TempData["success"] = "Checkout thành công, vui lòng đợi đơn hàng của bạn được duyệt.";
				return RedirectToAction("Index", "Cart");
			}
			return View();
		}
	}
}
