using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class SocialMediaBLL
    {
        SocialMediaDAO dao = new SocialMediaDAO();
        public bool AddSocialMedia(SocialMediaDTO model, SessionDTO session)
        {
            SocialMedia social = new SocialMedia();
            social.Name = model.Name;
            social.Link = model.Link;
            social.ImagePath = model.ImagePath;
            social.AddDate = DateTime.Now;
            social.LastUpdateUserID = session.UserID;
            social.LastUpdateDate = DateTime.Now;
            int ID = dao.AddSocialMedia(social);
            LogDAO.AddLog(General.ProcessType.SocialAdd, General.TableName.Social, ID, session);
            
            return true;
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
            dtoList = dao.GetSocialMedias();
            return dtoList;
        }

        public SocialMediaDTO GetSocialMediaWithID(int id)
        {
            SocialMediaDTO dto = dao.GetSocialMediaWithID(id);
            return dto;
        }

        public string UpdateSocialMedia(SocialMediaDTO model, SessionDTO session)
        {
            string oldImagePath = dao.UpdateSocialMedia(model, session);
            LogDAO.AddLog(General.ProcessType.SocialUpdate, General.TableName.Social, model.ID, session);
            return oldImagePath;
        }

        public string DeleteSocialMedia(int id, SessionDTO session)
        {
            string oldImagePath = dao.DeleteSocialMedia(id, session);
            LogDAO.AddLog(General.ProcessType.SocialDelete, General.TableName.Social, id, session);
            return oldImagePath;
        }
    }
}