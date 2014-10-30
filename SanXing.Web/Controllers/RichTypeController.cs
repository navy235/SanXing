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
    public class RichTypeController : BaseController
    {
        //
        // GET: /Area/
        private IRichTypeService RichTypeService;
        public RichTypeController(
            IRichTypeService RichTypeService
          )
        {
            this.RichTypeService = RichTypeService;
        }

        public ActionResult Index(int page = 1)
        {
            const int pageSize = 20;

            var query = RichTypeService.GetAll().Include(x => x.PCate)
                .Where(x => x.Deleted.Equals(false));

            var data = query.OrderBy(x => x.Code).Paging<RichType>(page - 1, pageSize);


            var model = data.Select(x =>
                {
                    var m = new RichTypeListItemModel();
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

        private void PrepareListModel(RichTypeListItemModel model, RichType entity)
        {
            model.ID = entity.ID;
            model.CateName = entity.CateName;
            model.Deleted = entity.Deleted;
            model.PID = entity.PID;
            model.Code = entity.Code;
            model.Level = entity.Level;
            model.PCateName = (entity.PCate != null) ? entity.PCate.CateName : "";
        }

        public ActionResult Create()
        {
            var model = new RichTypeModel();
            ViewBag.Data_PID = Utilities.GetSelectListData(
                RichTypeService.GetAll()
                .Where(x => x.PID.Equals(null) && !x.Deleted)
                .OrderBy(x => x.Code)
                .ToList(), x => x.ID, x => x.CateName, true);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RichTypeModel model)
        {
            ServiceResult result = new ServiceResult();
            TempData["Service_Result"] = result;
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    RichTypeService.Insert(entity);
                    result.Message = "添加富贵类别成功！";
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
            var entity = RichTypeService.Single(ID);

            var model = entity.ToModel();

            ViewBag.Data_PID = Utilities.GetSelectListData(
             RichTypeService.GetAll()
             .Where(x => x.PID.Equals(null) && !x.Deleted)
             .OrderBy(x => x.Code)
             .ToList(), x => x.ID, x => x.CateName, model.PID, true);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RichTypeModel model)
        {
            ServiceResult result = new ServiceResult();
            TempData["Service_Result"] = result;
            var entity = RichTypeService.Single(model.ID);
            if (ModelState.IsValid)
            {
                try
                {
                    entity = model.ToEntity(entity);
                    RichTypeService.Update(entity);
                    result.Message = "编辑富贵类别成功！";
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
            var entity = RichTypeService.Single(ID);
            try
            {
                RichTypeService.Delete(entity);
                result.Message = "删除富贵类别成功！";
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

