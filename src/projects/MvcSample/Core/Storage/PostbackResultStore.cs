using System.Collections.Generic;
using EnsureThat;
using MvcSample.Models;

namespace MvcSample.Core.Storage
{
    public class PostbackResultStore : IPostbackResultStore
    {
        protected IStore State { get; private set; }

        public int Count
        {
            get { return State.Count<PostbackResult>(); }
        }

        public PostbackResultStore(IStore store)
        {
            Ensure.That(store, "store").IsNotNull();

            State = store;
        }

        public virtual PostbackResult PutSuccessful(IDictionary<string, string> values)
        {
            return Put(PostbackResultType.Successful, values);
        }

        public virtual PostbackResult PutCancelled(IDictionary<string, string> values)
        {
            return Put(PostbackResultType.Cancelled, values);
        }

        public virtual PostbackResult PutPending(IDictionary<string, string> values)
        {
            return Put(PostbackResultType.Pending, values);
        }

        protected virtual PostbackResult Put(PostbackResultType type, IDictionary<string, string> values)
        {
            var entity = new PostbackResult(type, values);

            State.Put(entity.Id, entity);

            return entity;
        }
    }
}