using HawkerSearch.Web.Interfaces;
using System.Text.Json;

namespace HawkerSearch.Web.Services
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SetObject(string key, object value)
        {
            if (value is string)
            {
                _httpContextAccessor.HttpContext?.Session.SetString(key, value as string);
            }
            else
            {
                _httpContextAccessor.HttpContext?.Session.SetString(key, JsonSerializer.Serialize(value));
            }
        }
        public T GetObject<T>(string key)
        {
            var value = _httpContextAccessor.HttpContext?.Session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
        public void RemoveObject(string key)
        {
            _httpContextAccessor.HttpContext?.Session.Remove(key);
        }
    }
}
