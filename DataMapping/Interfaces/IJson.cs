namespace DataMapping.Interfaces
{
    public interface IJson
    {
        IJson InitByJson(string json);
        string ToJson();
    }
}