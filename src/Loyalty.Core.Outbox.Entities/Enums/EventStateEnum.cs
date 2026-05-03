using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Core.Outbox.Entities.Enums
{
    public enum EventStateEnum
    {
        NotPublished = 0,

        InProgress = 1,

        Published = 2,

        PublishedFailed = 3
    }
}
