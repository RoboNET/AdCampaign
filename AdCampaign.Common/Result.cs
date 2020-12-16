#nullable enable
using System;

namespace AdCampaign.Common
{
    public record Result(Error[]? Errors = null)
    {
        public bool Ok => Errors is not { Length: > 0 };

        public static implicit operator Result(Error error) => new(new[] { error });
    }

    public record Result<T>(T? Data, Error[]? Errors = null) : Result(Errors)
    {
        public T Unwrap() => Ok && Data != null ? Data : throw new InvalidOperationException();

        public static implicit operator Result<T>(Error error) => new(default, new[] { error });

        public static implicit operator Result<T>(T data) => new(data);
    }
}