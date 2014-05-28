using System;
using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using MvcSample.Core.Collections;
using MvcSample.Models;

namespace MvcSample.Core.Storage
{
    public class CallbackResultStore : ICallbackResultStore
    {
        protected IStore State { get; private set; }

        public int Count
        {
            get { return State.Count<CallbackResult>(); }
        }

        public CallbackResultStore(IStore store)
        {
            Ensure.That(store, "store").IsNotNull();

            State = store;
        }

        public virtual CallbackResult Put(IDictionary<string, string> values)
        {
            var entity = new CallbackResult(Count + 1, values);

            State.Put(entity.Id, entity);

            return entity;
        }

        public virtual IEnumerable<CallbackResult> GetAll()
        {
            return State.Query<CallbackResult>();
        }

        public virtual IEnumerable<CallbackResult> GetInitiallyAuthorizedPayments()
        {
            return GetPayments().Where(p => p.PaymentStatus == "AUTHORIZED");
        }

        public virtual IEnumerable<CallbackResult> GetFinalizablePayments()
        {
            return GetInitiallyAuthorizedPayments().Where(p => p.IsFinalized == false);
        }

        public virtual IEnumerable<CallbackResult> GetRefundablePayments()
        {
            var previouslyFinalizedButNotRefunded = GetInitiallyAuthorizedPayments().Where(p => p.IsFinalized && !p.IsRefunded);
            var initiallyCompleted = GetInitiallyCompletedPayments().Where(p => !p.IsRefunded);

            return previouslyFinalizedButNotRefunded.Merge(initiallyCompleted);
        }

        public virtual IEnumerable<CallbackResult> GetInitiallyCompletedPayments()
        {
            return GetPayments().Where(p => p.PaymentStatus == "COMPLETED");
        } 

        protected virtual IEnumerable<CallbackResult> GetPayments()
        {
            return State.Query<CallbackResult>().Where(i => i.IsPayment && i.Amount.HasValue);
        }

        public void MarkAsFinalized(string transactionId)
        {
            Apply(i => i.TransactionId == transactionId, i => i.MarkAsFinalized());
        }

        public void MarkAsRefunded(string transactionId)
        {
            Apply(i => i.TransactionId == transactionId, i => i.MarkAsRefunded());
        }

        protected virtual void Apply(Func<CallbackResult, bool> predicate, Action<CallbackResult> change)
        {
            State.Apply(predicate, change);
        }
    }
}