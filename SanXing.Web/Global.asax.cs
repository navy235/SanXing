using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Mt.Core;
using Mt.Core.Infrastructure;
using SanXing.Web.Framework;
using SanXing.Web.Framework.Mvc;
using FluentValidation.Mvc;
using SanXing.Web.Framework.Themes;

namespace SanXing.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            EngineContext.Initialize(false);

            var dependencyResolver = new MtDependencyResolver();

            DependencyResolver.SetResolver(dependencyResolver);

            //remove all view engines
            ViewEngines.Engines.Clear();
            //except the themeable razor view engine we use
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new MtValidatorFactory()));

            LogHelper.SetConfig();
        }

    }
}
