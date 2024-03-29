﻿using System;
using System.Net;
using Rc.Services.Orders.Core.Exceptions;
using ApplicationException = Rc.Services.Orders.Application.Exceptions.ApplicationException;

namespace Rc.Services.Orders.Infrastructure.Errors
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