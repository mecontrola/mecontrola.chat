namespace MeControla.Core.Repositories
{
    public interface IPaginationFilter
    {
        int PageNumber { get; }
        int PageSize { get; }
    }
}