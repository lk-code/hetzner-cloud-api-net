namespace Hetzner.Cloud.Demo.Helper;

public static class BooleanHelper
{
    public static string ToString(this bool value, string trueValue, string falseValue)
    {
        return value ? trueValue : falseValue;
    }
}