using System.ComponentModel.DataAnnotations;

namespace MetaTesina.Models
{
    public class AssetType
    {
        public int AssetTypeID { get; set; }

        [Required]
        [Display(Name="Nome Risorsa")]
        [MinLength(5)]
        [MaxLength(20)]
        public string AssetTypeName { get; set; }
        
        [Required]
        [Display(Name="Descrizione Risorsa")]
        [MinLength(10)]
        [MaxLength(250)]
        public string AssetTypeDescription { get; set; }
    }
}