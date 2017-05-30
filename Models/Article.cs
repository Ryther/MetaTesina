using System;
using System.ComponentModel.DataAnnotations;

namespace MetaTesina.Models
{
    public class Article
    {
        [Required]
        [Display(Name="Articolo")]
        public int ArticleID { get; set; }

        [Required]
        [Display(Name="Categoria Articolo")]
        public int CategoryID { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        [Display(Name="Immagine Link Articolo")]
        public int ArticleLinkImgID { get; set; }

        [Required]
        public virtual Asset ArticleLinkImg { get; set; }

        [Required]
        [Display(Name="Immagine Principale Articolo")]
        public int ArticleMainImgID { get; set; }

        [Required]
        public virtual Asset ArticleMainImg { get; set; }

        [Required]
        [Display(Name="Autore Articolo")]
        public int AuthorID { get; set; }

        [Required]
        [Display(Name="Data Articolo")]
        public DateTime ArticleCreateDate { get; set; }

        [Required]
        [Display(Name="Data Articolo")]
        public DateTime ArticleModifyDate { get; set; }

        [Required]
        [Display(Name="Titolo Articolo")]
        [MinLength(2)]
        [StringLength(40)]
        public string ArticleTitle { get; set; }

        [Required]
        [Display(Name="Descrizione Articolo")]
        [MinLength(2)]
        [StringLength(250)]
        public string ArticleDescription { get; set; }
        
        [Required]
        [Display(Name="Contenuto Articolo")]
        [MinLength(10)]
        [StringLength(10000)]
        public string ArticleContent { get; set; }
    }
}