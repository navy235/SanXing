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
            var costDate = DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                costDate = Convert.ToDateTime(date);
            }
            var model = new CostModel()
            {
                CostDate = costDate
            };

            ViewBag.Data_CostTypeID = GetGroupSelect();

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CostModel model)
        {
            ServiceResult result = new ServiceResult();
            if (!ModelState.IsValid)
            {
                result.AddModelStateError(ModelState);
            }
            else
            {
                try
                {
                    var entity = model.ToEntity();
                    entity.CreateTime = DateTime.Now;
                    entity.LastTime = DateTime.Now;
                    entity.UserID = CookieHelper.UID;
                    CostService.Insert(entity);
                    entity.CostType = CostTypeService.Single(entity.CostTypeID);
                    var CostItem = new CostListItemModel()
                    {
                        CostDate = entity.CostDate,
                        CostTypeID = entity.CostTypeID,
                        CostTypeName = entity.CostType.CateName,
                        CreateTime = entity.CreateTime,
                        Deleted = entity.Deleted,
                        Description = entity.Description,
                        ID = entity.ID,
                        LastTime = entity.LastTime,
                        Money = entity.Money,
                        UserID = entity.UserID
                    };
                    result.SuccessData = entity.CostDate.ToString("yyyy-MM-dd");
                    result.SuccessHtml = RenderPartialViewToString("costitem", CostItem);
                    result.Message = "添加费用记录成功！";
                }
                catch (Exception ex)
                {
                    result.AddServiceError(Utilities.GetInnerMostException(ex));
                    LogHelper.WriteLog("用户:" + CookieHelper.UID + "添加费用记录失败!", ex);
                }
            }
            return Json(result);
        }

        private GroupSelect GetGroupSelect()
        {
            var select = new GroupSelect();
            var source = CostTypeService.GetAll()
                .Where(x => x.PID.Equals(null) && !x.Deleted).ToList();

            foreach (var cate in source)
            {
                if (CostTypeService.GetAll()
                    .Where(x => x.PID == cate.ID).Any())
                {
                    var group = new GroupSelectOptgroup();
                    group.Label = cate.CateName;
                    var childs = CostTypeService.GetAll()
                    .Where(x => x.PID == cate.ID).ToList();
                    foreach (var child in childs)
                    {
                        var item = new GroupSelectItem();
                        item.Text = child.CateName;
                        item.Value = child.ID.ToString();
                        group.Items.Add(item);
                    }
                    select.Groups.Add(group);
                }
                else
                {
                    var item = new GroupSelectItem();
                    item.Text = cate.CateName;
                    item.Value = cate.ID.ToString();
                    select.Items.Add(item);
                }
            }
            return select;
        }
    }
}