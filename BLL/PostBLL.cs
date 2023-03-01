﻿using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class PostBLL
    {
        PostDAO dao = new PostDAO();
        public bool AddPost(PostDTO model)
        {
            Post post = new Post();
            post.Title = model.Title;
            post.ShortContent = model.ShortContent;
            post.PostContent = model.PostContent;
            post.Slider = model.Slider;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.Notification = model.Notification;
            post.CategoryID = model.CategoryID;
            post.SeoLink = SeoLink.GenerateUrl(model.Title);
            post.LanguageName = model.Language;
            post.AddDate = DateTime.Now;
            post.AddUserID = UserStatic.UserID;
            post.LastUpdateUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;
            int ID = dao.AddPost(post);
            LogDAO.AddLog(General.ProcessType.PostAdd, General.TableName.Post, ID);
            SavePostImage(model.PostImages, ID);
            AddTag(model.TagText, ID);
            return true;
        }

        private void AddTag(string tTagText, int PostID)
        {
            string[] tags;
            tags = tTagText.Split(',');
            List<PostTag> taglist = new List<PostTag>();
            foreach (var item in tags)
            {
                taglist.Add(new PostTag()
                {
                    TagContent = item,
                    PostID = PostID,
                    AddDate = DateTime.Now,
                    LastUpdateUserID = UserStatic.UserID,
                    LastUpdateDate = DateTime.Now,
                });
            }

            foreach (var item in taglist)
            {
                int tagID = dao.AddTag(item);
                LogDAO.AddLog(General.ProcessType.TagAdd, General.TableName.Tag, tagID);
            }
        }

        void SavePostImage(List<PostImageDTO> list, int PostID)
        {
            List<PostImage> imagelist = new List<PostImage>();
            foreach (var item in list)
            {
                imagelist.Add(new PostImage()
                {
                    ImagePath = item.ImagePath,
                    PostID = PostID,
                    AddDate = DateTime.Now,
                    LastUpdateUserID = UserStatic.UserID,
                    LastUpdateDate = DateTime.Now,
                });
            }

            foreach (var item in imagelist)
            {
                int imageID = dao.AddImage(item);
                LogDAO.AddLog(General.ProcessType.ImageAdd, General.TableName.Image, imageID);
            }
        }

        public List<PostDTO> GetPosts()
        {
            return dao.GetPosts();
        }

        public PostDTO GetPostWithID(int id)
        {
            PostDTO dto = new PostDTO();
            dto = dao.GetPostWithID(id);
            dto.PostImages = dao.GetPostImagesWithPostID(id);
            List<PostTag> taglist = dao.GetPostTagsWithPostID(id);
            string tagtext = "";
            foreach (var item in taglist)
            {
                tagtext += item.TagContent + ",";
            }
            dto.TagText = tagtext;
            return dto;
        }

        public bool UpdatePost(PostDTO model)
        {
            model.SeoLink = SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);
            LogDAO.AddLog(General.ProcessType.AdsAdd, General.TableName.Post, model.ID);
            if (model.PostImages != null)
            {
                SavePostImage(model.PostImages, model.ID);
            }

            dao.DeleteTags(model.ID);
            AddTag(model.TagText, model.ID);
            return true;
        }
    }
}