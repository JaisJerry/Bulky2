using System.ComponentModel.DataAnnotations;

namespace Bulky2.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
        [Display(Name ="Display Order")]

        [Range(0,100,ErrorMessage ="0 to 100 Only")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
