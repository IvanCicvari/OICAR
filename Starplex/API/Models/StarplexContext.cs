using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class StarplexContext : DbContext
{
    public StarplexContext()
    {
    }

    public StarplexContext(DbContextOptions<StarplexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<LikesDislike> LikesDislikes { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<View> Views { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=.\\SQLExpress2;Database=Starplex;User=sa;Password=SQL;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__E7957687D8FE1B18");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.CommentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("comment_date");
            entity.Property(e => e.CommentText).HasColumnName("comment_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoId).HasColumnName("video_id");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comments__user_i__6477ECF3");

            entity.HasOne(d => d.Video).WithMany(p => p.Comments)
                .HasForeignKey(d => d.VideoId)
                .HasConstraintName("FK__Comments__video___656C112C");
        });

        modelBuilder.Entity<LikesDislike>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__LikesDis__992C793038EDD8FA");

            entity.HasIndex(e => new { e.UserId, e.VideoId, e.LikeStatus }, "UQ__LikesDis__F8867C009523D36B").IsUnique();

            entity.Property(e => e.LikeId).HasColumnName("like_id");
            entity.Property(e => e.LikeStatus).HasColumnName("like_status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoId).HasColumnName("video_id");

            entity.HasOne(d => d.User).WithMany(p => p.LikesDislikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__LikesDisl__user___5629CD9C");

            entity.HasOne(d => d.Video).WithMany(p => p.LikesDislikes)
                .HasForeignKey(d => d.VideoId)
                .HasConstraintName("FK__LikesDisl__video__571DF1D5");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__863A7EC1F03DF4EF");

            entity.HasIndex(e => new { e.SubscriberId, e.ChannelId }, "UQ__Subscrip__87B034E702F811F0").IsUnique();

            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.SubscriberId).HasColumnName("subscriber_id");

            entity.HasOne(d => d.Channel).WithMany(p => p.SubscriptionChannels)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("FK__Subscript__chann__60A75C0F");

            entity.HasOne(d => d.Subscriber).WithMany(p => p.SubscriptionSubscribers)
                .HasForeignKey(d => d.SubscriberId)
                .HasConstraintName("FK__Subscript__subsc__5FB337D6");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__Users__EAE6D9DF58E34BA1");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616465DBF26A").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5726E78C5CC").IsUnique();

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.IsVerified)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_verified");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
            entity.Property(e => e.ProfileImage).HasColumnName("profile_image");
            entity.Property(e => e.SubscriptionStatus).HasColumnName("subscription_status");
            entity.Property(e => e.Username)
                .HasMaxLength(225)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Idvideo).HasName("PK__Videos__6521D6B3A79F6F22");

            entity.Property(e => e.Idvideo).HasColumnName("IDVideo");
            entity.Property(e => e.Categories).HasColumnName("categories");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.PrivacySetting).HasColumnName("privacy_setting");
            entity.Property(e => e.ThumbnailUrl).HasColumnName("thumbnail_url");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TotalLikes)
                .HasDefaultValueSql("((0))")
                .HasColumnName("total_likes");
            entity.Property(e => e.TotalSubscribers)
                .HasDefaultValueSql("((0))")
                .HasColumnName("total_subscribers");
            entity.Property(e => e.TotalViews)
                .HasDefaultValueSql("((0))")
                .HasColumnName("total_views");
            entity.Property(e => e.UploadDate)
                .HasColumnType("datetime")
                .HasColumnName("upload_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoUrl).HasColumnName("video_url");

            entity.HasOne(d => d.User).WithMany(p => p.Videos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Videos__user_id__52593CB8");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.ViewId).HasName("PK__Views__B5A34EE22F71F327");

            entity.HasIndex(e => new { e.UserId, e.VideoId, e.ViewDate }, "UQ__Views__0D2E646FBB2ACCED").IsUnique();

            entity.Property(e => e.ViewId).HasColumnName("view_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoId).HasColumnName("video_id");
            entity.Property(e => e.ViewDate)
                .HasColumnType("datetime")
                .HasColumnName("view_date");

            entity.HasOne(d => d.User).WithMany(p => p.Views)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Views__user_id__5AEE82B9");

            entity.HasOne(d => d.Video).WithMany(p => p.Views)
                .HasForeignKey(d => d.VideoId)
                .HasConstraintName("FK__Views__video_id__5BE2A6F2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
