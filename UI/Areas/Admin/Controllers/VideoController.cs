using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {
        VideoBLL bll = new VideoBLL();

        public ActionResult VideoList()
        {
            List<VideoDTO> dtolist = bll.GetVideos();
            return View(dtolist);
        }

        // GET
        public ActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/" + path;
                model.VideoPath =
                    String.Format(
                        @"<iframe width = ""300"" height = ""200"" src = ""{0}"" frameborder = ""0""  allowfullscreen ></iframe> ",
                        mergelink);
                if (bll.AddVideo(model, session))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
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

            return View(model);
        }

        public ActionResult UpdateVideo(int id)
        {
            VideoDTO dto = bll.GetVideoWithID(id);
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/" + path;
                model.VideoPath =
                    String.Format(
                        @"<iframe width = ""300"" height = ""200"" src = ""{0}"" frameborder = ""0""  allowfullscreen ></iframe> ",
                        mergelink);
                if (bll.UpdateVideo(model, session))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
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

            return View(model);
        }

        public JsonResult DeleteVideo(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bll.DeleteVideo(id, session);
            return Json("");
        }
    }
}