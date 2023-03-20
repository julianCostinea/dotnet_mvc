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
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bll.ApproveComment(ID, session);
            return RedirectToAction("UnapprovedComments", "Comment");
        }
        public ActionResult ApproveComment2(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bll.ApproveComment(ID, session);
            return RedirectToAction("AllComments", "Comment");
        }
        
        public JsonResult DeleteComment(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bll.DeleteComment(ID, session);
            return Json("");
        }
    }
}