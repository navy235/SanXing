// 源文件头信息：
// <copyright file="MonthTableViewModel.cs">
// Copyright(c)2011-2014 Maitonn.All rights reserved.
// CLR版本： 4.0.30319.18408
// 开发组织：Navy.shen
// 公司网站：http://www.dotaeye.com
// 所属工程：SanXing.ViewModel
// 最后修改：Navy.shen
// 最后修改：2014/10/30 14:10:07
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanXing.ViewModels
{
    public class MonthTableViewModel
    {
        public int UID { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int MaxRows { get; set; }

        public int FirstRowIndex { get; set; }

        public int DayCount { get; set; }
    }
}
