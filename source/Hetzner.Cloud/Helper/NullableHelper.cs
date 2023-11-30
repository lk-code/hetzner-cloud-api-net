using Hetzner.Cloud.Exceptions;

namespace Hetzner.Cloud.Helper;

internal static class NullableHelper
{
    internal static int Ensure(this int? value, string? errorMessage)
    {
        if (value == null)
        {
            if (errorMessage == null)
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
        if (value == null)
        {
            if (errorMessage == null)
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
        if (value == null)
        {
            if (errorMessage == null)
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
        if (value == null)
        {
            if (errorMessage == null)
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

    internal static string Ensure(this string? value, string? errorMessage)
    {
        if (value == null)
        {
            if (errorMessage == null)
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

    internal static Dictionary<string, string> Ensure(this Dictionary<string, string>? value, string? errorMessage = null)
    {
        if (value == null)
        {
            if (errorMessage == null)
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
        if (value == null)
        {
            if (errorMessage == null)
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
