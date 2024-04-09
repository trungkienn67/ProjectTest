using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWL.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Yeu cau nhap ten Thuong Hieu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Yeu cau nhap mo ta Thuong Hieu")]
        public string Description { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
	}
}
