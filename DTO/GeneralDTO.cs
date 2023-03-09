using System.Collections.Generic;

namespace DTO
{
    public class GeneralDTO
    {
        public List<PostDTO> SliderPost { get; set; }
        public List<PostDTO> PopularPost { get; set; }
        public List<PostDTO> MostViewedPost { get; set; }
        public List<PostDTO> BreakingPost { get; set; }
        public List<VideoDTO> Videos { get; set; }
        public List<AdsDTO> Adslist { get; set; }
        public PostDTO PostDetail { get; set; }
    }
}