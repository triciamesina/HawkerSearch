namespace HawkerSearch.Web.Interfaces
{
    public interface ISessionManager
    {
        void SetObject(string key, object value);
        T GetObject<T>(string key);
        void RemoveObject(string key);
    }
}
