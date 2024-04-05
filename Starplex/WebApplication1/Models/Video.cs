using System;
using System.Collections.Generic;

namespace WEBAPI.Models;

public partial class Video
{
    public int Idvideo { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? UploadDate { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? VideoUrl { get; set; }

    public int? Duration { get; set; }

    public string? Categories { get; set; }

    public string? PrivacySetting { get; set; }

    public int? TotalLikes { get; set; }

    public int? TotalViews { get; set; }

    public int? TotalSubscribers { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<LikesDislike> LikesDislikes { get; } = new List<LikesDislike>();

    public virtual User? User { get; set; }

    public virtual ICollection<View> Views { get; } = new List<View>();

    public virtual ICollection<Genre> Genres { get; } = new List<Genre>();

    public virtual ICollection<Tag> Tags { get; } = new List<Tag>();
}
