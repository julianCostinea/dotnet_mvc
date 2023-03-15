using System.Collections.Generic;
using System.Web.Mvc;
using BLL;
using DTO;

namespace UI.Areas.Admin.Controllers
{
    public class LogController : BaseController
    {
        LogBLL bll = new LogBLL();
        // GET
        public ActionResult LogList()
        {
            List<LogDTO> list = new List<LogDTO>();
            list = bll.GetLogs();
            return View(list);
        }
    }
}