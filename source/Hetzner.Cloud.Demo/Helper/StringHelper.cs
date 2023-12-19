namespace Hetzner.Cloud.Demo.Helper;

public static class StringHelper
{
    public static string Ensure(this string? val, string fallback = "")
    {
        return val ?? fallback;
    }
}