namespace MvcSample.Core.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(T item);
    }
}