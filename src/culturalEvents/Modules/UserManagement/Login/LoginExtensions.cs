using culturalEvents.Shared.Abstractions;

namespace culturalEvents.Modules.UserManagement.Login;

public static class LoginExtensions
{
    public static WebApplicationBuilder RegisterLoginFeature(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommandHandler<LoginRequest>, LoginHandler>();
        return builder;
    }
}
