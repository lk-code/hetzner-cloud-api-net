using Hetzner.Cloud.Interfaces;
using Hetzner.Cloud.Sorting;

namespace Hetzner.Cloud.UriBuilder;

public static class UriBuilderExtensions
{
    public static UriBuilder AsUriBuilder(this string address)
    {
        return new UriBuilder(address);
    }

    public static UriBuilder AddPagination(this UriBuilder uriBuilder, long currentPage, long itemsPerPage)
    {
        uriBuilder.AddUriParameter("page", currentPage.ToString());
        uriBuilder.AddUriParameter("per_page", itemsPerPage.ToString());

        return uriBuilder;
    }

    public static UriBuilder AddSorting<T>(this UriBuilder uriBuilder, Sorting<T>? sorting)
    {
        if (sorting is not null)
        {
            uriBuilder.AddUriParameter("sort", sorting.AsUriParameter());
        }

        return uriBuilder;
    }

    public static UriBuilder AddFilter(this UriBuilder uriBuilder, IEnumerable<IFilter>? filters)
    {
        if (filters is null)
        {
            return uriBuilder;
        }

        IEnumerable<IFilter> enumerable = filters as IFilter[] ?? filters.ToArray();
        foreach (var filter in enumerable)
        {
            uriBuilder.AddUriParameter(filter.GetFilterField(), filter.GetValue());
        }

        return uriBuilder;
    }
}