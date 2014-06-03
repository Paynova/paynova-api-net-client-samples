using System;

namespace MvcSample.Models
{
    [Serializable]
    public enum PostbackResultType
    {
        Successful,
        Cancelled,
        Pending
    }
}