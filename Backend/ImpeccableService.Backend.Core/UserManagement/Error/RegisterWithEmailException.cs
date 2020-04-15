using System;

namespace ImpeccableService.Backend.Core.UserManagement.Error
{
    public class RegisterWithEmailException : Exception
    {
        public enum ErrorCause
        {
            EmailExists
        }

        public RegisterWithEmailException(ErrorCause cause)
        {
            Cause = cause;
        }

        public ErrorCause Cause { get; }
    }
}
