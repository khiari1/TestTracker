using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Tsi.Erp.TestTracker.Api.Services
{
    public class HtmlRendrer : IHtmlRendrer
    {
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IRazorViewEngine _compositeViewEngine;
        private readonly IServiceProvider _serviceProvider;

        public HtmlRendrer(ITempDataProvider tempDataProvider, IRazorViewEngine compositeViewEngine, IServiceProvider serviceProvider)
        {
            _tempDataProvider = tempDataProvider;
            _compositeViewEngine = compositeViewEngine;
            _serviceProvider = serviceProvider;
        }
        public async Task<string> RenderAsync(string viewName, object model)
        {

            var actionContext = ActionContext();

            var viewResult = _compositeViewEngine.FindView(actionContext, viewName, false);

            using var writer = new StringWriter();

            var viewContext = await ViewContextAsync(actionContext, viewResult, writer, model);

            return writer.ToString();
        }

        private ActionContext ActionContext()
        {
            var httpContext = new DefaultHttpContext()
            {
                RequestServices = _serviceProvider,
            };

            var routeData = new RouteData();

            var actionDescriptor = new ActionDescriptor();

            var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

            return actionContext;
        }

        private async Task<ViewContext> ViewContextAsync(ActionContext actionContext, ViewEngineResult viewEngine,TextWriter writer,object model)
        {

            var viewData = new ViewDataDictionary(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary())
            {
                Model = model,
            };

            // Create ViewContext
            var viewContext = new ViewContext(actionContext,
                                              viewEngine.View!,
                                              viewData,
                                              new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                                              writer!,
                                              new HtmlHelperOptions());

            await viewEngine.View.RenderAsync(viewContext);

            return viewContext;
        }

    }
}
