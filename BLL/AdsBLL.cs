using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class AdsBLL
    {
        AdsDAO dao = new AdsDAO();
        public void AddAds(AdsDTO dto)
        {
            Ad ads = new Ad();
            ads.Name = dto.Name;
            ads.Link = dto.Link;
            ads.ImagePath = dto.ImagePath;
            ads.Size = dto.Imagesize;
            ads.AddDate = DateTime.Now;
            ads.LastUpdateUserID = UserStatic.UserID;
            ads.LastUpdateDate = DateTime.Now;
            int ID = dao.AddAds(ads);
            LogDAO.AddLog(General.ProcessType.AdsAdd, General.TableName.Ads, ID);
        }

        public List<AdsDTO> GetAds()
        {
            return dao.GetAds();
        }


        public AdsDTO GetAdsWithID(int id)
        {
            return dao.GetAdsWithID(id);
        }

        public string UpdateAds(AdsDTO model)
        {
            string oldImagePath = dao.UpdateAds(model);
            LogDAO.AddLog(General.ProcessType.AdsUpdate, General.TableName.Ads, model.ID);
            return oldImagePath;
        }
    }
}