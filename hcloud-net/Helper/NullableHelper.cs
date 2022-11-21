using lkcode.hetznercloudapi.Exceptions;

namespace lkcode.hetznercloudapi.Helper;

public static class NullableHelper
{
    public static int EnsureWithException(this int? value, string errorMessage)
    {
        if (value == null)
        {
            throw new InvalidArgumentException(errorMessage);
        }

        return (int)value;
    }

    public static long EnsureWithException(this long? value, string errorMessage)
    {
        if (value == null)
        {
            throw new InvalidArgumentException(errorMessage);
        }

        return (long)value;
    }

    public static DateTime EnsureWithException(this DateTime? value, string errorMessage)
    {
        if (value == null)
        {
            throw new InvalidArgumentException(errorMessage);
        }

        return (DateTime)value;
    }

    public static string Ensure(this string? value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        return value;
    }
}
