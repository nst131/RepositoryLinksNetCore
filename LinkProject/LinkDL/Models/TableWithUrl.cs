using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDL.Models
{
    public class TableWithUrl
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CountRedirection { get; set; } = 0;
    }

    public class TableWithUrlConfiguration : IEntityTypeConfiguration<TableWithUrl>
    {
        public void Configure(EntityTypeBuilder<TableWithUrl> builder)
        {
            builder.ToTable(nameof(TableWithUrl)).HasKey(e => e.Id);

            builder.Property(x => x.OriginalUrl)
           .IsRequired()
           .HasMaxLength(256)
           .HasColumnName(nameof(TableWithUrl.OriginalUrl));

            builder.Property(x => x.ShortUrl)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName(nameof(TableWithUrl.ShortUrl));

            builder
            .HasIndex(x => x.ShortUrl)
            .IsUnique();

            builder.Property(x => x.CreatedAt)
            .IsRequired();

            builder.Property(x => x.CountRedirection)
            .IsRequired();
        }
    }
}
