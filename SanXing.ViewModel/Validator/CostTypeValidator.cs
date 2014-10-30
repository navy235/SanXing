﻿// <autogenerated>
//   This file was generated by T4 code generator Main.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace SanXing.ViewModels.Validator
{
    public class CostTypeValidator : AbstractValidator<CostTypeModel>
    {
        public CostTypeValidator()
        {
            RuleFor(x => x.CateName)
                  .NotEmpty().WithMessage("请输入消费类型名称");

            RuleFor(x => x.Code).NotEmpty().WithMessage("请输入消费类型代码");

        }
    }
}