namespace MvcSample.Models
{
    public class PostbackViewModel
    {
        public PostbackResult PostbackResult { get; private set; }

        public PostbackViewModel(PostbackResult postbackResult)
        {
            PostbackResult = postbackResult;
        }
    }
}