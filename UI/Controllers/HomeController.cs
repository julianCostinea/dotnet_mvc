 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        LayoutBLL layoutbll = new LayoutBLL();
        GeneralBLL bll = new GeneralBLL();
        public ActionResult Index()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetAllPosts();
            return View(dto);
        }
        
        public ActionResult CategoryPostList(string CategoryName)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            return View();
        }
        public ActionResult PostDetail(int ID)
        {HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetPostDetail(ID);
            return View(dto);
        }
    }
}