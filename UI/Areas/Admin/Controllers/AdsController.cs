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
    public class AdsController : BaseController
    {
        AdsBLL bll = new AdsBLL();

        // GET
        public ActionResult AdsList()
        {
            List<AdsDTO> AdsList = new List<AdsDTO>();
            AdsList = bll.GetAds();
            return View(AdsList);
        }

        public ActionResult AddAds()
        {
            AdsDTO dto = new AdsDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult AddAds(AdsDTO dto)
        {
            if (dto.AdsImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = dto.AdsImage;
                string fileName = "";
                Bitmap AdsImage = new Bitmap(postedFile.InputStream);
                Bitmap resizedImage = new Bitmap(AdsImage, 200, 200);
                string ext = Path.GetExtension(postedFile.FileName);
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                {
                    fileName = Guid.NewGuid() + postedFile.FileName;
                    string path = Server.MapPath("~/Areas/Admin/Content/AdsImage/" + fileName);
                    resizedImage.Save(path);
                    dto.ImagePath = fileName;
                    bll.AddAds(dto);
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    dto = new AdsDTO();
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

            return View(dto);
        }

        public ActionResult UpdateAds(int ID)
        {
            AdsDTO dto = bll.GetAdsWithID(ID);
            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateAds(AdsDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.AdsImage != null)
                {
                    HttpPostedFileBase postedFile = model.AdsImage;
                    string fileName = "";
                    Bitmap AdsImage = new Bitmap(postedFile.InputStream);
                    Bitmap resizedImage = new Bitmap(AdsImage, 200, 200);
                    string ext = Path.GetExtension(postedFile.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                    {
                        fileName = Guid.NewGuid() + postedFile.FileName;
                        string path = Server.MapPath("~/Areas/Admin/Content/AdsImage/" + fileName);
                        resizedImage.Save(path);
                        model.ImagePath = fileName;
                    }
                }

                string oldImagePath = bll.UpdateAds(model);
                if (model.AdsImage != null)
                {
                    string oldPath = Server.MapPath("~/Areas/Admin/Content/AdsImage/" + oldImagePath);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }

            return View(model);
        }

        public JsonResult DeleteAds(int ID)
        {
            string imagePath = bll.DeleteAds(ID);
            string oldPath = Server.MapPath("~/Areas/Admin/Content/AdsImage/" + imagePath);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            return Json("");
        }
    }
}