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
    public class TimeCostTypeController : BaseController
    {
        //
        // GET: /Area/
        private ITimeCostTypeService TimeCostTypeService;
        public TimeCostTypeController(
            ITimeCostTypeService TimeCostTypeService
          )
        {
            this.TimeCostTypeService = TimeCostTypeService;
        }

        public ActionResult Index(int page = 1)
        {
            const int pageSize = 20;

            var query = TimeCostTypeService.GetAll().Include(x => x.PCate)
                .Where(x => x.Deleted.Equals(false));

            var data = query.OrderBy(x => x.Code).Paging<TimeCostType>(page - 1, pageSize);


            var model = data.Select(x =>
                {
                    var m = new TimeCostTypeListItemModel();
                    PrepareListModel(m, x);
                    return m;

                }).ToList();

            ViewBag.PageInfo = new PagingInfo()
                {
                    TotalItems = data.TotalCount,
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                };
            return View(model);
        }

        private void PrepareListModel(TimeCostTypeListItemModel model, TimeCostType entity)
        {
            model.ID = entity.ID;
            model.CateName = entity.CateName;
            model.Deleted = entity.Deleted;
            model.PID = entity.PID;
            model.Code = entity.Code;
            model.Level = entity.Level;
            model.Description = entity.Description;
            model.PCateName = (entity.PCate != null) ? entity.PCate.CateName : "";
        }

        public ActionResult Create()
        {
            var model = new TimeCostTypeModel();
            ViewBag.Data_PID = Utilities.GetSelectListData(
                TimeCostTypeService.GetAll()
                .Where(x => x.PID.Equals(null) && !x.Deleted)
                .OrderBy(x => x.Code)
                .ToList(), x => x.ID, x => x.CateName, true);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeCostTypeModel model)
        {
            ServiceResult result = new ServiceResult();
            TempData["Service_Result"] = result;
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    TimeCostTypeService.Insert(entity);
                    result.Message = "添加时间费用类别成功！";
                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    result.Message = Utilities.GetInnerMostException(ex);
                    result.AddServiceError(result.Message);
                }
            }
            else
            {
                result.Message = "请检查表单是否填写完整！";
                result.AddServiceError("请检查表单是否填写完整！");
            }
            return View(model);
        }

        public ActionResult Edit(int ID)
        {
            var entity = TimeCostTypeService.Single(ID);

            var model = entity.ToModel();

            ViewBag.Data_PID = Utilities.GetSelectListData(
             TimeCostTypeService.GetAll()
             .Where(x => x.PID.Equals(null) && !x.Deleted)
             .OrderBy(x => x.Code)
             .ToList(), x => x.ID, x => x.CateName, model.PID, true);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TimeCostTypeModel model)
        {
            ServiceResult result = new ServiceResult();
            TempData["Service_Result"] = result;
            var entity = TimeCostTypeService.Single(model.ID);
            if (ModelState.IsValid)
            {
                try
                {
                    entity = model.ToEntity(entity);
                    TimeCostTypeService.Update(entity);
                    result.Message = "编辑时间费用类别成功！";
                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    result.Message = Utilities.GetInnerMostException(ex);
                    result.AddServiceError(result.Message);

                }
            }
            else
            {
                result.Message = "请检查表单是否填写完整！";
                result.AddServiceError("请检查表单是否填写完整！");

            }

            return View(model);
        }

        public ActionResult Delete(int ID)
        {
            ServiceResult result = new ServiceResult();
            TempData["Service_Result"] = result;
            var entity = TimeCostTypeService.Single(ID);
            try
            {
                TimeCostTypeService.Delete(entity);
                result.Message = "删除时间费用类别成功！";
            }
            catch (Exception ex)
            {
                result.Message = Utilities.GetInnerMostException(ex);
                result.AddServiceError(result.Message);
            }
            return RedirectToAction("index");
        }
    }
}

