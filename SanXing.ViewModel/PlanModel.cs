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
    [Validator(typeof(PlanValidator))]
    public class PlanModel : BaseMtEntityModel
    {
        [HiddenInput(DisplayValue = false)]
        public override int ID { get; set; }

 

    }

    public class PlanSearchModel : BaseMtSearchModel
    {

    }

    public class PlanListItemModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
    }

}
