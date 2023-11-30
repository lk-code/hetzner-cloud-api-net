using Hetzner.Cloud.Exceptions;

namespace Hetzner.Cloud.Helper;

internal static class NullableHelper
{
    internal static string Ensure(this string? value, string? errorMessage)
    {
        if (value is null)
        {
            if (errorMessage is null)
            {
                return string.Empty;
            }
            else
            {
                throw new InvalidArgumentException(errorMessage);
            }
        }

        return value;
    }
    
    internal static int Ensure(this int? value, string? errorMessage)
    {
        if (value is null)
        {
            if (errorMessage is null)
            {
                return int.MinValue;
            }
            else
            {
                throw new InvalidArgumentException(errorMessage);
            }
        }

        return (int)value;
    }

    internal static long Ensure(this long? value, string? errorMessage)
    {
        if (value is null)
        {
            if (errorMessage is null)
            {
                return long.MinValue;
            }
            else
            {
                throw new InvalidArgumentException(errorMessage);
            }
        }

        return (long)value;
    }

    internal static double Ensure(this double? value, string? errorMessage)
    {
        if (value is null)
        {
            if (errorMessage is null)
            {
                return double.MinValue;
            }
            else
            {
                throw new InvalidArgumentException(errorMessage);
            }
        }

        return (double)value;
    }

    internal static DateTime Ensure(this DateTime? value, string? errorMessage)
    {
        if (value is null)
        {
            if (errorMessage is null)
            {
                return DateTime.MinValue;
            }
            else
            {
                throw new InvalidArgumentException(errorMessage);
            }
        }

        return (DateTime)value;
    }

    internal static Dictionary<string, string> Ensure(this Dictionary<string, string>? value, string? errorMessage = null)
    {
        if (value is null)
        {
            if (errorMessage is null)
            {
                return new();
            }
            else
            {
                throw new InvalidArgumentException(errorMessage);
            }
        }

        return value;
    }

    internal static IEnumerable<T> Ensure<T>(this IEnumerable<T>? value, string? errorMessage)
    {
        if (value is null)
        {
            if (errorMessage is null)
            {
                return Enumerable.Empty<T>();
            }
            else
            {
                throw new InvalidArgumentException(errorMessage);
            }
        }

        return value;
    }
}
