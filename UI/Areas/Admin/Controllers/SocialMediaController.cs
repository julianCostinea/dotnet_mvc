using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class SocialMediaController : BaseController
    {
        SocialMediaBLL bll = new SocialMediaBLL();

        // GET
        public ActionResult AddSocialMedia()
        {
            SocialMediaDTO model = new SocialMediaDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO model)
        {
            if (model.SocialImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.SocialImage;
                Bitmap SocialMedia = new Bitmap(postedFile.InputStream);
                string ext = Path.GetExtension(postedFile.FileName);
                string fileName = "";
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                {
                    fileName = Guid.NewGuid() + postedFile.FileName;
                    string path = Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + fileName);
                    SocialMedia.Save(path);
                    model.ImagePath = fileName;

                    if (bll.AddSocialMedia(model))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        model = new SocialMediaDTO();
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.GeneralError;
                    }
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.ExtensionError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            SocialMediaDTO newModel = new SocialMediaDTO();
            return View(newModel);
        }

        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> model = new List<SocialMediaDTO>();
            model = bll.GetSocialMedias();
            return View(model);
        }

        public ActionResult UpdateSocialMedia(int id)
        {
            SocialMediaDTO model = bll.GetSocialMediaWithID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.SocialImage != null)
                {
                    HttpPostedFileBase postedFile = model.SocialImage;
                    Bitmap SocialMedia = new Bitmap(postedFile.InputStream);
                    string ext = Path.GetExtension(postedFile.FileName);
                    string fileName = "";
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                    {
                        fileName = Guid.NewGuid() + postedFile.FileName;
                        string path = Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + fileName);
                        SocialMedia.Save(path);
                        model.ImagePath = fileName;
                    }
                }

                string oldImagePath = bll.UpdateSocialMedia(model);
                if (model.SocialImage != null)
                {
                    string path = Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }

            return View(model);
        }
        
        public JsonResult DeleteSocialMedia(int id)
        {
            string oldImagePath = bll.DeleteSocialMedia(id);
            string path = Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return Json("");
        }
    }
}