using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ItalTech.ExtensionMethods
{
    public static class SessionExt
    {
        /// <summary>
        /// Salva in sessione un oggetto
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void SetObj(this ISession session, string key, object obj)
        {
            var serializedObject = JsonSerializer.Serialize(obj);
            session.SetString(key, serializedObject);
        }

        /// <summary>
        /// Recupera dalla sessione un oggetto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObj<T>(this ISession session, string key)
        {
            var serializedObject = session.GetString(key);
            if (serializedObject == null)
                return default;
            return JsonSerializer.Deserialize<T>(serializedObject);
        }
    }
}
