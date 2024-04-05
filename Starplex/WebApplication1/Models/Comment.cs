using System;
using System.Collections.Generic;

namespace WEBAPI.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? UserId { get; set; }

    public int? VideoId { get; set; }

    public string? CommentText { get; set; }

    public DateTime? CommentDate { get; set; }

    public virtual User? User { get; set; }

    public virtual Video? Video { get; set; }
}
