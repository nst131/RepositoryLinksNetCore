using LinkDL.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkDL.Context
{
    public class LinkContext : DbContext, ILinkContext
    {
        public LinkContext(DbContextOptions<LinkContext> option) : base(option) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TableWithUrlConfiguration());
        }

        public class LinkContextFactory : ILinkContextFactory
        {
            private readonly IDbContextFactory<LinkContext> factory;

            public LinkContextFactory(IDbContextFactory<LinkContext> factory)
            {
                this.factory = factory;
            }

            public async Task<ILinkContext> CreateDbContext()
            {
                return await factory.CreateDbContextAsync();
            }
        }
    }
}
