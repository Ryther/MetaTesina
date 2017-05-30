using System.ComponentModel.DataAnnotations;

namespace MetaTesina.Models
{
    public class Asset
    {
        [Required]
        [Display(Name="Risorsa")]
        public int AssetID { get; set; }

        [Required]
        [Display(Name="Tipo Risorsa")]
        public int AssetTypeID { get; set; }

        [Required]
        [Display(Name="Nome Risorsa")]
        [MinLength(5)]
        [StringLength(20)]
        public string AssetName { get; set; }

        [Required]
        [Display(Name="Descrizione Risorsa")]
        [MinLength(10)]
        [StringLength(250)]
        public string AssetDescription { get; set; }

        [Required]
        [Display(Name="Percorso Risorsa")]
        [MinLength(5)]
        [StringLength(250)]
        public string AssetPath { get; set; }
    }
}