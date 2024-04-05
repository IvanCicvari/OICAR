using System;
using System.Collections.Generic;

namespace WEBAPI.Models;

public partial class LikesDislike
{
    public int LikeId { get; set; }

    public int? UserId { get; set; }

    public int? VideoId { get; set; }

    public int? LikeStatus { get; set; }

    public virtual User? User { get; set; }

    public virtual Video? Video { get; set; }
}
