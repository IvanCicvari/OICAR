using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class User
{
    [JsonIgnore]
    public int Iduser { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }
    [JsonIgnore]

    public string? PasswordHash { get; set; }
    [JsonIgnore]

    public string? PasswordSalt { get; set; }
    [JsonIgnore]

    public DateTime? CreatedAt { get; set; }
    [JsonIgnore]

    public DateTime? LastLogin { get; set; }
    [JsonIgnore]

    public string? ProfileImage { get; set; }

    public string? Bio { get; set; }
    [JsonIgnore]

    public bool? IsVerified { get; set; }
    [JsonIgnore]

    public string? SubscriptionStatus { get; set; }

    public string? Password { get; set; }
    [JsonIgnore]

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
    [JsonIgnore]
    public virtual ICollection<LikesDislike> LikesDislikes { get; } = new List<LikesDislike>();
    [JsonIgnore]
    public virtual ICollection<Subscription> SubscriptionChannels { get; } = new List<Subscription>();
    [JsonIgnore]
    public virtual ICollection<Subscription> SubscriptionSubscribers { get; } = new List<Subscription>();
    [JsonIgnore]
    public virtual ICollection<Video> Videos { get; } = new List<Video>();
    [JsonIgnore]
    public virtual ICollection<View> Views { get; } = new List<View>();
}
