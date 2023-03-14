using System.Collections.Generic;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        PostBLL bll = new PostBLL();
        // GET
        public ActionResult UnapprovedComments()
        {
            List<CommentDTO> commentlist = new List<CommentDTO>();
            commentlist = bll.GetComments();
            return View(commentlist);
        }
        
        public ActionResult AllComments()
        {
            List<CommentDTO> commentlist = new List<CommentDTO>();
            commentlist = bll.GetAllComments();
            return View(commentlist);
        }
        
        public ActionResult ApproveComment(int ID)
        {
            bll.ApproveComment(ID);
            return RedirectToAction("UnapprovedComments", "Comment");
        }
        public ActionResult ApproveComment2(int ID)
        {
            bll.ApproveComment(ID);
            return RedirectToAction("AllComments", "Comment");
        }
        
        public JsonResult DeleteComment(int ID)
        {
            bll.DeleteComment(ID);
            return Json("");
        }
    }
}