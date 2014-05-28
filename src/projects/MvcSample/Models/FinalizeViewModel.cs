using System;
using System.Collections.Generic;
using System.Linq;
using Paynova.Api.Client.Responses;

namespace MvcSample.Models
{
    public class FinalizeViewModel
    {
        public Finalizable[] FinalizableItems { get; private set; }
        public FinalizeAuthorizationResponse Finalized { get; set; }

        public FinalizeViewModel()
        {
            FinalizableItems = new Finalizable[0];
        }

        public virtual FinalizeViewModel SetFinalizable(IEnumerable<CallbackResult> finalizable)
        {
            FinalizableItems = finalizable
                .Select(i => new Finalizable
                {
                    TransactionId = i.TransactionId,
                    OrderId = i.OrderId.HasValue ? i.OrderId.Value : Guid.Empty,
                    Amount = i.Amount.HasValue ? i.Amount.Value : 0,
                    Meta = new Finalizable.MetaData
                    {
                        CurrencyCode = i.CurrencyCode,
                        CardFirstSix = i.CardFirstSix,
                        CardLastFour = i.CardLastFour    
                    }
                })
                .ToArray();

            return this;
        }
    }
}