using LetsPaint.ModelAccess.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsPaint.Filters
{
    public class LetsPaintAuth : IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public LetsPaintAuth(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
           var model= _session.GetObjectFromJson<UserModel>("UserAuth");
            if(model==null || model.UserId<0)
            {
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}
