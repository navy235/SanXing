﻿// <autogenerated>
//   This file was generated by T4 code generator Main.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Mt.Core;
using SanXing.Data.Models;
using System.Linq;

namespace SanXing.Data.Service
{
    public interface IUserService
    {
        User Single(int ID);

        void Delete(User entity);

        IPagedList<User> GetAll(int pageIndex, int pageSize, bool showHidden = false);

        void Insert(User entity);

        void Update(User entity);

        IQueryable<User> GetAll();

    }
}


