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
    public class UserController : BaseController
    {
        UserBLL bll = new UserBLL();

        public ActionResult UserList()
        {
            List<UserDTO> model = new List<UserDTO>();
            model = bll.GetUsers();
            return View(model);
        }

        public ActionResult AddUser()
        {
            UserDTO model = new UserDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO model)
        {
            if (model.UserImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                HttpPostedFileBase postedFile = model.UserImage;
                string fileName = "";
                Bitmap UserImage = new Bitmap(postedFile.InputStream);
                Bitmap resizedImage = new Bitmap(UserImage, 200, 200);
                string ext = Path.GetExtension(postedFile.FileName);
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                {
                    fileName = Guid.NewGuid() + postedFile.FileName;
                    string path = Server.MapPath("~/Areas/Admin/Content/UserImages/" + fileName);
                    resizedImage.Save(path);
                    model.Imagepath = fileName;
                    bll.AddUser(model, session);
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new UserDTO();
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

            return View(model);
        }

        public ActionResult UpdateUser(int id)
        {
            UserDTO model = new UserDTO();
            model = bll.GetUserWithID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.UserImage != null)
                {
                    HttpPostedFileBase postedFile = model.UserImage;
                    string fileName = "";
                    Bitmap UserImage = new Bitmap(postedFile.InputStream);
                    Bitmap resizedImage = new Bitmap(UserImage, 200, 200);
                    string ext = Path.GetExtension(postedFile.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".gif")
                    {
                        fileName = Guid.NewGuid() + postedFile.FileName;
                        string path = Server.MapPath("~/Areas/Admin/Content/UserImages/" + fileName);
                        resizedImage.Save(path);
                        model.Imagepath = fileName;
                    }
                }

                SessionDTO session = (SessionDTO)Session["UserInfo"];
                string oldImagePath = bll.UpdateUser(model, session);
                if (model.UserImage != null)
                {
                    string oldPath = Server.MapPath("~/Areas/Admin/Content/UserImages/" + oldImagePath);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
            }

            return View(model);
        }
        
        public JsonResult DeleteUser(int id)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            string oldImagePath = bll.DeleteUser(id, session);
            string oldPath = Server.MapPath("~/Areas/Admin/Content/UserImages/" + oldImagePath);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            return Json("");
        }
    }
}