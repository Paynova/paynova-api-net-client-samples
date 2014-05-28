using System.Collections.Generic;
using System.Dynamic;

namespace MvcSample.Core.Collections
{
    public class DynamicDictionary : DynamicObject
    {
        private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name.ToLower(), out result);
        }

        // If you try to set a value of a property that is 
        // not defined in the class, this method is called. 
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dictionary[binder.Name.ToLower()] = value;

            return true;
        }
    }
}