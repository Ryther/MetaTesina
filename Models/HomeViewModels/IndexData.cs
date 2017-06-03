using System;
using System.ComponentModel.DataAnnotations;

namespace MetaTesina.Models.HomeViewModels
{
    public class IndexData
    {
        public int ArticleID { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDescription { get; set; }
        public DateTime ArticleCreateDate { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int ArticleLinkImgID { get; set; }
        public string AssetPath { get; set; }
    }
}