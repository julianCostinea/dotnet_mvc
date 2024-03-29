﻿using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class VideoDAO
    {
        public int AddVideo(Video video)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
            try
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return video.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            }
        }

        public List<VideoDTO> GetVideos()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<Video> videolist = db.Videos.Where(x => x.isDeleted == false).ToList();
                List<VideoDTO> dtolist = new List<VideoDTO>();
                foreach (Video video in videolist)
                {
                    VideoDTO dto = new VideoDTO();
                    dto.ID = video.ID;
                    dto.VideoPath = video.VideoPath;
                    dto.OriginalVideoPath = video.OriginalVideoPath;
                    dto.Title = video.Title;
                    dto.AddDate = video.AddDate;
                    dtolist.Add(dto);
                }

                return dtolist;
            }
        }

        public VideoDTO GetVideoWithID(int id)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Video video = db.Videos.FirstOrDefault(x => x.ID == id);
                VideoDTO dto = new VideoDTO();
                dto.ID = video.ID;
                dto.Title = video.Title;
                dto.OriginalVideoPath = video.OriginalVideoPath;
                return dto;
            }
        }

        public void UpdateVideo(VideoDTO model, SessionDTO session)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Video video = db.Videos.FirstOrDefault(x => x.ID == model.ID);
                    video.Title = model.Title;
                    video.VideoPath = model.VideoPath;
                    video.OriginalVideoPath = model.OriginalVideoPath;
                    video.LastUpdateDate = DateTime.Now;
                    video.LastUpdateUserID = session.UserID;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void DeleteVideo(int id, SessionDTO session)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Video video = db.Videos.FirstOrDefault(x => x.ID == id);
                video.isDeleted = true;
                video.DeletedDate = DateTime.Now;
                video.LastUpdateUserID = session.UserID;
                video.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
            }
        }
    }
}