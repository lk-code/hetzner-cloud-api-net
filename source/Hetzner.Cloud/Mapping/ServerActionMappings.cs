using System.Text.Json;
using Hetzner.Cloud.Helper;
using Hetzner.Cloud.Models;

namespace Hetzner.Cloud.Mapping;

internal static class ServerActionMappings
{
    internal static ServerAction ToServerAction(this JsonElement json)
    {
        ServerAction data = new(json.GetProperty("id").GetInt64(),
            json.GetProperty("command").GetString()!,
            json.GetProperty("progress").GetInt32(),
            json.GetProperty("resources").ToServerActionResources(),
            json.GetProperty("started").GetDateTime(),
            json.GetProperty("command").ToEnum<ServerActionStatus>())
        {
            Error = json.GetProperty("error").ToError(),
            Finished = json.GetProperty("finished").GetDateTime(),
        };

        return data;
    }
    internal static ServerActionResource[] ToServerActionResources(this JsonElement json)
    {
        ServerActionResource[] data = json.EnumerateArray()
            .Select(x => x.ToServerActionResource())
            .ToArray();

        return data;
    }
    
    internal static ServerActionResource ToServerActionResource(this JsonElement json)
    {
        ServerActionResource data = new(json.GetProperty("id").GetInt64(),
            json.GetProperty("type").GetString()!);

        return data;
    }
}