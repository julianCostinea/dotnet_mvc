using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class PostDAO
    {
        public int AddPost(Post post)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                }

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
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.PostImages.Add(item);
                    db.SaveChanges();
                }

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
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                db.PostTags.Add(item);
                db.SaveChanges();
            }

            return item.ID;
        }

        public List<PostDTO> GetPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                // var postlist = (from p in db.Posts.Where(x => x.isDeleted == 0)
                //     join c in db.Categories on p.CategoryID equals c.ID
                //     select new PostDTO()
                //     {
                //         ID = p.ID,
                //         Title = p.Title,
                //         CategoryName = c.CategoryName,
                //         AddDate = p.AddDate,
                //     }).ToList();
                // foreach (var item in postlist)
                // {
                //     PostDTO dto = new PostDTO();
                //     dto.ID = item.ID;
                //     dto.Title = item.Title;
                //     dto.CategoryName = item.CategoryName;
                //     dto.AddDate = item.AddDate;
                //     dtolist.Add(dto);
                // }
                List<PostDTO> postlist = (from p in db.Posts.Where(x => x.isDeleted == 0)
                    join c in db.Categories on p.CategoryID equals c.ID
                    select new PostDTO()
                    {
                        ID = p.ID,
                        Title = p.Title,
                        CategoryName = c.CategoryName,
                        AddDate = p.AddDate,
                    }).Select(x => new PostDTO()
                {
                    ID = x.ID,
                    Title = x.Title,
                    CategoryName = x.CategoryName,
                    AddDate = x.AddDate,
                }).ToList();
            }

            return dtolist;
        }

        public PostDTO GetPostWithID(int id)
        {
            PostDTO dto = new PostDTO();

            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.FirstOrDefault(x => x.ID == id);
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
            }

            return dto;
        }

        public List<PostImageDTO> GetPostImagesWithPostID(int id)
        {
            List<PostImageDTO> dtolist = new List<PostImageDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<PostImage> list = db.PostImages.Where(x => x.PostID == id && x.isDeleted == false).ToList();
                foreach (var item in list)
                {
                    PostImageDTO dto = new PostImageDTO();
                    dto.ID = item.ID;
                    dto.ImagePath = item.ImagePath;
                    dtolist.Add(dto);
                }
            }

            return dtolist;
        }

        public List<PostTag> GetPostTagsWithPostID(int id)
        {
            List<PostTag> list = new List<PostTag>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                list = db.PostTags.Where(x => x.PostID == id && x.isDeleted == false).ToList();
            }

            return list;
        }

        public void UpdatePost(PostDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
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
        }

        public void DeleteTags(int modelId)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
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

        public string DeletePostImage(int id)
        {
            try
            {
                string path = "";
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    PostImage img = db.PostImages.FirstOrDefault(x => x.ID == id);
                    path = img.ImagePath;
                    img.isDeleted = true;
                    img.DeletedDate = DateTime.Now;
                    img.LastUpdateUserID = UserStatic.UserID;
                    img.LastUpdateDate = DateTime.Now;
                    db.SaveChanges();
                }

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
            List<PostImageDTO> dtolist = new List<PostImageDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Post post = db.Posts.FirstOrDefault(x => x.ID == id);
                post.isDeleted = 1;
                post.DeletedDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserID;
                post.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
                List<PostImage> imagelist = db.PostImages.Where(x => x.PostID == id && x.isDeleted == false).ToList();
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
            }

            return dtolist;
        }

        public List<PostDTO> GetHotNews()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
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
                foreach (var item in postlist)
                {
                    PostDTO dto = new PostDTO();
                    dto.ID = item.ID;
                    dto.Title = item.Title;
                    dto.CategoryName = item.CategoryName;
                    dto.AddDate = item.AddDate;
                    dtolist.Add(dto);
                }
            }

            return dtolist;
        }

        public void AddComment(Comment comment)
        {
            try
            {
                using (POSTDATAEntities db = new POSTDATAEntities())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
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
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
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
            }

            return dtolist;
        }

        public void ApproveComment(int id)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Comment cmt = db.Comments.FirstOrDefault(x => x.ID == id);
                cmt.isApproved = true;
                cmt.LastUpdateUserID = UserStatic.UserID;
                cmt.LastUpdateDate = DateTime.Now;
                cmt.ApproveDate = DateTime.Now;
                cmt.ApproveUserID = UserStatic.UserID;
                db.SaveChanges();
            }
        }

        public void DeleteComment(int id)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Comment cmt = db.Comments.FirstOrDefault(x => x.ID == id);
                cmt.isDeleted = true;
                cmt.DeletedDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                cmt.LastUpdateDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public List<CommentDTO> GetAllComments()
        {
            List<CommentDTO> dtolist = new List<CommentDTO>();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<CommentDTO> list = (from c in db.Comments.Where(x => x.isDeleted == false)
                    join p in db.Posts on c.PostID equals p.ID
                    select new
                    {
                        ID = c.ID,
                        PostTitle = p.Title,
                        Email = c.Email,
                        Content = c.CommentContent,
                        AddDate = c.AddDate,
                        isapproved = c.isApproved
                    }).Select(x => new CommentDTO()
                {
                    ID = x.ID,
                    PostTitle = x.PostTitle,
                    Email = x.Email,
                    CommentContent = x.Content,
                    AddDate = x.AddDate,
                    isApproved = x.isapproved
                }).ToList();
                // foreach (var item in list)
                // {
                //     CommentDTO dto = new CommentDTO();
                //     dto.ID = item.ID;
                //     dto.PostTitle = item.PostTitle;
                //     dto.Email = item.Email;
                //     dto.CommentContent = item.Content;
                //     dto.AddDate = item.AddDate;
                //     dto.isApproved = item.isapproved;
                //     dtolist.Add(dto);
                // }
            }

            return dtolist;
        }

        public int GetMessageCount()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                return db.Contacts.Count(x => x.isDeleted == false && x.isRead == false);
            }
        }

        public int GetCommentCount()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                return db.Comments.Count(x => x.isDeleted == false && x.isApproved == false);
            }
        }

        public CountDTO GetALlCounts()
        {
            CountDTO dto = new CountDTO();
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                dto.PostCount = db.Posts.Count(x => x.isDeleted == 0);
                dto.CommentCount = db.Comments.Count(x => x.isDeleted == false);
                dto.MessageCount = db.Contacts.Count(x => x.isDeleted == false);
                dto.ViewCount = db.Posts.Where(x => x.isDeleted == 0).Sum(x => x.ViewCount);
            }

            return dto;
        }
    }
}