using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Mt.Core;
using SanXing.Data;
using SanXing.Data.Service;
using SanXing.Data.Models;
using SanXing.ViewModels;
using System.IO;
using System.Text;
using SanXing.Web.Framework.Helpers;
using SanXing.Web.Framework.Mvc;

namespace SanXing.Web.Controllers
{
    public class CostController : BaseController
    {


        private ICostTypeService CostTypeService;
        private ICostService CostService;
        public CostController(
        ICostTypeService CostTypeService,
        ICostService CostService
      )
        {
            this.CostTypeService = CostTypeService;
            this.CostService = CostService;
        }


        // GET: /Cost/
        public ActionResult Index(int Month = 0)
        {
            var month = new MonthTableViewModel();
            if (Month == 0)
            {
                Month = DateTime.Now.Month;
            }
            var time = new DateTime(DateTime.Now.Year, Month, 1);
            month.Year = time.Year;
            month.Month = time.Month;
            month.UID = CookieHelper.UID;
            month.FirstRowIndex = (int)time.DayOfWeek;
            month.DayCount = Utilities.GetMonthDayCount(DateTime.Now.Year, Month);
            month.MaxRows = (int)Math.Ceiling(((double)month.DayCount + month.FirstRowIndex) / 7);
            ViewBag.MonthTable = month;

            return View();
        }


        public ActionResult YearMonthWeek()
        {
            return PartialView();
        }

        public ActionResult Create(string date = null)
        {
            var createTime = DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                createTime = Convert.ToDateTime(date);
            }
            var model = new CostModel();
            ViewBag.Data_CostTypeID = Utilities.GetSelectListData(
                CostTypeService.GetAll()
                .Where(x => x.PID.Equals(null) && !x.Deleted)
                .OrderBy(x => x.Code)
                .ToList(), x => x.ID, x => x.CateName, true);

            return PartialView(model);
        }
    }
}