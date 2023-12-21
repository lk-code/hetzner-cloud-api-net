using Hetzner.Cloud.Exceptions;

namespace Hetzner.Cloud.Pagination;

public static class PaginationExtensions
{
    public static void MustBeGreatherThanZero(this long val)
    {
        if (val <= 0)
        {
            throw new InvalidArgumentException($"invalid number ({val}). must be greather than 0.");
        }
    }
}