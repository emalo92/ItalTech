using System;
using System.Collections.Generic;
using System.Linq;

namespace ItalTech.ExtensionMethods
{
    public static class DictionaryExtension
    {
        /// <summary>
        /// Trasforma un dictionary (string, dynamic) nella querystring  relativa
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string GetQueryString(this Dictionary<string, dynamic> dict)
        {
            if (dict == null || dict.Count == 0)
                return "";
            return dict.GetDictionaryStrings().Select(k => k.Key + "=" + k.Value).Aggregate((a, b) => a + "&" + b);
        }
        /// <summary>
        /// Trasforma un dictionary (string, dynamic) in dictionary (string, string). Usato per le viste.
        /// Del Datetime fa il seguente ToString(yyyy-MM-ddTHH:mm).
        /// Del decimal fa il seguente ToString(.00).
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionaryStrings(this Dictionary<string, dynamic> dict)
        {
            if (dict == null)
                return null;
            if (dict.Count == 0)
                return new Dictionary<string, string>();
            var dateTimeConverter = "yyyy-MM-ddTHH':'mm"; //Questa formattazione serve per visualizzare correttamente la data / orario nei DatePicker
            var decimalConverter = ".00";
            return dict.Select(d => (KeyValuePair<string, string>)GetSafeDictionaryFromDynamic(d.Key, d.Value, dateTimeConverter, decimalConverter)).ToDictionary(k => k.Key, v => v.Value);
        }

        /// <summary>
        /// Trasforma la coppia (string, dynamic) in coppia (string, string).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dateTimeFormatter">Formattazione della data</param>
        /// <param name="decimalFormatter">Formattazione del decimal</param>
        /// <returns></returns>
        static KeyValuePair<string, string> GetSafeDictionaryFromDynamic(string key, dynamic value, string dateTimeFormatter, string decimalFormatter)
        {
            string valStr = null;
            if (value != null)
            {
                if (value is DateTime)
                {
                    valStr = value.ToString(dateTimeFormatter);
                }
                else if (value is decimal)
                {
                    valStr = value.ToString(decimalFormatter);
                }
                else if(value is bool)
                {
                    valStr = value.ToString().ToLower();
                }
                else
                {
                    valStr = value.ToString();
                }
            }
            return new KeyValuePair<string, string>(key, valStr);
        }
    }
}
