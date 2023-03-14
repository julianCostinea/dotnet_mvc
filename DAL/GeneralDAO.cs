using System.Collections.Generic;
using System.Linq;
using DTO;

namespace DAL
{
    public class GeneralDAO : PostContext
    {
        public List<PostDTO> GetSliderPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Slider == true)
                join c in db.Categories on p.CategoryID equals c.ID
                select new
                {
                    postID = p.ID,
                    postTitle = p.Title,
                    categoryName = c.CategoryName,
                    seolink = p.SeoLink,
                    viewcount = p.ViewCount,
                    Adddate = p.AddDate,
                }).Take(8).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.postTitle;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.FirstOrDefault(x => x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }

            return dtolist;
        }

        public List<PostDTO> GetBreakingPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == 0)
                join c in db.Categories on p.CategoryID equals c.ID
                select new
                {
                    postID = p.ID,
                    postTitle = p.Title,
                    categoryName = c.CategoryName,
                    seolink = p.SeoLink,
                    viewcount = p.ViewCount,
                    Adddate = p.AddDate,
                }).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.postTitle;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.FirstOrDefault(x => x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }

            return dtolist;
        }

        public List<PostDTO> GetPopularPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Area2 == true)
                join c in db.Categories on p.CategoryID equals c.ID
                select new
                {
                    postID = p.ID,
                    postTitle = p.Title,
                    categoryName = c.CategoryName,
                    seolink = p.SeoLink,
                    viewcount = p.ViewCount,
                    Adddate = p.AddDate,
                }).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.postTitle;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.FirstOrDefault(x => x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }

            return dtolist;
        }

        public List<PostDTO> GetMostViewedPosts()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Area2 == true)
                join c in db.Categories on p.CategoryID equals c.ID
                select new
                {
                    postID = p.ID,
                    postTitle = p.Title,
                    categoryName = c.CategoryName,
                    seolink = p.SeoLink,
                    viewcount = p.ViewCount,
                    Adddate = p.AddDate,
                }).OrderByDescending(x => x.viewcount).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.postTitle;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.FirstOrDefault(x => x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }

            return dtolist;
        }

        public List<VideoDTO> GetVideos()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).Take(5)
                .ToList();
            foreach (var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.AddDate = item.AddDate;
                dtolist.Add(dto);
            }

            return dtolist;
        }

        public PostDTO GetPostDetail(int id)
        {
            Post post = db.Posts.FirstOrDefault(x => x.ID == id);
            post.ViewCount++;
            db.SaveChanges();
            PostDTO dto = new PostDTO();
            dto.ID = post.ID;
            dto.Title = post.Title;
            dto.ShortContent = post.ShortContent;
            dto.PostContent = post.PostContent;
            dto.Language = post.LanguageName;
            dto.SeoLink = post.SeoLink;
            dto.CategoryID = post.CategoryID;
            dto.CategoryName = db.Categories.FirstOrDefault(x => x.ID == dto.CategoryID).CategoryName;
            List<PostImage> images = db.PostImages.Where(x => x.PostID == post.ID).ToList();
            List<PostImageDTO> imageDTOs = new List<PostImageDTO>();
            foreach (var item in images)
            {
                PostImageDTO imageDTO = new PostImageDTO();
                imageDTO.ID = item.ID;
                imageDTO.ImagePath = item.ImagePath;
                imageDTOs.Add(imageDTO);
            }

            dto.PostImages = imageDTOs;
            dto.CommentCount = db.Comments.Where(x => x.PostID == post.ID).Count();
            List<Comment> comments = db.Comments.Where(x => x.PostID == post.ID).ToList();
            List<CommentDTO> commentDTOs = new List<CommentDTO>();
            foreach (var item in comments)
            {
                CommentDTO commentDTO = new CommentDTO();
                commentDTO.ID = item.ID;
                commentDTO.CommentContent = item.CommentContent;
                commentDTO.AddDate = item.AddDate;
                commentDTO.Name = item.NameSurname;
                commentDTO.Email = item.Email;
                commentDTOs.Add(commentDTO);
            }

            dto.CommentList = commentDTOs;
            List<PostTag> tags = db.PostTags.Where(x => x.PostID == post.ID).ToList();
            List<TagDTO> tagDTOs = new List<TagDTO>();
            foreach (var item in tags)
            {
                TagDTO tagDTO = new TagDTO();
                tagDTO.ID = item.ID;
                tagDTO.TagContent = item.TagContent;
                tagDTOs.Add(tagDTO);
            }

            dto.TagList = tagDTOs;
            return dto;
        }

        public List<VideoDTO> GetAllVideos()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate)
                .ToList();
            foreach (var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.AddDate = item.AddDate;
                dtolist.Add(dto);
            }

            return dtolist;
        }

        public List<PostDTO> GetCategoryPostList(int categoryId)
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == 0 && x.CategoryID == categoryId)
                join c in db.Categories on p.CategoryID equals c.ID
                select new
                {
                    postID = p.ID,
                    postTitle = p.Title,
                    categoryName = c.CategoryName,
                    seolink = p.SeoLink,
                    viewcount = p.ViewCount,
                    Adddate = p.AddDate,
                }).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.postID;
                dto.Title = item.postTitle;
                dto.CategoryName = item.categoryName;
                dto.ViewCount = item.viewcount;
                dto.SeoLink = item.seolink;
                PostImage image = db.PostImages.FirstOrDefault(x => x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.PostID == item.postID && x.isDeleted == false).Count();
                dto.AddDate = item.Adddate;
                dtolist.Add(dto);
            }

            return dtolist;
        }
    }
}