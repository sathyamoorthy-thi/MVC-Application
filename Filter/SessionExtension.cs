using System.Text.Json;




public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static Result  GetObjectFromJson<Result>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(Result) : JsonSerializer.Deserialize<Result>(value);
    }
}