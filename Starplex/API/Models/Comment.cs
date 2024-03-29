using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? UserId { get; set; }

    public int? VideoId { get; set; }

    public string? CommentText { get; set; }

    public DateTime? CommentDate { get; set; }
    [JsonIgnore]

    public virtual User? User { get; set; }
    [JsonIgnore]

    public virtual Video? Video { get; set; }
}
