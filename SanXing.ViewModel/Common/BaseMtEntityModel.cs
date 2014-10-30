using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace SanXing.ViewModels
{
    /// <summary>
    /// Base MtCommerce model
    /// </summary>
    public partial class BaseMtModel
    {
        public BaseMtModel()
        {
            //this.CustomProperties = new Dictionary<string, object>();
        }
        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        //[Editable(false)]
        //public Dictionary<string, object> CustomProperties { get; set; }
    }

    /// <summary>
    /// Base MtCommerce entity model
    /// </summary>
    public partial class BaseMtEntityModel : BaseMtModel
    {
        public virtual int ID { get; set; }
    }
}
