using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class LikesDislike
{
    public int LikeId { get; set; }

    public int? UserId { get; set; }

    public int? VideoId { get; set; }

    public int? LikeStatus { get; set; }
    [JsonIgnore]

    public virtual User? User { get; set; }
    [JsonIgnore]

    public virtual Video? Video { get; set; }
}
