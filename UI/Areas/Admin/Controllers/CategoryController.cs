using System.Collections.Generic;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryBLL bll = new CategoryBLL();

        // GET
        public ActionResult CategoryList()
        {
            List<CategoryDTO> dtoList = bll.GetCategories();
            return View(dtoList);
        }

        public ActionResult AddCategory()
        {
            CategoryDTO dto = new CategoryDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bll.AddCategory(dto, session))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            return View(dto);
        }

        public ActionResult UpdateCategory(int id)
        {
            CategoryDTO dto = bll.GetCategoryWithID(id);
            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                if (bll.UpdateCategory(dto, session))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            return View(dto);
        }

        public JsonResult DeleteCategory(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            List<PostImageDTO> postimagelist = bll.DeleteCategory(id, session);
            foreach (var item in postimagelist)
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