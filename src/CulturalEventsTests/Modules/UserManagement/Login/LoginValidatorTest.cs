using culturalEvents.Modules.UserManagement.Login;
using Xunit;

namespace CulturalEventsTests.Modules.UserManagement.Login;

public class LoginValidatorTest
{
    private readonly LoginValidator _validator;

    public LoginValidatorTest()
    {
        _validator = new LoginValidator();
    }

    private static LoginRequest CreateValidRequest()
    {
        return new LoginRequest(
            "test@example.com",
            "Password123!*"
        );
    }

    [Fact]
    public void Validate_WhenRequestIsValid_ShouldReturnValidResult()
    {
        LoginRequest request = CreateValidRequest();
        var validationResult = _validator.Validate(request);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void Validate_WhenEmailIsEmpty_ShouldReturnInvalidResult()
    {
        LoginRequest request = CreateValidRequest() with { Email = string.Empty };
        var validationResult = _validator.Validate(request);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void Validate_WhenEmailIsInvalid_ShouldReturnInvalidResult()
    {
        LoginRequest request = CreateValidRequest() with { Email = "invalid-email" };
        var validationResult = _validator.Validate(request);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
        public void Validate_WhenPasswordIsEmpty_ShouldReturnInvalidResult()
        {
            LoginRequest request = CreateValidRequest() with { Password = string.Empty };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password");
        }

        [Fact]
        public void Validate_WhenPasswordIsTooShort_ShouldReturnInvalidResult()
        {
            LoginRequest request = CreateValidRequest() with { Password = "P1!" };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password");
        }

        [Fact]
        public void Validate_WhenPasswordExceedsMaxLength_ShouldReturnInvalidResult()
        {
            LoginRequest request = CreateValidRequest() with { Password = new string('P', 101) };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password");
        }

        [Theory]
        [InlineData("password123!")] // No uppercase letter
        [InlineData("PASSWORD123!")] // No lowercase letter
        [InlineData("Password!")]    // No number
        [InlineData("Password123")]  // No symbol
        public void Validate_WhenPasswordDoesNotMeetComplexityRequirements_ShouldReturnInvalidResult(string password)
        {
            LoginRequest request = CreateValidRequest() with { Password = password };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password");
        }
}
