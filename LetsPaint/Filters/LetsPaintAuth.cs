using LetsPaint.ModelAccess.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsPaint.Filters
{
    public class LetsPaintAuthAttribute :Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            
            var model = context.HttpContext.Session.GetObjectFromJson<LoginOutputModel>("loginData");
            if (model != null && string.IsNullOrEmpty(model.Email))
            {
                context.Result = new RedirectToRouteResult("/viwes/shared/unauthorised.cshtml");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
    }
}
