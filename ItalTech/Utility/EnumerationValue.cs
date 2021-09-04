using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Utility
{
    public static class EnumerationValue
    {
        /// <summary>
        /// Restituisce, data una stringa, il corrispondente valore dell'Enumerations, se coincidente. 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="stringToConvert"></param>
        /// <returns></returns>
        public static string GetEnumValueByString(this Enum enumeration, string stringToConvert)
        {
            string enumValue = null; ;
            foreach (var en in Enum.GetValues(enumeration.GetType()))
            {
                if (en.GetDescription().Contains(stringToConvert))
                {
                    enumValue = en.ToString();
                }
            }
            return enumValue;
        }
    }
}
