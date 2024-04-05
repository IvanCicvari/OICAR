using System;
using System.Collections.Generic;

namespace WEBAPI.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Video> Videos { get; } = new List<Video>();
}
