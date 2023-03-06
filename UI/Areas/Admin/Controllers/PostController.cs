using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        PostBLL bll = new PostBLL();

        // GET
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostList()
        {
            List<PostDTO> postlist = new List<PostDTO>();
            postlist = bll.GetPosts();
            return View(postlist);
        }

        public ActionResult AddPost()
        {
            PostDTO model = new PostDTO();
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDTO model)
        {
            if (model.PostImage[0] == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                foreach (var item in model.PostImage)
                {
                    Bitmap image = new Bitmap(item.InputStream);
                    string ext = Path.GetExtension(item.FileName);
                    if (ext != ".jpg" && ext != ".png" && ext != ".jpeg")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropdown();
                        return View(model);
                    }
                }

                List<PostImageDTO> imagelist = new List<PostImageDTO>();
                foreach (var postefile in model.PostImage)
                {
                    Bitmap image = new Bitmap(postefile.InputStream);
                    Bitmap resizeimage = new Bitmap(image, 750, 422);
                    string filename = Guid.NewGuid().ToString() + postefile.FileName;
                    resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                    imagelist.Add(new PostImageDTO()
                    {
                        ImagePath = filename
                    });
                }

                model.PostImages = imagelist;
                if (bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new PostDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }

        public ActionResult UpdatePost(int id)
        {
            PostDTO model = new PostDTO();
            model = bll.GetPostWithID(id);
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            model.isUpdate = true;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(PostDTO model)
        {
            IEnumerable<SelectListItem> selectlist = CategoryBLL.GetCategoriesForDropdown();
            if (ModelState.IsValid)
            {
                if (model.PostImage[0] != null)
                {
                    foreach (var item in model.PostImage)
                    {
                        Bitmap image = new Bitmap(item.InputStream);
                        string ext = Path.GetExtension(item.FileName);
                        if (ext != ".jpg" && ext != ".png" && ext != ".jpeg")
                        {
                            ViewBag.ProcessState = General.Messages.ExtensionError;
                            model.Categories = CategoryBLL.GetCategoriesForDropdown();
                            return View(model);
                        }
                    }

                    List<PostImageDTO> imagelist = new List<PostImageDTO>();
                    foreach (var postedfile in model.PostImage)
                    {
                        Bitmap image = new Bitmap(postedfile.InputStream);
                        Bitmap resizeimage = new Bitmap(image, 750, 422);
                        string filename = Guid.NewGuid().ToString() + postedfile.FileName;
                        resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                        imagelist.Add(new PostImageDTO()
                        {
                            ImagePath = filename
                        });
                    }
                    model.PostImages = imagelist;
                }

                if (bll.UpdatePost(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            model = bll.GetPostWithID(model.ID);
            model.Categories = selectlist;
            model.isUpdate = true;
            return View(model);
        }
        
        public JsonResult DeletePostImage(int id)
        {
            string imagePath = bll.DeletePostImage(id);
            string oldPath = Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagePath);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            return Json("");
        }
        
        public JsonResult DeletePost(int id)
        {
            List<PostImageDTO> imagelist =  bll.DeletePost(id);
            foreach (var item in imagelist)
            {
                string oldPath = Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }
            return Json("");
        }
    }
}