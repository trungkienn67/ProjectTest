using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace JWL.Repository
{
	public static class SessionExtensions
	{
		public static void SetJson(this ISession session , string key, object value) 
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}	
		public static T GetJson<T>(this ISession session , string key)
		{
			var sessionJWL = session.GetString(key);
			return sessionJWL == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionJWL);

		}
	}
}
