using System.ComponentModel.DataAnnotations.Schema;

namespace JWL.Models
{
	public class OderDetails
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string OderCode { get; set; }
		public long ProductId { get; set;}
		public decimal Price { get; set; }			
		public int Quantity { get; set;}
	}
}
