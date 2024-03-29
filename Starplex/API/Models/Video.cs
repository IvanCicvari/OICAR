using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Video
{
    [JsonIgnore]
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
    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
    [JsonIgnore]
    public virtual ICollection<LikesDislike> LikesDislikes { get; } = new List<LikesDislike>();
    [JsonIgnore]
    public virtual User? User { get; set; }
    [JsonIgnore]
    public virtual ICollection<View> Views { get; } = new List<View>();
}
