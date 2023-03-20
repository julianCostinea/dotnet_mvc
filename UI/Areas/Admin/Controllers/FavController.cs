using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class FavController : BaseController
    {
        FavBLL bll = new FavBLL();

        // GET
        public ActionResult UpdateFav()
        {
            FavDTO dto = new FavDTO();
            dto = bll.GetFav();
            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateFav(FavDTO model)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.FavImage != null)
                {
                    string favname = "";
                    HttpPostedFileBase postedFileFav = model.FavImage;
                    Bitmap FavImage = new Bitmap(postedFileFav.InputStream);
                    Bitmap resizeFavImage = new Bitmap(FavImage, 32, 32);
                    string ext = Path.GetExtension(postedFileFav.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".ico")
                    {
                        favname = Guid.NewGuid() + postedFileFav.FileName;
                        string path = Server.MapPath("~/Areas/Admin/Content/FavImage/" + favname);
                        resizeFavImage.Save(path);
                        model.Fav = favname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }

                if (model.LogoImage != null)
                {
                    string favname = "";
                    HttpPostedFileBase postedFileFav = model.LogoImage;
                    Bitmap FavImage = new Bitmap(postedFileFav.InputStream);
                    Bitmap resizeFavImage = new Bitmap(FavImage, 32, 32);
                    string ext = Path.GetExtension(postedFileFav.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".ico")
                    {
                        favname = Guid.NewGuid() + postedFileFav.FileName;
                        string path = Server.MapPath("~/Areas/Admin/Content/FavImage/" + favname);
                        resizeFavImage.Save(path);
                        model.Logo = favname;
                    }
                    else
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                    }
                }

                FavDTO returndto = new FavDTO();
                returndto = bll.UpdateFav(model, session);
                if (model.FavImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Fav));
                    }
                }

                if (model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + returndto.Logo));
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }

            return View(model);
        }
    }
}