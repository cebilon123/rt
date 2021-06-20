namespace Rc.Services.Fraud.Application.Handlers.Commands
{
    public class SignUpCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}