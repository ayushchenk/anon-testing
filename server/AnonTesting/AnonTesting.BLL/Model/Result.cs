using System.Text.Json.Serialization;

namespace AnonTesting.BLL.Model
{
    public class Result
    {
        [JsonIgnore]
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        protected Result(bool success, string error)
        {
            IsSuccess = success;
            Error = error;
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }

        public static Result<T> Failure<T>(string error)
        {
            return new Result<T>(default!, false, error);
        }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }
    }

    public class Result<T> : Result
    {
        [JsonIgnore]
        public T Value { get; private set; }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}
