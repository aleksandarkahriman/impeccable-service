using System;

namespace Utility.Application.ResultContract
{
    public class ResultWithData<T> : Result
    {
        public ResultWithData(T data)
        {
            if (data == null)
            {
                throw new ArgumentException("Value of data cannot be null.");
            }

            Data = data;
            State = ResultState.Ok;
        }

        public ResultWithData(Exception exception) : base(exception)
        {
        }

        public T Data { get; }
    }
}
