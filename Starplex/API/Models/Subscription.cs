using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models;

public partial class Subscription
{
    [JsonIgnore]
    public int SubscriptionId { get; set; }
    
    public int? SubscriberId { get; set; }
    public int? ChannelId { get; set; }
    [JsonIgnore]
    public virtual User? Channel { get; set; }
    [JsonIgnore]
    public virtual User? Subscriber { get; set; }
}
