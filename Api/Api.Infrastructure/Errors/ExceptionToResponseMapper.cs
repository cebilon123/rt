using System;
using System.Diagnostics;
using System.Net;
using Api.Application.Exceptions.Auth;
using Api.Application.Exceptions.User;
using Api.Core.Exceptions;
using Api.Core.Exceptions.UserSession;
using JWT.Exceptions;
using ApplicationException = Api.Application.Exceptions.ApplicationException;

namespace Api.Infrastructure.Errors
{
    public class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public Error GetErrorBasedOnException(Exception exception) =>
            exception switch
            {
                DomainException ex => HandleDomainException(ex),
                ApplicationException ex => HandleApplicationException(ex),
                SignatureVerificationException ex => new Error("Error", exception.Message, HttpStatusCode.Unauthorized),
                _ => new Error("Error", exception.Message, HttpStatusCode.BadRequest)
            };

        private Error HandleDomainException(DomainException exception)
            => exception switch
            {
                InvalidUserId ex => new Error(ex.Code, ex.Message, HttpStatusCode.InternalServerError),
                _ => new Error(exception.Code, exception.Message,
                    HttpStatusCode.BadRequest)
            };

        private Error HandleApplicationException(ApplicationException exception)
            => exception switch
            {
                UserNotFoundException ex => new Error(ex.Code, ex.Message, HttpStatusCode.NotFound),
                UnauthorizedException ex => new Error(ex.Code, ex.Message, HttpStatusCode.Unauthorized),
                _ => new Error(exception.Code, exception.Message,
                    HttpStatusCode.BadRequest)
            };
    }
}