﻿// <autogenerated>
//   This file was generated by T4 code generator Main.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

using SanXing.ViewModels.Validator;

namespace SanXing.ViewModels
{
    [Validator(typeof(ContactTypeValidator))]
    public class ContactTypeModel : BaseMtEntityModel
    {
        [HiddenInput(DisplayValue = false)]
        public override int ID { get; set; }

        [Display(Name = "类别名称")]
        public string CateName { get; set; }

        [Display(Name = "父级类别")]
        [UIHint("DropDownList")]
        public int? PID { get; set; }

        [Display(Name = "类别代码")]
        [UIHint("Integer")]
        public int Code { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool Deleted { get; set; }



    }

    public class ContactTypeSearchModel : BaseMtSearchModel
    {

    }

    public class ContactTypeListItemModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        public string CateName { get; set; }

        public int? PID { get; set; }

        public string PCateName { get; set; }

        public string Code { get; set; }

        public int Level { get; set; }

        public bool Deleted { get; set; }
    }

}