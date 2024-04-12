using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWL.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên Brand")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hãy nhập mô tả của Brand")]
        public string Description { get; set; }
        public string Status { get; set; }
        public string Slug { get; set; }

    }
}

