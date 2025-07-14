using AutoMapper;
using LinkBL.Ecxeptions;
using LinkBL.ModelBL.TableWithUrlBL;
using LinkBL.ModelBL.TableWithUrlBL.Dto;
using LinkDL.Context;
using LinkDL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace LinkBL.ModelBL.TableWithUrlCrud
{
    public class TableWithUrlCrud : ITableWithUrlCrud
    {
        private readonly ILinkContextFactory factory;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public TableWithUrlCrud(
            ILinkContextFactory factory,
            IMapper mapper,
            IConfiguration configuration)
        {
            this.factory = factory;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<ICollection<OutTableWithUrlDtoBL>> GetAllAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException();

            await using var context = await factory.CreateDbContext();

            var tables = await context.Set<TableWithUrl>()
                .AsNoTracking()
                .ToListAsync(token);

            return tables.Select(x => mapper.Map<OutTableWithUrlDtoBL>(x)).ToList();
        }

        public async Task<OutTableWithUrlDtoBL> GetAsync(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException();

            await using var context = await factory.CreateDbContext();

            if (id <= 0)
                throw new ArgumentOutOfRangeException("id is out of acceciable range");

            var table = await context.Set<TableWithUrl>().FirstOrDefaultAsync(x => x.Id == id);

            if (table is null)
                throw new DtoVereficationException("Table with id is not existed in database");

            return mapper.Map<OutTableWithUrlDtoBL>(table);
        }

        public async Task<OutTableWithUrlDtoBL> InsertAsync(string originalUrl, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException();

            await using var context = await factory.CreateDbContext();

            if (await context.Set<TableWithUrl>().AnyAsync(x => x.OriginalUrl == originalUrl))
                throw new DtoVereficationException($"{nameof(TableWithUrl.OriginalUrl)} is existed in database yet");

            int count;
            int amountOfSymbols = int.TryParse(configuration["Common:AmountOfSymbols"], out count) ? count : 5;

            var shortUrl = UrlGenerator.Generate(5);

            while (await context.Set<TableWithUrl>().AnyAsync(x => x.ShortUrl == shortUrl))
            {
                shortUrl = UrlGenerator.Generate(5);
            }

            var entity = await context.Set<TableWithUrl>().AddAsync(new TableWithUrl()
            {
                OriginalUrl = originalUrl,
                ShortUrl = shortUrl,
                CreatedAt = DateTime.UtcNow,
                CountRedirection = 0
            });

            await context.SaveChangesAsync(token);

            return mapper.Map<OutTableWithUrlDtoBL>(entity.Entity);
        }

        public async Task UpdateAsync(int id, string originalUrl, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException();

            await using var context = await factory.CreateDbContext();

            if (await context.Set<TableWithUrl>().AnyAsync(x => x.OriginalUrl == originalUrl))
                throw new DtoVereficationException($"{nameof(TableWithUrl.OriginalUrl)} is existed in database yet");

            var table = new TableWithUrl() { Id = id, OriginalUrl = originalUrl };

            context.Set<TableWithUrl>().Attach(table);
            context.Entry<TableWithUrl>(table).Property(x => x.OriginalUrl).IsModified = true;

            await context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException();

            await using var context = await factory.CreateDbContext();

            if (!await context.Set<TableWithUrl>().AnyAsync(x => x.Id == id))
                throw new DtoVereficationException($"{nameof(TableWithUrl)} with this {nameof(TableWithUrl.Id)} does not exist in database");

            context.Set<TableWithUrl>().Remove(new TableWithUrl() { Id = id });

            await context.SaveChangesAsync();
        }

        public async Task<OutMagnifyCountOfTransitionsDtoBL> MagnifyCountOfTransitionsAsync(string shortUrl, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                throw new OperationCanceledException();

            await using var context = await factory.CreateDbContext();

            var response = await context.Set<TableWithUrl>().FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);

            if (response is null)
                throw new DtoVereficationException($"{nameof(TableWithUrl.ShortUrl)} does not exist in database");

            response.CountRedirection = response.CountRedirection + 1;
            context.Entry<TableWithUrl>(response).Property(x => x.CountRedirection).IsModified = true;
            await context.SaveChangesAsync();

            return mapper.Map<OutMagnifyCountOfTransitionsDtoBL>(response);
        }
    }

    public interface ITableWithUrlCrud
    {
        Task DeleteAsync(int id, CancellationToken token);
        Task<OutTableWithUrlDtoBL> GetAsync(int id, CancellationToken token);
        Task<ICollection<OutTableWithUrlDtoBL>> GetAllAsync(CancellationToken token);
        Task<OutTableWithUrlDtoBL> InsertAsync(string originalUrl, CancellationToken token);
        Task UpdateAsync(int id, string originalUrl, CancellationToken token);
        Task<OutMagnifyCountOfTransitionsDtoBL> MagnifyCountOfTransitionsAsync(string shortUrl, CancellationToken token);
    }
}
