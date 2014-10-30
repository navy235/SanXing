using System.Collections.Generic;
using System.Web.Mvc;

namespace SanXing.ViewModels
{
    /// <summary>
    /// Base MtCommerce entity model
    /// </summary>
    public partial class BaseMtSearchModel
    {
        public BaseMtSearchModel()
        {
            page = 1;
            rows = 10;
        }

        [HiddenInput(DisplayValue = false)]
        public int page { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int rows { get; set; }
    }
}
