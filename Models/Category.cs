using System.ComponentModel.DataAnnotations;

namespace MetaTesina.Models
{
    public class Category
    {
        
        [Display(Name="Categoria")]
        public int CategoryID { get; set; }

        [Required]
        [Display(Name="Categoria")]
        [MinLength(2)]
        [MaxLength(20)]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name="Descrizione Categoria")]
        [MinLength(10)]
        [MaxLength(250)]
        public string CategoryDescription { get; set; }
    }
}