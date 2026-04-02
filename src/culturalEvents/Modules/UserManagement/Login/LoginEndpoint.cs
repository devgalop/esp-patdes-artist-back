using culturalEvents.Shared.Abstractions;
using FluentValidation;

namespace culturalEvents.Modules.UserManagement.Login;

public class LoginEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (
            LoginRequest request, 
            IMediator mediator,
            IValidator<LoginRequest> validator
        ) =>
        {
            await validator.ValidateAndThrowAsync(request);
            await mediator.SendAsync(request);
            return Results.Ok(new { Message = "Login successful." });
        })
        .WithName("LoginUser")
        .WithSummary("Login a user to the system")
        .WithDescription(""" 
            Logs in a user to the system with the provided credentials.
            - `Email`: Email address of the user.
            - `Password`: User's password.
        """)
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem();
    }
}
