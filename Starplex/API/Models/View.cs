using System;
using System.Collections.Generic;

namespace API.Models;

public partial class View
{
    public int ViewId { get; set; }

    public int? UserId { get; set; }

    public int? VideoId { get; set; }

    public DateTime? ViewDate { get; set; }

    public virtual User? User { get; set; }

    public virtual Video? Video { get; set; }
}
