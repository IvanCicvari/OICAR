using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class View
{
    [JsonIgnore]
    public int ViewId { get; set; }

    public int? UserId { get; set; }

    public int? VideoId { get; set; }
    public DateTime? ViewDate { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
    [JsonIgnore]
    public virtual Video? Video { get; set; }
}
