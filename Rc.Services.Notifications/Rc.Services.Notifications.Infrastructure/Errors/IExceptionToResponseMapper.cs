using System;

namespace Rc.Services.Notifications.Infrastructure.Errors
{
    public interface IExceptionToResponseMapper
    {
        Error GetErrorBasedOnException(Exception exception);
    }
}