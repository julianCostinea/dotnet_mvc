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
            List<PostImage> list = db.PostImages.Where(x => x.PostID == id && x.isDeleted == false).ToList();
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

        public string DeletePostImage(int id)
        {
            try
            {
                PostImage img = db.PostImages.FirstOrDefault(x => x.ID == id);
                string path = img.ImagePath;
                img.isDeleted = true;
                img.DeletedDate = DateTime.Now;
                img.LastUpdateUserID = UserStatic.UserID;
                img.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
                return path;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<PostImageDTO> DeletePost(int id)
        {
            Post post = db.Posts.FirstOrDefault(x => x.ID == id);
            post.isDeleted = 1;
            post.DeletedDate = DateTime.Now;
            post.LastUpdateUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;
            db.SaveChanges();
            List<PostImage> imagelist = db.PostImages.Where(x => x.PostID == id && x.isDeleted == false).ToList();
            List<PostImageDTO> dtolist = new List<PostImageDTO>();
            foreach (var item in imagelist)
            {
                PostImageDTO dto = new PostImageDTO();
                dto.ImagePath = item.ImagePath;
                item.isDeleted = true;
                item.DeletedDate = DateTime.Now;
                item.LastUpdateUserID = UserStatic.UserID;
                item.LastUpdateDate = DateTime.Now;
                dtolist.Add(dto);
            }

            db.SaveChanges();
            return dtolist;
        }

        public List<PostDTO> GetHotNews()
        {
            var postlist = (from p in db.Posts.Where(x => x.isDeleted == 0 && x.Area1 == true)
                join c in db.Categories on p.CategoryID equals c.ID
                select new PostDTO()
                {
                    ID = p.ID,
                    Title = p.Title,
                    CategoryName = c.CategoryName,
                    AddDate = p.AddDate,
                    SeoLink = p.SeoLink
                }).Take(8).ToList();
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

        public void AddComment(Comment comment)
        {
            try
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<CommentDTO> GetComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            var list = (from c in db.Comments.Where(x => x.isDeleted == false && x.isApproved == false)
                join p in db.Posts on c.PostID equals p.ID
                select new
                {
                    ID = c.ID,
                    PostTitle = p.Title,
                    Email = c.Email,
                    Content = c.CommentContent,
                    AddDate = c.AddDate
                }).ToList();
            foreach (var item in list)
            {
                CommentDTO dto = new CommentDTO();
                dto.ID = item.ID;
                dto.PostTitle = item.PostTitle;
                dto.Email = item.Email;
                dto.CommentContent = item.Content;
                dto.AddDate = item.AddDate;
                dtolist.Add(dto);
            }

            return dtolist;
        }

        public void ApproveComment(int id)
        {
            Comment cmt = db.Comments.FirstOrDefault(x => x.ID == id);
            cmt.isApproved = true;
            cmt.LastUpdateUserID = UserStatic.UserID;
            cmt.LastUpdateDate = DateTime.Now;
            cmt.ApproveDate = DateTime.Now;
            cmt.ApproveUserID = UserStatic.UserID;
            db.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            Comment cmt = db.Comments.FirstOrDefault(x => x.ID == id);
            cmt.isDeleted = true;
            cmt.DeletedDate = DateTime.Now;
            cmt.LastUpdateUserID = UserStatic.UserID;
            cmt.LastUpdateDate = DateTime.Now;
            db.SaveChanges();
        }

        public List<CommentDTO> GetAllComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            var list = (from c in db.Comments.Where(x => x.isDeleted == false)
                join p in db.Posts on c.PostID equals p.ID
                select new
                {
                    ID = c.ID,
                    PostTitle = p.Title,
                    Email = c.Email,
                    Content = c.CommentContent,
                    AddDate = c.AddDate,
                    isapproved = c.isApproved
                }).ToList();
            foreach (var item in list)
            {
                CommentDTO dto = new CommentDTO();
                dto.ID = item.ID;
                dto.PostTitle = item.PostTitle;
                dto.Email = item.Email;
                dto.CommentContent = item.Content;
                dto.AddDate = item.AddDate;
                dto.isApproved = item.isapproved;
                dtolist.Add(dto);
            }

            return dtolist;
        }
    }
}