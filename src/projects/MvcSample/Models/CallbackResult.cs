using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSample.Models
{
    public class CallbackResult
    {
        protected IDictionary<string, string> State { get; private set; }

        public int Id { get; private set; }
        public bool IsEmpty
        {
            get { return !State.Any(); }
        }
        public string TransactionId { get; private set; }
        public string SessionId { get; private set; }
        public string OccurredAt { get; private set; }
        public string Type { get; private set; }
        public bool IsPayment
        {
            get { return Type == "PAYMENT"; }
        }
        public decimal? Amount { get; private set; }
        public string CurrencyCode { get; private set; }
        public string PaymentStatus { get; private set; }
        public Guid? OrderId { get; private set; }
        public string OrderNumber { get; private set; }
        public string CardFirstSix { get; private set; }
        public string CardLastFour { get; private set; }
        public bool IsFinalized { get; private set; }
        public bool IsRefunded { get; private set; }

        public IEnumerable<KeyValuePair<string, string>> Values
        {
            get { return State; }
        }

        public CallbackResult(int id, IDictionary<string, string> values)
        {
            Id = id;
            State = values;
            InitializeFields();
        }

        protected void InitializeFields()
        {
            if(IsEmpty)
                return;

            TransactionId = GetString("TRANSACTION_ID");
            SessionId = GetString("SESSION_ID");
            OccurredAt = GetString("EVENT_TIMESTAMP");
            Type = GetString("EVENT_TYPE");
            Amount = GetDecimal("AMOUNT");
            CurrencyCode = GetString("CURRENCY_CODE");
            PaymentStatus = GetString("PAYMENT_STATUS");
            OrderId = GetGuid("ORDER_ID");
            OrderNumber = GetString("ORDER_NUMBER");
            CardFirstSix = GetString("CARD_FIRST_SIX");
            CardLastFour = GetString("CARD_LAST_FOUR");
        }

        public virtual void MarkAsFinalized()
        {
            IsFinalized = true;
        }

        public virtual void MarkAsRefunded()
        {
            IsRefunded = true;
        }

        public virtual string GetString(string field)
        {
            return State.ContainsKey(field) ? State[field] : null;
        }

        protected virtual Guid? GetGuid(string field)
        {
            return State.ContainsKey(field) ? Guid.Parse(State[field]) : (Guid?)null;
        }

        protected virtual decimal? GetDecimal(string field)
        {
            return State.ContainsKey(field) ? decimal.Parse(State[field]) : (decimal?) null;
        }
    }
}