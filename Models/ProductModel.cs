using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JWL.Repository.Validation;

namespace JWL.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Hãy nhập tên Sản Phẩm")]

        public string Name { get; set; }
        
        public string Slug { get; set; }

		[Required, MinLength(4, ErrorMessage = "Hãy nhập mô tả Sản Phẩm")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Hãy nhập giá của Sản Phẩm")]
		[Range(0.01, double.MaxValue)]
		[Column(TypeName = "decimal(8, 2)")]
		public decimal Price { get; set; }
		[Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một Brand")]
		public int BrandID { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một Category")]
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
