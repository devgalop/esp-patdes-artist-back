using culturalEvents.Shared.Abstractions;
using FluentValidation;

namespace culturalEvents.Modules.UserManagement.CreateUser
{
    public sealed class CreateUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/users", async(
                CreateUserRequest request, 
                IMediator mediator,
                IValidator<CreateUserRequest> validator
            ) =>
            {
                await validator.ValidateAndThrowAsync(request);
                await mediator.SendAsync(request);
                return Results.Json(new { Message = "User created successfully." }, statusCode: StatusCodes.Status201Created);
            })
            .WithName("AddUser")
            .WithSummary("Add a new user to the system")
            .WithDescription(""" 
                Adds a new user to the system with the provided information.
                - `Name`: Full name of the user.
                - `Email`: Email address of the user.
                - `Password`: User's password.
            """)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem();
        }
    }
}