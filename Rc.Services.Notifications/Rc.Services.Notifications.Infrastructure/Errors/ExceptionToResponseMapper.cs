using System;
using System.Net;
using Rc.Services.Notifications.Core.Exceptions;
using ApplicationException = Api.Application.Exceptions.ApplicationException;

namespace Rc.Services.Notifications.Infrastructure.Errors
{
    public class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public Error GetErrorBasedOnException(Exception exception)
        {
            return exception switch
            {
                DomainException ex => HandleDomainException(ex),
                ApplicationException ex => HandleApplicationException(ex),
                _ => new Error("Error", exception.Message, HttpStatusCode.BadRequest)
            };
        }

        private Error HandleDomainException(DomainException exception)
        {
            return exception switch
            {
                _ => new Error(exception.Code, exception.Message,
                    HttpStatusCode.BadRequest)
            };
        }

        private Error HandleApplicationException(ApplicationException exception)
        {
            return exception switch
            {
                _ => new Error(exception.Code, exception.Message,
                    HttpStatusCode.BadRequest)
            };
        }
    }
}