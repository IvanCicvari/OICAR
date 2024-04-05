using System;
using System.Collections.Generic;

namespace WEBAPI.Models;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int? SubscriberId { get; set; }

    public int? ChannelId { get; set; }

    public virtual User? Channel { get; set; }

    public virtual User? Subscriber { get; set; }
}
