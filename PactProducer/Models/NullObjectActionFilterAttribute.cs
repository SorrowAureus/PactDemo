using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace PactProducer.Models {
    /// <summary>
    /// Converts <c>null</c> return values into an HTTP 404 return code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NullObjectActionFilterAttribute : ActionFilterAttribute {
        /// <inheritdoc />
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {
            if ((actionExecutedContext.Response == null) || !actionExecutedContext.Response.IsSuccessStatusCode) return;
            object contentValue;
            actionExecutedContext.Response.TryGetContentValue(out contentValue);
            if (contentValue == null) {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Object not found");
            }
        }
    }
}