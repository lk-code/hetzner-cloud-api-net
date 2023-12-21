using System.Text;

namespace Hetzner.Cloud.UriBuilder;

public class UriBuilder(string address)
{
    private readonly Dictionary<string, string> _arguments = new();
    
    public UriBuilder AddUriParameter(string key, string value)
    {
        _arguments.Add(key, value);

        return this;
    }

    public string ToUri()
    {
        if (string.IsNullOrEmpty(address))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(address));
        }
        
        StringBuilder builder = new();

        if (!address.StartsWith("/"))
        {
            address = $"/{address}";
        }
        
        builder.Append(address);
        
        if (_arguments.Any())
        {
            builder.Append("?");
            
            foreach (var argument in this._arguments)
            {
                builder.Append($"{argument.Key}={argument.Value}&");
            }
            
            builder.Remove(builder.Length - 1, 1);
        }
        
        string uri = builder.ToString();

        return uri;
    }
}