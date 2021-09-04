using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ItalTech.Utility;

namespace ItalTech.ExtensionMethods
{
    public static class GenericExtension
    {
        /// <summary>
        /// Restituisce un clone dynamic delle sole proprietà passate in input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public static dynamic CloneSomeProperties<T>(this T source, params string[] propertyNames)
        {
            if (source == null)
                return null;
            BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;
            Type t = typeof(T);

            dynamic objectToReturn =  new ExpandoObject();

            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo sPI in properties)
            {
                if (propertyNames.Contains(sPI.Name))
                {
                    PropertyInfo tPI = t.GetProperty(sPI.Name, flags);
                    if (tPI != null && tPI.CanWrite && tPI.PropertyType.IsAssignableFrom(sPI.PropertyType))
                    {
                        ((IDictionary<string, object>)objectToReturn).Add(sPI.Name, sPI.GetValue(source, null));
                    }
                }
            }
            return objectToReturn;
        }

        /// <summary>
        /// Restituisce un clone dynamic dell'oggetto eccetto le proprietà passate in input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public static dynamic CloneExceptSomeProperties<T>(this T source, params string[] propertyNames)
        {
            if (source == null)
                return null;
            BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;
            Type t = typeof(T);

            dynamic objectToReturn = new ExpandoObject();

            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo sPI in properties)
            {
                if (propertyNames.Contains(sPI.Name))
                {
                    continue;
                }
                PropertyInfo tPI = t.GetProperty(sPI.Name, flags);
                if (tPI != null && tPI.CanWrite && tPI.PropertyType.IsAssignableFrom(sPI.PropertyType))
                {
                    ((IDictionary<string, object>)objectToReturn).Add(sPI.Name, sPI.GetValue(source, null));
                }
            }
            return objectToReturn;
        }

        /// <summary>
        /// Clona tutte le proprietà dall'oggetto iniziale, eccetto quelle contenute nell'oggetto newDataToCopy da cui saranno copiati i valori.
        /// Se newDataToCopy è null fa un clone identico a quello iniziale. 
        /// </summary>
        /// <typeparam name="T1">Tipo del'oggetto sorgente</typeparam>
        /// <typeparam name="T2">Tipo dell'oggetto da cui copiare i valori delle proprietà che hanno lo stesso nome del sorgente</typeparam>
        /// <param name="source">Sorgente</param>
        /// <param name="newDataToCopy">Oggetto da cui copiare i valori delle proprietà che hanno lo stesso nome del sorgente</param>
        /// <param name="newDataParams">Array delle proprietà contenute nell'oggetto newDataToCopy che matchano con l'oggetto iniziale</param>
        /// <returns></returns>
        public static T1 CloneSomeProperties<T1, T2>(this T1 source, T2 newDataToCopy, out string[] newDataParams)
        {
            var newDataParamList = new List<string>();
            if (source == null)
            {
                newDataParams = newDataParamList.ToArray();
                return default;
            }
                
            BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;
            Type t1 = typeof(T1);
            Type t2 = typeof(T2);

            T1 result = Activator.CreateInstance<T1>();

            PropertyInfo[] properties1 = t1.GetProperties();
            List<PropertyInfo> properties2 = t2.GetProperties().ToList();

            foreach (PropertyInfo sPI in properties1)
            {
                if (newDataToCopy != null && properties2.Select(p => p.Name).Contains(sPI.Name))
                {
                    PropertyInfo tPI = t2.GetProperty(sPI.Name, flags);
                    if (tPI != null && tPI.CanWrite && tPI.PropertyType.IsAssignableFrom(tPI.PropertyType))
                    {
                        sPI.SafeSetValue(newDataToCopy, result, tPI);
                        newDataParamList.Add(sPI.Name);
                    }
                }
                else
                {
                    PropertyInfo tPI = t1.GetProperty(sPI.Name, flags);
                    if (tPI != null && tPI.CanWrite && tPI.PropertyType.IsAssignableFrom(sPI.PropertyType))
                    {
                        sPI.SetValue(result, tPI.GetValue(source, null));
                    }
                }
            }
            newDataParams = newDataParamList.ToArray();
            return result;
        }

        private static void SafeSetValue<T1, T2>(this PropertyInfo sPI, T2 newDataToCopy, T1 result, PropertyInfo tPI)
        {
            var value = tPI.GetValue(newDataToCopy, null);
            if(sPI.PropertyType == typeof(string) && value != null && value.GetType().IsEnum)
            {
                sPI.SetValue(result, value.GetDescription());
            }
            else 
            {
                sPI.SetValue(result, value);
            }
        }
    }
}
