using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DTO
{
    public class PostDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a short content")]
        public string ShortContent { get; set; }

        [Required(ErrorMessage = "Please enter a content")]
        public string PostContent { get; set; }

        [Required(ErrorMessage = "Please enter a category")]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public List<PostImageDTO> PostImages { get; set; }
        [Display(Name = "Post Image")] public List<HttpPostedFileBase> PostImage { get; set; }
        public List<TagDTO> TagList { get; set; }
        public string TagText { get; set; }
        public int ViewCount { get; set; }
        public string SeoLink { get; set; }
        public bool Slider { get; set; }
        public bool Area1 { get; set; }
        public bool Area2 { get; set; }
        public bool Area3 { get; set; }
        public bool Notification { get; set; }
        public string Language { get; set; }
        public DateTime AddDate { get; set; }
        public bool isUpdate { get; set; } = false;
        public string ImagePath { get; set; }
        public int CommentCount { get; set; }
    }
}