namespace Hetzner.Cloud.Demo.Helper;

public static class Int64Helper
{
    public static string AsMegaBytes(this long value)
    {
        return $"{value / 1024 / 1024} MB";
    }
    
    public static string AsGigaBytes(this long value)
    {
        return $"{value / 1024 / 1024 / 1024} GB";
    }
}