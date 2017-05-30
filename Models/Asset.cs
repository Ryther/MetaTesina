using System.ComponentModel.DataAnnotations;

namespace MetaTesina.Models
{
    public class Asset
    {
        [Required]
        [Display]
        public int AssetID { get; set; }
        public int AssetTypeID { get; set; }
        public string AssetName { get; set; }
        public string AssetDescription { get; set; }
        public string AssetPath { get; set; }
    }
}