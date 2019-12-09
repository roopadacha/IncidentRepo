using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncidentAPI.ActionFilters
{

    public class ValidateEntityExistsAttribute<T> : IActionFilter where T : BaseEntity
    {
        private IGenericRepository<T> repository;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id;

            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (int)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }
            this.repository = (IGenericRepository<T>)context.HttpContext.RequestServices.GetService(typeof(IGenericRepository<T>));
            if(this.repository == null)
            {
                context.Result = new BadRequestObjectResult("service not found.");
                return;
            }
            var entity = this.repository.GetByIdAsync(id).Result;
            if (entity == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
