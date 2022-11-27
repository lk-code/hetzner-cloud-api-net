namespace lkcode.hetznercloudapi.Helper;

internal static class DictionaryHelper
{
    internal static string ToQueryString(this Dictionary<string, string>? arguments)
    {
        if (arguments == null)
        {
            return string.Empty;
        }

        return "?" + string.Join("&", arguments.Select(x => x.Key + "=" + x.Value));
    }
}
