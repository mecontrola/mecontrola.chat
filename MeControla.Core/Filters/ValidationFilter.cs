using MeControla.Core.Data.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeControla.Core.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorInModelState = GetListErrorsInModelState(context);
                var errorResponse = MountErrorResponse(errorInModelState);

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }

        private static KeyValuePair<string, IEnumerable<string>>[] GetListErrorsInModelState(ActionExecutingContext context)
            => context.ModelState.Where(x => x.Value.Errors.Count > 0)
                                 .ToDictionary(k => k.Key, v => v.Value.Errors.Select(x => x.ErrorMessage))
                                 .ToArray();

        private static IList<ErrorModel> MountErrorResponse(KeyValuePair<string, IEnumerable<string>>[] errors)
            => errors.Select(error => error.Value.Select(message => new ErrorModel
            {
                FieldName = error.Key,
                Message = message
            })).SelectMany(x => x).ToList();
    }
}