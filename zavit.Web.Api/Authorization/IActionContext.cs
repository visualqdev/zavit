namespace zavit.Web.Api.Authorization
{
    public interface IActionContext
    {
        object GetActionParameter(string parameterName);
        T GetActionParameter<T>();
    }
}