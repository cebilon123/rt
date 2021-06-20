using System;

namespace Rc.Services.Fraud.Infrastructure.Errors
{
    public interface IExceptionToResponseMapper
    {
        Error GetErrorBasedOnException(Exception exception);
    }
}