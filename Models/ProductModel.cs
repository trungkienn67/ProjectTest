using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JWL.Repository.Validation;

namespace JWL.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Yeu cau nhap ten San Pham")]

        public string Name { get; set; }
        
        public string Slug { get; set; }

		[Required, MinLength(4, ErrorMessage = "Yeu cau nhap mo ta San Pham")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Yeu cau nhap gia San Pham")]
		[Range(0.01, double.MaxValue)]
		[Column(TypeName = "decimal(8, 2)")]
		public decimal Price { get; set; }
		[Required, Range(1, int.MaxValue, ErrorMessage = "Chon mot thuong hieu")]
		public int BrandID { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Chon mot danh muc")]
        public int CategoryID { get; set; }

        public BrandModel Brand { get; set; }
        public CategoryModel Category { get; set; }		


        public string Image { get; set; } = "noimage.jpg";

        [NotMapped]	
		[FileExtension]

        [Required]
        public IFormFile ImageUpload { get; set; }
		
	}
}
