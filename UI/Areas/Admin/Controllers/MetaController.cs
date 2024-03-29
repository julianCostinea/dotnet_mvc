﻿using System.Collections.Generic;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : BaseController
    {
        MetaBLL bll = new MetaBLL();

        // GET
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMeta()
        {
            MetaDTO dto = new MetaDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult AddMeta(MetaDTO model)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bll.AddMeta(model, session))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
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

            MetaDTO newModel = new MetaDTO();
            return View(newModel);
        }

        public ActionResult MetaList()
        {
            List<MetaDTO> model = new List<MetaDTO>();
            model = bll.GetMetaData();
            return View(model);
        }

        public ActionResult UpdateMeta(int ID)
        {
            MetaDTO model = new MetaDTO();
            model = bll.GetMetaWithID(ID);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO model)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bll.UpdateMeta(model, session))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    ModelState.Clear();
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

            MetaDTO newModel = new MetaDTO();
            return View(newModel);
        }

        public JsonResult DeleteMeta(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bll.DeleteMeta(ID, session);
            return Json("");
        }
    }
}