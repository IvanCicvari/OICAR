using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WEBAPI.Models;

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

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<LikesDislike> LikesDislikes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<View> Views { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("name=ConnectionStrings:StarplexConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__E7957687E1FDFE27");

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
                .HasConstraintName("FK__Comments__user_i__52593CB8");

            entity.HasOne(d => d.Video).WithMany(p => p.Comments)
                .HasForeignKey(d => d.VideoId)
                .HasConstraintName("FK__Comments__video___534D60F1");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__18428D42D49D914C");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.GenreName).HasColumnName("genre_name");
        });

        modelBuilder.Entity<LikesDislike>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__LikesDis__992C7930F49BB206");

            entity.HasIndex(e => new { e.UserId, e.VideoId, e.LikeStatus }, "UQ__LikesDis__F8867C00D2D04E69").IsUnique();

            entity.Property(e => e.LikeId).HasColumnName("like_id");
            entity.Property(e => e.LikeStatus).HasColumnName("like_status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoId).HasColumnName("video_id");

            entity.HasOne(d => d.User).WithMany(p => p.LikesDislikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__LikesDisl__user___440B1D61");

            entity.HasOne(d => d.Video).WithMany(p => p.LikesDislikes)
                .HasForeignKey(d => d.VideoId)
                .HasConstraintName("FK__LikesDisl__video__44FF419A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC23DA5A41");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName).HasColumnName("role_name");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__863A7EC1AC624862");

            entity.HasIndex(e => new { e.SubscriberId, e.ChannelId }, "UQ__Subscrip__87B034E7CA1643C5").IsUnique();

            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.SubscriberId).HasColumnName("subscriber_id");

            entity.HasOne(d => d.Channel).WithMany(p => p.SubscriptionChannels)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("FK__Subscript__chann__4E88ABD4");

            entity.HasOne(d => d.Subscriber).WithMany(p => p.SubscriptionSubscribers)
                .HasForeignKey(d => d.SubscriberId)
                .HasConstraintName("FK__Subscript__subsc__4D94879B");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__4296A2B623CF5320");

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.TagName).HasColumnName("tag_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__Users__EAE6D9DFFA6B6993");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164E5C010CF").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5729748B7FB").IsUnique();

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
            entity.Property(e => e.Password).HasColumnName("password");
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
            entity.HasKey(e => e.Idvideo).HasName("PK__Videos__6521D6B3B68DB983");

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
                .HasConstraintName("FK__Videos__user_id__403A8C7D");

            entity.HasMany(d => d.Genres).WithMany(p => p.Videos)
                .UsingEntity<Dictionary<string, object>>(
                    "VideoGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__VideoGenr__genre__5EBF139D"),
                    l => l.HasOne<Video>().WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__VideoGenr__video__5DCAEF64"),
                    j =>
                    {
                        j.HasKey("VideoId", "GenreId").HasName("PK__VideoGen__D97536C4F57DA92E");
                    });

            entity.HasMany(d => d.Tags).WithMany(p => p.Videos)
                .UsingEntity<Dictionary<string, object>>(
                    "VideoTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__VideoTags__tag_i__5AEE82B9"),
                    l => l.HasOne<Video>().WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__VideoTags__video__59FA5E80"),
                    j =>
                    {
                        j.HasKey("VideoId", "TagId").HasName("PK__VideoTag__9CD8743B369C67B7");
                    });
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.ViewId).HasName("PK__Views__B5A34EE21A6E1DDF");

            entity.HasIndex(e => new { e.UserId, e.VideoId, e.ViewDate }, "UQ__Views__0D2E646F0F0B3B38").IsUnique();

            entity.Property(e => e.ViewId).HasColumnName("view_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoId).HasColumnName("video_id");
            entity.Property(e => e.ViewDate)
                .HasColumnType("datetime")
                .HasColumnName("view_date");

            entity.HasOne(d => d.User).WithMany(p => p.Views)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Views__user_id__48CFD27E");

            entity.HasOne(d => d.Video).WithMany(p => p.Views)
                .HasForeignKey(d => d.VideoId)
                .HasConstraintName("FK__Views__video_id__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
