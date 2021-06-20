namespace Rc.Services.Orders.Core.Exceptions.User
{
    public class InvalidPasswordException : DomainException
    {
        public override string Code => "invalid_password";

        public InvalidPasswordException() : base("Invalid password")
        {
        }
    }
}