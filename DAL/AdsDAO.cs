using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class AdsDAO:PostContext
    {
        public int AddAds(Ad ads)
        {
            try
            {
                db.Ads.Add(ads);
                db.SaveChanges();
                return ads.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdsDTO> GetAds()
        {
            List<Ad>list = db.Ads.Where(x=>x.isDeleted==false).ToList();
            List<AdsDTO> dtoList = new List<AdsDTO>();
            foreach (var item in list)
            {
                AdsDTO dto = new AdsDTO();
                dto.ID = item.ID;
                dto.Name = item.Name;
                dto.Link = item.Link;
                dto.ImagePath = item.ImagePath;
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public AdsDTO GetAdsWithID(int id)
        {
            Ad ads = db.Ads.FirstOrDefault(x => x.ID == id);
            AdsDTO dto = new AdsDTO();
            dto.ID = ads.ID;
            dto.Name = ads.Name;
            dto.Link = ads.Link;
            dto.ImagePath = ads.ImagePath;
            dto.Imagesize = ads.Size;
            return dto;
        }

        public string UpdateAds(AdsDTO model)
        {
            try
            {
                Ad ads = db.Ads.FirstOrDefault(x => x.ID == model.ID);
                string oldImagePath = ads.ImagePath;
                ads.Name = model.Name;
                ads.Link = model.Link;
                if (model.ImagePath == null)
                {
                    ads.ImagePath = model.ImagePath;
                }
                ads.Size = model.Imagesize;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}