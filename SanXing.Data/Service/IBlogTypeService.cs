﻿// <autogenerated>
//   This file was generated by T4 code generator Main.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Mt.Core;
using SanXing.Data.Models;
using System.Linq;

namespace SanXing.Data.Service
{
    public interface IBlogTypeService
    {
        BlogType Single(int ID);

        void Delete(BlogType entity);

        IPagedList<BlogType> GetAll(int pageIndex, int pageSize, bool showHidden = false);

        void Insert(BlogType entity);

        void Update(BlogType entity);

        IQueryable<BlogType> GetAll();
    }
}


