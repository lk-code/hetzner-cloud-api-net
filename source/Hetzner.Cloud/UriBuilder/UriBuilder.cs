using System.Text;

namespace Hetzner.Cloud.UriBuilder;

public class UriBuilder(string address)
{
    private string _address = address;
    private Dictionary<string, string> _arguments = new();
    
    public UriBuilder AddUriParameter(string key, string value)
    {
        this._arguments.Add(key, value);

        return this;
    }

    public string ToUri()
    {
        StringBuilder builder = new(this._address);
        
        if (this._arguments.Any())
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