using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : Controller
    {
        AddressBLL bll = new AddressBLL();

        // GET
        public ActionResult AddressList()
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddresses();
            return View(list);
        }

        public ActionResult AddAddress()
        {
            AddressDTO dto = new AddressDTO();
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new AddressDTO();
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

        public ActionResult UpdateAddress(int id)
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddresses();
            AddressDTO dto = list.FirstOrDefault(x => x.ID == id);
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
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

        public JsonResult DeleteAddress(int id)
        {
            bll.DeleteAddress(id);
            return Json("");
        }
    }
}