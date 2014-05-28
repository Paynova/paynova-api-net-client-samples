using System;
using System.Collections.Generic;
using System.Globalization;

namespace MvcSample.Models
{
    public class Payment
    {
        protected IDictionary<string, string> State { get; set; }

        public int Number { get; private set; }
        public bool IsCancelled { get; private set; }
        public bool IsFailed { get; private set; }
        public bool CanBeFinalized { get; private set; }
        public bool CanBeRefunded { get; private set; }
        public string TransactionId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal? Amount { get; private set; }

        public Payment(Guid orderId, int number, IDictionary<string, string> postedValues)
        {
            OrderId = orderId;
            Number = number;
            State = postedValues;

            InitializeFields();
        }

        protected void InitializeFields()
        {
            TransactionId = GetString("TRANSACTION_ID");
            Amount = GetDecimal("AMOUNT");
            IsCancelled = FieldHasValue("STATUS", "Cancelled");
            IsFailed = FieldHasValue("STATUS", "Failed");
            CanBeFinalized = FieldHasValue("STATUS", "Authorized");
            CanBeRefunded = FieldHasValue("STATUS", "Completed");
        }

        public virtual IEnumerable<KeyValuePair<string, string>> GetPostedValues()
        {
            return State;
        } 

        protected virtual bool FieldHasValue(string field, string expected)
        {
            var actual = GetString(field);

            return string.Equals(actual, expected, StringComparison.InvariantCulture);
        }

        protected virtual decimal? GetDecimal(string field)
        {
            decimal value;

            return decimal.TryParse(GetString(field), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value) ? value : (decimal?)null;
        }

        protected virtual string GetString(string field)
        {
            field = GenerateFullFieldName(field);

            return State.ContainsKey(field) ? State[field] : null;
        }

        protected virtual string GenerateFullFieldName(string suffix)
        {
            return string.Format("PAYMENT_{0}_{1}", Number, suffix);
        }
    }
}