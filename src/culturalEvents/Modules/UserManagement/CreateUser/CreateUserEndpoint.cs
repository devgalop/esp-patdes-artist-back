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
                return Results.Ok(new { Message = "User created successfully." });
            });
        }
    }
}