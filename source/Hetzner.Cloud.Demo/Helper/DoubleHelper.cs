namespace Hetzner.Cloud.Demo.Helper;

public static class DoubleHelper
{
    public static double Ensure(this double? val, double fallback = 0)
    {
        return val ?? fallback;
    }
}