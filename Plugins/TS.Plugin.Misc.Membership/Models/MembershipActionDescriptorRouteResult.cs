namespace Ts.Plugin.Misc.Membership.Models
{
    public class MembershipActionDescriptorRouteResult
    {
        public MembershipActionDescriptorRouteResult(string controllerName, string actionName)
        {
            ControllerName = controllerName;
            ActionName = actionName;
        }

        public string ControllerName { get; private set; }
        public string ActionName { get; private set; }
    }
}
