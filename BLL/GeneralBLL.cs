﻿using System.Collections.Generic;
using System.Linq;
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

        CategoryDAO categorydao = new CategoryDAO();
        public GeneralDTO GetCategoryPostList(string categoryName)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            if (categoryName == "video")
            {
                dto.Videos = dao.GetAllVideos();
                dto.CategoryName = "video";
            }
            else
            {
                List<CategoryDTO> categorylist = categorydao.GetCategories();
                int categoryID = 0;
                foreach (var item in categorylist)
                {
                    if (item.CategoryName == SeoLink.GenerateUrl(item.CategoryName))
                    {
                        categoryID = item.ID;
                        dto.CategoryName = item.CategoryName;
                        break;
                    }
                }
                dto.CategoryPostList = dao.GetCategoryPostList(categoryID);
            }
            return dto;
        }

        AddressDAO addressdao = new AddressDAO();
        public GeneralDTO GetContactPageItems()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.Address = addressdao.GetAddresses().FirstOrDefault();
            return dto;
        }

        public GeneralDTO GetSearchPosts(string modelSearchText)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.CategoryPostList = dao.GetSearchPosts(modelSearchText);
            dto.SearchText = modelSearchText;
            return dto;
        }
    }
}