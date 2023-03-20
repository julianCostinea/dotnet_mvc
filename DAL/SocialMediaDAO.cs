using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class SocialMediaDAO:PostContext
    {
        public int AddSocialMedia(SocialMedia social)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.SocialMedias.Add(social);
                    db.SaveChanges();
                    return social.ID;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<SocialMedia> list = db.SocialMedias.Where(x => x.isDeleted == false).ToList();
                List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
                foreach (var item in list)
                {
                    SocialMediaDTO dto = new SocialMediaDTO();
                    dto.ID = item.ID;
                    dto.Name = item.Name;
                    dto.Link = item.Link;
                    dto.ImagePath = item.ImagePath;
                    dtoList.Add(dto);
                }

                return dtoList;
            }
        }

        public SocialMediaDTO GetSocialMediaWithID(int id)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                SocialMedia social = db.SocialMedias.FirstOrDefault(x => x.ID == id);
                SocialMediaDTO dto = new SocialMediaDTO();
                dto.ID = social.ID;
                dto.Name = social.Name;
                dto.Link = social.Link;
                dto.ImagePath = social.ImagePath;
                return dto;
            }
        }

        public string UpdateSocialMedia(SocialMediaDTO model, SessionDTO session)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    SocialMedia social = db.SocialMedias.FirstOrDefault(x => x.ID == model.ID);
                    string oldImagePath = social.ImagePath;
                    social.Name = model.Name;
                    social.Link = model.Link;
                    if (model.ImagePath != null)
                        social.ImagePath = model.ImagePath;
                    social.LastUpdateUserID = session.UserID;
                    social.LastUpdateDate = DateTime.Now;
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

        public string DeleteSocialMedia(int id, SessionDTO session)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    SocialMedia social = db.SocialMedias.FirstOrDefault(x => x.ID == id);
                    string oldImagePath = social.ImagePath;
                    social.isDeleted = true;
                    social.DeletedDate = DateTime.Now;
                    social.LastUpdateUserID = session.UserID;
                    social.LastUpdateDate = DateTime.Now;
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
}