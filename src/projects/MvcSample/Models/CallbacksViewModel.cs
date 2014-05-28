namespace MvcSample.Models
{
    public class CallbacksViewModel
    {
        public CallbackResult[] Callbacks { get; private set; }

        public CallbacksViewModel(CallbackResult[] callbacks)
        {
            Callbacks = callbacks;
        }
    }
}