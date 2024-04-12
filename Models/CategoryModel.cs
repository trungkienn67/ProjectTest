using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWL.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Hãy nhập tên Category")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hãy nhập mô tả của Category")]
        public string Description { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
	}
}
