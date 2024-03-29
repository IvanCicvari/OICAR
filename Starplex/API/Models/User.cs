using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using API.Models;

namespace API.Models;

public partial class User
{
    public int Iduser { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    [JsonIgnore]
    public string? PasswordSalt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? ProfileImage { get; set; }

    public string? Bio { get; set; }

    public bool? IsVerified { get; set; }

    public string? SubscriptionStatus { get; set; }
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