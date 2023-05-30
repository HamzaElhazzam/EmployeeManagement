using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller

    {
        //ghlet  f production Employee/afzaz/afa948/454
        [Route("Error/{statusCode}")]
        public IActionResult NotFound(int statusCode)
        {
            
            StatusResult model = new StatusResult();
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                {
                        model.Message = "sorry, the ressource you requested count not be found !";
                        model.Path = statusCodeResult.OriginalPath;
                        model.QS = statusCodeResult.OriginalQueryString;
                }
                break;
            }
            return View(model);
        }
        //matalan ghletna f mode development f chi connection string wla shi haja
        [Route("Error")]
        [AllowAnonymous]
        public ActionResult Error()
        {
            var exStatus = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.Message = exStatus.Error.Message;
            ViewBag.StackTrace = exStatus.Error.StackTrace;
            ViewBag.Path = exStatus.Path;
            return View();
        }
    }
}
