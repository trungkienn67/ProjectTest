using Microsoft.AspNetCore.Identity;

namespace JWL.Models
{
	public class AppUserModel : IdentityUser
	{
		public string Occupation {  get; set; }	
	}
}
