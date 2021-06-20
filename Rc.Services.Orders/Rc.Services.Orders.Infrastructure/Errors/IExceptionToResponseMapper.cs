using System;

namespace Rc.Services.Orders.Infrastructure.Errors
{
    public interface IExceptionToResponseMapper
    {
        Error GetErrorBasedOnException(Exception exception);
    }
}