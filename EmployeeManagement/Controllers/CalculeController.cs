using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    [AllowAnonymous]
    public class CalculeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Calcule cal)
        {
            int a = cal.Num1;
            int b = cal.Num2;
        
            if (cal.calcule == "option1")
            {
                cal.Res = a+b;
                
            }
            else if (cal.calcule == "option2")
            {
                cal.Res = a - b;
            }
            else
            {
                cal.Res = a * b;
            }
            
            //if(cal.calcule  "Add")
            //{
            //    cal.Res = cal.Num1 + cal.Num2;
            //}
            //else
            //{
            //    cal.Res = cal.Num1 - cal.Num2;
            //}
            

            return View(cal);
        }
    }
}
