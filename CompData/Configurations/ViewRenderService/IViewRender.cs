using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMData.Configurations.ViewRenderService
{
    public interface IViewRender
    {
        public Task<string> RenderToStringAsync(string viewName);
        public Task<string> RenderToStringAsync<TModel>(string viewName, TModel model);
        public string RenderToString<TModel>(string viewPath, TModel model);
        public string RenderToString(string viewPath);
    }
}
