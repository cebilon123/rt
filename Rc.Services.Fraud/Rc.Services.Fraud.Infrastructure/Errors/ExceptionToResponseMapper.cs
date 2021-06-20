using System;
using System.Net;
using Rc.Services.Fraud.Application.Exceptions.Auth;
using Rc.Services.Fraud.Application.Exceptions.User;
using Rc.Services.Fraud.Core.Exceptions;
using Rc.Services.Fraud.Core.Exceptions.UserSession;
using ApplicationException = Rc.Services.Fraud.Application.Exceptions.ApplicationException;

namespace Rc.Services.Fraud.Infrastructure.Errors
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
                InvalidUserId ex => new Error(ex.Code, ex.Message, HttpStatusCode.InternalServerError),
                _ => new Error(exception.Code, exception.Message,
                    HttpStatusCode.BadRequest)
            };
        }

        private Error HandleApplicationException(ApplicationException exception)
        {
            return exception switch
            {
                UserNotFoundException ex => new Error(ex.Code, ex.Message, HttpStatusCode.NotFound),
                UnauthorizedException ex => new Error(ex.Code, ex.Message, HttpStatusCode.Unauthorized),
                _ => new Error(exception.Code, exception.Message,
                    HttpStatusCode.BadRequest)
            };
        }
    }
}