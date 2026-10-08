using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.WebAPI.Filter
{
    /// <summary>
    /// Pretvara izuzetke u odgovore oblika { "errors": { "ERROR": ["poruka"] } }
    /// (isti oblik kao automatska validacija modela, pa UI parsira greske na jedan nacin).
    /// </summary>
    public class ErrorFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ErrorFilter> _logger;

        public ErrorFilter(ILogger<ErrorFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            HttpStatusCode status;
            string poruka;

            switch (context.Exception)
            {
                case NotFoundException ex:
                    status = HttpStatusCode.NotFound;
                    poruka = ex.Message;
                    break;
                case ForbiddenException ex:
                    status = HttpStatusCode.Forbidden;
                    poruka = ex.Message;
                    break;
                case UserException ex:
                    status = HttpStatusCode.BadRequest;
                    poruka = ex.Message;
                    break;
                default:
                    _logger.LogError(context.Exception, "Neočekivana greška");
                    status = HttpStatusCode.InternalServerError;
                    poruka = "Greška na serveru. Pokušajte ponovo.";
                    break;
            }

            context.Result = new JsonResult(new
            {
                errors = new Dictionary<string, string[]> { { "ERROR", new[] { poruka } } }
            })
            {
                StatusCode = (int)status
            };
            context.ExceptionHandled = true;
        }
    }
}
