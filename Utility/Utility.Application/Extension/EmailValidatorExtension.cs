using System;
using System.Net.Mail;
using Utility.Application.ResultContract;

namespace Utility.Application.Extension
{
    public static class EmailValidatorExtension
    {
        public static Result IsValidEmail(this string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                if (mailAddress.Address != email)
                {
                    throw new ArgumentException("Invalid email address");
                }

                return Result.Ok();
            }
            catch (Exception exception)
            {
                return new Result(exception);
            }
        }
    }
}
