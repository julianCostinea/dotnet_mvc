using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class VideoDTO
    {
        public int ID { get; set; }
        public string VideoPath { get; set; }
        [Required(ErrorMessage = "Fill in video path")]
        public string OriginalVideoPath { get; set; }
        [Required(ErrorMessage = "Fill in video title")]
        public string Title { get; set; }
        public DateTime AddDate { get; set; }
    }
}