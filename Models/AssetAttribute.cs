using System.ComponentModel.DataAnnotations;

namespace MetaTesina.Models
{
    public class AssetAttribute
    {
        public int AssetAttributeID { get; set; }

        [Required]
        [Display(Name="Tipo Risorsa")]
        public int AssetTypeID { get; set; }
        public virtual AssetType AssetType { get; set; }

        [Required]
        [Display(Name="Nome Attributo Risorsa")]
        [MinLength(5)]
        [MaxLength(20)]
        public string AssetTypeName { get; set; }
        
        [Required]
        [Display(Name="Descrizione Attributo Risorsa")]
        [MinLength(10)]
        [MaxLength(250)]
        public string AssetTypeDescription { get; set; }
    }
}