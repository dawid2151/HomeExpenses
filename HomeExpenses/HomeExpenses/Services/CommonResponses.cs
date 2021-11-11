using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Services
{
    public static class CommonResponses
    {
        public static IActionResult ValidationFailed(ControllerBase controller)
        { 
            return controller.BadRequest("Could not validate.");      
        }
        public static IActionResult DoesNotExist(ControllerBase controller)
        {
            return controller.BadRequest("Does not exist.");
        }
        public static IActionResult AlreadyExists(ControllerBase controller)
        {
            return controller.BadRequest("Already exists.");
        }
    }
}
