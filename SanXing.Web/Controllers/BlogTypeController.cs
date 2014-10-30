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
    public class BlogTypeController : BaseController
    {
        //
        // GET: /Area/
        private IBlogTypeService BlogTypeService;
        public BlogTypeController(
            IBlogTypeService BlogTypeService
          )
        {
            this.BlogTypeService = BlogTypeService;
        }

        public ActionResult Index(int page = 1)
        {
            const int pageSize = 20;

            var query = BlogTypeService.GetAll().Include(x => x.PCate)
                .Where(x => x.Deleted.Equals(false));

            var data = query.OrderBy(x => x.Code).Paging<BlogType>(page - 1, pageSize);


            var model = data.Select(x =>
                {
                    var m = new BlogTypeListItemModel();
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

        private void PrepareListModel(BlogTypeListItemModel model, BlogType entity)
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
            var model = new BlogTypeModel();
            ViewBag.Data_PID = Utilities.GetSelectListData(
                BlogTypeService.GetAll()
                .Where(x => x.PID.Equals(null) && !x.Deleted)
                .OrderBy(x => x.Code)
                .ToList(), x => x.ID, x => x.CateName, true);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogTypeModel model)
        {
            ServiceResult result = new ServiceResult();
            TempData["Service_Result"] = result;
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    entity.UserID = CookieHelper.UID;
                    BlogTypeService.Insert(entity);
                    result.Message = "添加文章类别成功！";
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
            var entity = BlogTypeService.Single(ID);

            var model = entity.ToModel();

            ViewBag.Data_PID = Utilities.GetSelectListData(
             BlogTypeService.GetAll()
             .Where(x => x.PID.Equals(null) && !x.Deleted)
             .OrderBy(x => x.Code)
             .ToList(), x => x.ID, x => x.CateName, model.PID, true);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogTypeModel model)
        {
            ServiceResult result = new ServiceResult();
            TempData["Service_Result"] = result;
            var entity = BlogTypeService.Single(model.ID);
            if (ModelState.IsValid)
            {
                try
                {
                    entity = model.ToEntity(entity);
                    BlogTypeService.Update(entity);
                    result.Message = "编辑文章类别成功！";
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
            var entity = BlogTypeService.Single(ID);
            try
            {
                BlogTypeService.Delete(entity);
                result.Message = "删除文章类别成功！";
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

