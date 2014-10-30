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
    [Validator(typeof(CostValidator))]
    public class CostModel : BaseMtEntityModel
    {
        [HiddenInput(DisplayValue = false)]
        public override int ID { get; set; }

        [Display(Name = "费用时间")]
        [UIHint("DateTime")]
        public DateTime CostDate { get; set; }

        [Display(Name = "费用类别")]
        [UIHint("DropDownList")]
        public int CostTypeID { get; set; }

        [Display(Name = "金额")]
        public int Money { get; set; }

        [Display(Name = "描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }

    public class CostSearchModel : BaseMtSearchModel
    {

    }

    public class CostListItemModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        public int Money { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastTime { get; set; }
        public int CostTypeID { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public bool Deleted { get; set; }

        public DateTime CostDate { get; set; }

        public string CostTypeName { get; set; }
    }

}