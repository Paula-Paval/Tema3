using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Cinerva.Web.ActionFilters
{
    public class AddUserAgentActionFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);

            var controller = context.Controller as Controller;
            if (controller == null)  return;

            controller.ViewBag.Message = context.HttpContext.Request.Headers.UserAgent.ToString();
        }

    }
}
