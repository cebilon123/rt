using Microsoft.AspNetCore.Mvc;
using Rc.Services.Fraud.Application.Handlers;

namespace Rc.Services.Fraud.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        // private readonly ICommandDispatcher _commandDispatcher;
        // private readonly IRequestInfoProvider _requestInfoProvider;
        //
        // public AuthController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IRequestInfoProvider requestInfoProvider)
        // {
        //     _queryDispatcher = queryDispatcher;
        //     _commandDispatcher = commandDispatcher;
        //     _requestInfoProvider = requestInfoProvider;
        // }
        //
        // [HttpPost]
        // [Route("Sign-up")]
        // [SwaggerDescription("Create account for user using email and password.")]
        // public async Task<ActionResult> SignUp([FromBody] SignUpCommand command)
        // {
        //     await _commandDispatcher.SendAsync(command);
        //     return Created("Auth/sign-up", command.Email);
        // }
        //
        // [HttpPost]
        // [Route("Sign-in")]
        // [SwaggerDescription("Sign-in to application.")]
        // public async Task<ActionResult> SignIn([FromBody] SignInQuery query)
        // {
        //     var tokens = await _queryDispatcher.QueryAsync(query);
        //     HttpContext.Response.Cookies.Append(Const.AccessTokenName, tokens.AccessToken, new CookieOptions
        //     {
        //         HttpOnly = true
        //     });
        //     HttpContext.Response.Cookies.Append(Const.RefreshTokenName, tokens.RefreshToken, new CookieOptions
        //     {
        //         HttpOnly = true
        //     });
        //     
        //     return Ok();
        // }
        //
        // [HttpGet]
        // [Route("Refresh")]
        // [SwaggerDescription("Refresh token pair based on refresh token being send.")]
        // public async Task<ActionResult> Refresh()
        // {
        //     var refreshToken = HttpContext.Request.Cookies[Const.RefreshTokenName];
        //     var tokens = await _queryDispatcher.QueryAsync(new RefreshTokensQuery(refreshToken));
        //     HttpContext.Response.Cookies.Append(Const.AccessTokenName, tokens.AccessToken, new CookieOptions
        //     {
        //         HttpOnly = true
        //     });
        //     HttpContext.Response.Cookies.Append(Const.RefreshTokenName, tokens.RefreshToken, new CookieOptions
        //     {
        //         HttpOnly = true
        //     });
        //     
        //     return Ok();
        // }
    }
}