using System;

namespace Utility.Application.ResultContract
{
    public enum ResultState
    {
        Ok,
        Error,
        Cancelled
    }

    public class Result
    {
        public Result(Exception exception = null)
        {
            switch (exception)
            {
                case OperationCanceledException _:
                    State = ResultState.Cancelled;
                    break;
                case Exception _:
                    State = ResultState.Error;
                    break;
            }

            ErrorReason = exception;
        }

        private Result()
        {
            State = ResultState.Ok;
        }

        public ResultState State { get; protected set; }

        public Exception ErrorReason { get; }

        public bool Success => State == ResultState.Ok;

        public bool Failure => State != ResultState.Ok;

        public static Result Ok() => new Result();
    }
}
