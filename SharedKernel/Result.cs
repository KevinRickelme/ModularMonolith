using SharedKernel.Errors;
using System.Diagnostics.CodeAnalysis;

namespace SharedKernel;

public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public Result(bool isSuccess, IDictionary<string, string[]> erros)
    {
        if (isSuccess && erros != null ||
            !isSuccess && erros == null)
        {
            throw new ArgumentException("Invalid error", nameof(erros));
        }
        IsSuccess = isSuccess;
        Errors = erros;
    }


    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public IDictionary<string, string[]>? Errors { get; set; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);

    public static Result Failure(IDictionary<string, string[]> errors) => new(false, errors);
    public static Result<TValue> Failure<TValue>(IDictionary<string, string[]> errors) => new(default, false, errors);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public Result(TValue? value, bool isSuccess, IDictionary<string, string[]> erros)
        : base(isSuccess, erros)
    {
        _value = value;
        if (isSuccess && erros != null ||
            !isSuccess && erros == null)
        {
            throw new ArgumentException("Invalid error", nameof(erros));
        }
        Errors = erros;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static Result<TValue> ValidationFailure(Error error) =>
        new(default, false, error);
}
