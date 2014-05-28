using System.Collections.Generic;
using MvcSample.Models;

namespace MvcSample.Core.Storage
{
    public interface ICallbackResultStore
    {
        int Count { get; }
        CallbackResult Put(IDictionary<string, string> values);
        IEnumerable<CallbackResult> GetAll();
        IEnumerable<CallbackResult> GetInitiallyAuthorizedPayments();
        IEnumerable<CallbackResult> GetFinalizablePayments();
        IEnumerable<CallbackResult> GetRefundablePayments();
        IEnumerable<CallbackResult> GetInitiallyCompletedPayments();
        void MarkAsFinalized(string transactionId);
        void MarkAsRefunded(string transactionId);
    }
}