using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        ContactBLL bll = new ContactBLL();
        public ActionResult UnreadMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = bll.GetUnreadMessages();
            return View(list);
        }
        
        public ActionResult AllMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = bll.GetAllMessages();
            return View(list);
        }
        
        public ActionResult ReadMessage2(int id)
        {
            SessionDTO session = (SessionDTO) Session["UserInfo"];
            bll.ReadMessage(id, session);
            return RedirectToAction("AllMessages");
        }
        
        public ActionResult ReadMessage(int id)
        {
            SessionDTO session = (SessionDTO) Session["UserInfo"];
            bll.ReadMessage(id, session);
            return RedirectToAction("UnreadMessages");
        }
        
        public JsonResult DeleteMessage(int id)
        {
            SessionDTO session = (SessionDTO) Session["UserInfo"];
            bll.DeleteMessage(id, session);
            return Json("");
        }
    }
}