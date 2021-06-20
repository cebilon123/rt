using System;

namespace Rc.Services.Fraud.Infrastructure.Errors.Exceptions
{
    public class InfrastructureException : Exception
    {
        public string SourceTypeName { get; }

        public InfrastructureException(string message, Type sourceType) : base(message)
        {
            SourceTypeName = nameof(sourceType);
        }
    }
}