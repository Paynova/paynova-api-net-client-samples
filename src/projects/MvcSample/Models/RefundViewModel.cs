using System.Collections.Generic;
using System.Linq;
using Paynova.Api.Client.Responses;

namespace MvcSample.Models
{
    public class RefundViewModel
    {
        public Refundable[] RefundableItems { get; private set; }
        public RefundPaymentResponse Refunded { get; set; }

        public RefundViewModel()
        {
            RefundableItems = new Refundable[0];
        }

        public virtual RefundViewModel SetRefundable(IEnumerable<CallbackResult> refundable)
        {
            RefundableItems = refundable
                .Select(i => new Refundable
                {
                    TransactionId = i.TransactionId,
                    TotalAmount = i.Amount.HasValue ? i.Amount.Value : 0,
                    Meta = new Refundable.MetaData
                    {
                        OrderNumber = i.OrderNumber,
                        PaymentMethod = i.Type
                    }
                })
                .ToArray();

            return this;
        }
    }
}