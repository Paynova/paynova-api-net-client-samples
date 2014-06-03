using System.Collections.Generic;
using MvcSample.Models;

namespace MvcSample.Core.Storage
{
    public interface IPostbackResultStore
    {
        int Count { get; }
        PostbackResult PutSuccessful(IDictionary<string, string> values);
        PostbackResult PutCancelled(IDictionary<string, string> values);
        PostbackResult PutPending(IDictionary<string, string> values);
    }
}