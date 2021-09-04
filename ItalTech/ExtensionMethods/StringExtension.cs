using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.ExtensionMethods
{
    public static class StringExtension
    {
        /// <summary>
        /// Aggiunge allo username  un indirizzo email fake
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetEmailByUsername(this string userName, string emailPostFIx)
        {
            return userName + emailPostFIx;
        }
    }
}
