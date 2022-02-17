using Cinerva.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Cinerva.Web.Controllers
{
    [AddUserAgentActionFilter]
    public class BaseController : Controller
    {
       
    }
}
