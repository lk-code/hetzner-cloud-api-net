using Hetzner.Cloud.Exceptions;

namespace Hetzner.Cloud.Pagination;

public static class PaginationExtensions
{
    public static void IsValidPageRequest(this long page)
    {
        if (page <= 0)
        {
            throw new InvalidArgumentException($"invalid page number ({page}). must be greather than 0.");
        }
    }
}