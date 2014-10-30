using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanXing.ViewModels.Common
{
    public class GroupSelectListItem
    {
        public string group { get; set; }

        public bool selected { get; set; }

        public string text { get; set; }

        public string value { get; set; }
    }
}