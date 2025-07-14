
namespace LinkDL.Context
{
    public interface ILinkContextFactory
    {
        Task<ILinkContext> CreateDbContext();
    }
}