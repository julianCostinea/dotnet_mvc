using System.Collections.Generic;
using System.Linq;
using DAL;
using DTO;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO categorydao = new CategoryDAO();
        SocialMediaDAO socialmediadao = new SocialMediaDAO();
        FavDAO favdao = new FavDAO();
        MetaDAO metadao = new MetaDAO();
        AddressDAO addressdao = new AddressDAO();
        PostDAO postdao = new PostDAO();
        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = categorydao.GetCategories();
            List<SocialMediaDTO> socialmedialist = new List<SocialMediaDTO>();
            socialmedialist = socialmediadao.GetSocialMedias();
            dto.Facebook = socialmedialist.FirstOrDefault(x => x.Link.Contains("facebook"));
            // dto.Twitter = socialmedialist.FirstOrDefault(x => x.Link.Contains("twitter"));
            // dto.Instagram = socialmedialist.FirstOrDefault(x => x.Link.Contains("instagram"));
            // dto.Youtube = socialmedialist.FirstOrDefault(x => x.Link.Contains("youtube"));
            // dto.Linkedin = socialmedialist.FirstOrDefault(x => x.Link.Contains("linkedin"));
            dto.FavDTO = favdao.GetFav();
            dto.Metalist = metadao.GetMetaData();
            List<AddressDTO> addresslist = addressdao.GetAddresses();
            dto.Address = addresslist.FirstOrDefault();
            dto.HotNews = postdao.GetHotNews();
            return dto;
        }
    }
}