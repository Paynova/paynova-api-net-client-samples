using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSample.Models
{
    public class PostbackResult
    {
        protected IDictionary<string, string> State { get; private set; }

        public Guid Id { get { return SessionId; } }
        public PostbackResultType Type { get; private set; }
        public bool IsEmpty
        {
            get { return !State.Any(); }
        }
        public Guid SessionId { get; private set; }
        public string SessionStatus { get; private set; }
        public string Digest { get; private set; }
        public Guid OrderId { get; private set; }
        public string OrderNumber { get; private set; }
        public string CurrencyCode { get; private set; }
        public int PaymentCount { get; private set; }

        public PostbackResult(PostbackResultType type, IDictionary<string, string> postedValues)
        {
            Type = type;
            State = postedValues;
            InitializeFields();
        }

        protected void InitializeFields()
        {
            if (IsEmpty)
                return;

            SessionId = GetGuid("SESSION_ID");
            SessionStatus = GetString("SESSION_STATUS");
            Digest= GetString("DIGEST");
            OrderId = GetGuid("ORDER_ID");
            OrderNumber = GetString("ORDER_NUMBER");
            CurrencyCode = GetString("CURRENCY_CODE");
            PaymentCount = GetInt("PAYMENT_COUNT");
        }

        public virtual string GetString(string field)
        {
            return State.ContainsKey(field) ? State[field] : null;
        }

        protected virtual Guid GetGuid(string field)
        {
            return Guid.Parse(State[field]);
        }

        protected virtual int GetInt(string field)
        {
            return int.Parse(State[field]);
        }

        public virtual IEnumerable<KeyValuePair<string, string>> GetNonPaymentPostedValues()
        {
            return State.Where(kv => !IsPayment(kv.Key));
        }

        public virtual IEnumerable<Payment> GetPayments()
        {
            var orderId = Guid.Parse(State["ORDER_ID"]);

            return State
                .Select(kv => new
                {
                    Key = kv.Key,
                    Value = kv.Value,
                    PaymentNumber = GetPaymentNumber(kv.Key)
                })
                .Where(i => i.PaymentNumber.HasValue)
                .GroupBy(i => i.PaymentNumber.Value, i => i)
                .Select(i => new Payment(orderId, i.Key, i.ToDictionary(kv => kv.Key, kv => kv.Value)));
        }

        protected virtual bool IsPayment(string key)
        {
            return GetPaymentNumber(key).HasValue;
        }

        protected virtual int? GetPaymentNumber(string key)
        {
            if (!key.StartsWith("PAYMENT_"))
                return null;

            var parts = key.Split(new[] { '_' });
            if (parts.Length < 3)
                return null;

            int num;

            if (int.TryParse(parts[1], out num))
                return num;

            return null;
        }
    }
}