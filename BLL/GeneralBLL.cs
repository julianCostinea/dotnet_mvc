using DAL;
using DTO;

namespace BLL
{
    public class GeneralBLL
    {
        GeneralDAO dao = new GeneralDAO();
        AdsDAO adsdao = new AdsDAO();
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPosts();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.PopularPost = dao.GetPopularPosts();
            dto.MostViewedPost = dao.GetMostViewedPosts();
            dto.Videos = dao.GetVideos();
            dto.Adslist = adsdao.GetAds();
            return dto;
        }

        public GeneralDTO GetPostDetail(int id)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.PostDetail = dao.GetPostDetail(id);
            return dto;
        }
    }
}