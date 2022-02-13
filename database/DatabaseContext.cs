using Microsoft.EntityFrameworkCore;

namespace database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoCache> VideoCaches { get; set; }
        public DbSet<Youtuber> Youtubers { get; set; }
        public DbSet<YoutuberCache> YoutuberCaches { get; set; }


        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost:5432;Username=postgres;Password=1234;Database=youtube");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Video>(e =>
            {
                e.Property(e => e.VideoId).HasColumnName("id");

                e.HasKey(e => e.VideoId)
                .HasName("video_id");

                e.ToTable("video");

                e.Property(e => e.IsFamilyFriendly).HasColumnName("is_family_friendly").HasColumnType("boolean");

                e.Property(e => e.UploadDate).HasColumnName("upload_date").HasColumnType("Date");

                e.Property(e => e.IFrame).HasColumnName("iframe").HasColumnType("varchar");

                e.Property(e => e.Length).HasColumnName("length").HasColumnType("bigint");

                e.Property(e => e.Country).HasColumnName("country").HasColumnType("varchar");

                e.HasOne(e => e.Uploader).WithMany(e => e.Videos).HasConstraintName("uploader_id");

                e.HasMany(e => e.Cache).WithOne(c => c.Video);

                e.HasOne(e => e.Category).WithMany(e => e.Videos).HasConstraintName("category_id");


            });

            modelBuilder.Entity<VideoCache>(e =>
            {

                e.HasKey(e => e.Id).HasName("videocache_id");

                e.ToTable("video_cache");

                e.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");

                e.Property(e => e.Date).HasColumnName("date").HasColumnType("Date").HasDefaultValueSql("getdate()");

                e.Property(e => e.Description).HasColumnName("description");

                e.Property(e => e.Likes).HasColumnName("likes").HasColumnType("bigint");

                e.Property(e => e.Title).HasColumnName("title");

                e.Property(e => e.Views).HasColumnName("views").HasColumnType("bigint");

            });

            modelBuilder.Entity<Youtuber>(e =>
            {
                e.HasKey(e => e.AccountUrl).HasName("youtuber_id");

                e.ToTable("youtuber");

                e.Property(e => e.UserName).HasColumnName("username").HasColumnType("varchar");

                e.HasMany(e => e.YoutuberCache).WithOne(e => e.Youtuber);
            });

            modelBuilder.Entity<YoutuberCache>(e =>
            {
                e.HasKey(e => e.Id).HasName("youtuber_cache_id");

                e.ToTable("youtuber_cache");

                e.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");

                e.Property(e => e.Date).HasColumnName("date").HasColumnType("Date").HasDefaultValueSql("getdate()");

                e.Property(e => e.SubscriberCount).HasColumnName("number_of_subscribers").HasColumnType("bigint");


            });
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(e => e.Id).HasName("category_id");

                e.ToTable("category");

                e.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");

                e.Property(e => e.CategoryString).HasColumnName("category").HasColumnType("varchar");

            });
        }

    }
}