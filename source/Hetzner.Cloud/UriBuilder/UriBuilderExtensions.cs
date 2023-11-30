using Hetzner.Cloud.Exceptions;
using lkcode.hetznercloudapi.Interfaces;
using lkcode.hetznercloudapi.ParameterObjects.Sort;

namespace Hetzner.Cloud.UriBuilder;

public static class UriBuilderExtensions
{
    public static UriBuilder AsUriBuilder(this string address)
    {
        return new UriBuilder(address);
    }
    
    public static UriBuilder AddPagination(this UriBuilder uriBuilder, int currentPage, int itemsPerPage)
    {
        if (currentPage <= 0)
        {
            throw new InvalidArgumentException($"invalid page number ({currentPage}). must be greather than 0.");
        }
        
        if (itemsPerPage <= 0)
        {
            throw new InvalidArgumentException($"invalid page number ({itemsPerPage}). must be greather than 0.");
        }
        
        uriBuilder.AddUriParameter("page", currentPage.ToString());
        uriBuilder.AddUriParameter("per_page", itemsPerPage.ToString());
        
        return uriBuilder;
    }

    public static UriBuilder AddSorting(this UriBuilder uriBuilder, Sorting<ServerSortField>? sorting)
    {
        if (sorting is not null)
        {
            uriBuilder.AddUriParameter(sorting.Field.ToString(), sorting.Direction.ToString());
        }
        
        return uriBuilder;
    }

    public static UriBuilder AddFilter(this UriBuilder uriBuilder, IEnumerable<IFilter>? filters)
    {
        if (filters is not null && filters.Any())
        {
            foreach (var filter in filters)
            {
                uriBuilder.AddFilter(filter);
            }
        }
        
        return uriBuilder;
    }

    public static UriBuilder AddFilter(this UriBuilder uriBuilder, IFilter? filter)
    {
        uriBuilder.AddUriParameter(filter.GetFilterField(), filter.GetValue());
        
        return uriBuilder;
    }
}