using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Video> Videos { get; } = new List<Video>();
}
