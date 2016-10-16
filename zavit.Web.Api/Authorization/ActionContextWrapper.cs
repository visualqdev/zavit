using System.Linq;
using System.Web.Http.Controllers;

namespace zavit.Web.Api.Authorization
{
    public class ActionContextWrapper : IActionContext
    {
        readonly HttpActionContext _actionContext;

        public ActionContextWrapper(HttpActionContext actionContext)
        {
            _actionContext = actionContext;
        }

        public object GetActionParameter(string parameterName)
        {
            return _actionContext.ActionArguments.ContainsKey(parameterName) ? _actionContext.ActionArguments[parameterName] : null;
        }

        public T GetActionParameter<T>()
        {
            return _actionContext.ActionArguments.Values.OfType<T>().FirstOrDefault();
        }
    }
}