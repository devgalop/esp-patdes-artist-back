using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Modules.UserManagement.Common
{
    public enum UserError
    {
        UserAlreadyExists,
        UserNotFound,
        InvalidUserData,
        UnauthorizedAccess,
        UnknownError
    }
    public sealed class Result<TModel>
    {
        public bool IsSuccess { get; }
        public TModel? Value { get; }
        public UserError? Error { get; }

        private Result(bool isSuccess, TModel? value, UserError? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<TModel> Success(TModel value) => new(true, value, null);
        public static Result<TModel> Failure(UserError error) => new(false, default, error);
    }
}