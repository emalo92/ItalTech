using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace ItalTech.ExtensionMethods
{
    public static class TempDataExt
    {
		/// <summary>
		/// Inserisce la coppia key/value nel TempData
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tempData"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void Set<T>(this ITempDataDictionary tempData, string key, T value)
		{
			tempData[key] = JsonSerializer.Serialize(value);
		}

		/// <summary>
		/// Legge i dati e alla uscita dalla action i dati vengono eliminati dal TempData
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tempData"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T Get<T>(this ITempDataDictionary tempData, string key)
		{
            tempData.TryGetValue(key, out object o);
            return o == null ? default : JsonSerializer.Deserialize<T>((string)o);
		}

		/// <summary>
		/// Keep a differenza di Get mantiene i dati nel TempData dopo l'uscita dalla action
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="tempData"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T Keep<T>(this ITempDataDictionary tempData, string key)
		{
			tempData.Keep(key);
			tempData.TryGetValue(key, out object o);
			return o == null ? default : JsonSerializer.Deserialize<T>((string)o);
		}
	}
}
