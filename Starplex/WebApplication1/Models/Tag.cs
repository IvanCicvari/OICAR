using System;
using System.Collections.Generic;

namespace WEBAPI.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public virtual ICollection<Video> Videos { get; } = new List<Video>();
}
