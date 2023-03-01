using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class PostDAO : PostContext
    {
        public int AddPost(Post post)
        {
            try
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return post.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int AddImage(PostImage item)
        {
            try
            {
                db.PostImages.Add(item);
                db.SaveChanges();
                return item.ID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int AddTag(PostTag item)
        {
            db.PostTags.Add(item);
            db.SaveChanges();
            return item.ID;
        }

        public List<PostDTO> GetPosts()
        {
            var postlist = (from p in db.Posts.Where(x => x.isDeleted == 0)
                join c in db.Categories on p.CategoryID equals c.ID
                select new PostDTO()
                {
                    ID = p.ID,
                    Title = p.Title,
                    CategoryName = c.CategoryName,
                    AddDate = p.AddDate,
                }).ToList();
            List<PostDTO> dtolist = new List<PostDTO>();
            foreach (var item in postlist)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.CategoryName = item.CategoryName;
                dto.AddDate = item.AddDate;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public PostDTO GetPostWithID(int id)
        {
            Post post = db.Posts.FirstOrDefault(x => x.ID == id);
            PostDTO dto = new PostDTO();
            dto.ID = post.ID;
            dto.Title = post.Title;
            dto.ShortContent = post.ShortContent;
            dto.PostContent = post.PostContent;
            dto.Slider = post.Slider;
            dto.Area1 = post.Area1;
            dto.Area2 = post.Area2;
            dto.Area3 = post.Area3;
            dto.Notification = post.Notification;
            dto.CategoryID = post.CategoryID;
            dto.SeoLink = post.SeoLink;
            dto.Language = post.LanguageName;
            return dto;
        }

        public List<PostImageDTO> GetPostImagesWithPostID(int id)
        {
            List<PostImage> list = db.PostImages.Where(x => x.PostID == id && x.isDeleted == false ).ToList();
            List<PostImageDTO> dtolist = new List<PostImageDTO>();
            foreach (var item in list)
            {
                PostImageDTO dto = new PostImageDTO();
                dto.ID = item.ID;
                dto.ImagePath = item.ImagePath;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostTag> GetPostTagsWithPostID(int id)
        {
            return db.PostTags.Where(x => x.PostID == id && x.isDeleted == false).ToList();
        }

        public void UpdatePost(PostDTO model)
        {
            Post post = db.Posts.FirstOrDefault(x => x.ID == model.ID);
            post.Title = model.Title;
            post.ShortContent = model.ShortContent;
            post.PostContent = model.PostContent;
            post.Slider = model.Slider;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.Notification = model.Notification;
            post.CategoryID = model.CategoryID;
            post.SeoLink = model.SeoLink;
            post.LanguageName = model.Language;
            post.LastUpdateUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;
            db.SaveChanges();
        }

        public void DeleteTags(int modelId)
        {
            List<PostTag> list = db.PostTags.Where(x => x.PostID == modelId && x.isDeleted == false).ToList();
            foreach (var item in list)
            {
                item.isDeleted = true;
                item.DeletedDate = DateTime.Now;
                item.LastUpdateUserID = UserStatic.UserID;
                item.LastUpdateDate = DateTime.Now;
            }
            db.SaveChanges();
        }
    }
}