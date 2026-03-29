using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culturalEvents.Modules.UserManagement.CreateUser;
using Xunit;

namespace CulturalEventsTests.Modules.UserManagement.CreateUser
{
    public class CreateUserTests
    {
        private readonly CreateUserValidator _validator;

        public CreateUserTests()
        {
            _validator = new CreateUserValidator();
        }

        private static CreateUserRequest CreateValidRequest()
        {
            return new CreateUserRequest(
                "John",
                "john.doe@example.com",
                "Password123!"
            );
        }

        [Fact]
        public void Validate_WhenIsValidRequest_ShouldReturnValidResult()
        {
            CreateUserRequest request = CreateValidRequest();
            var validationResult = _validator.Validate(request);
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void Validate_WhenNameIsEmpty_ShouldReturnInvalidResult()
        {
            CreateUserRequest request = CreateValidRequest() with { Name = string.Empty };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Validate_WhenNameExceedsMaxLength_ShouldReturnInvalidResult()
        {
            CreateUserRequest request = CreateValidRequest() with { Name = new string('A', 51) };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Validate_WhenEmailIsEmpty_ShouldReturnInvalidResult()
        {
            CreateUserRequest request = CreateValidRequest() with { Email = string.Empty };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public void Validate_WhenEmailIsInvalid_ShouldReturnInvalidResult()
        {
            CreateUserRequest request = CreateValidRequest() with { Email = "invalid-email" };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public void Validate_WhenEmailExceedsMaxLength_ShouldReturnInvalidResult()
        {
            CreateUserRequest request = CreateValidRequest() with { Email = new string('A', 101) + "@example.com" };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Email");
        }

        [Fact]
        public void Validate_WhenPasswordIsEmpty_ShouldReturnInvalidResult()
        {
            CreateUserRequest request = CreateValidRequest() with { Password = string.Empty };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password");
        }

        [Fact]
        public void Validate_WhenPasswordIsTooShort_ShouldReturnInvalidResult()
        {
            CreateUserRequest request = CreateValidRequest() with { Password = "P1!" };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password");
        }

        [Fact]
        public void Validate_WhenPasswordExceedsMaxLength_ShouldReturnInvalidResult()
        {
            CreateUserRequest request = CreateValidRequest() with { Password = new string('P', 101) };
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
            CreateUserRequest request = CreateValidRequest() with { Password = password };
            var validationResult = _validator.Validate(request);
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "Password");
        }
    }
}