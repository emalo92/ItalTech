using System;
using System.Collections.Generic;

namespace ItalTech.Models.TableToExport
{
    /// <summary>
    /// Il tipo Column definisce sia nome e tipo degli elementi, sia gli elementi stessi
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Nome della colonna
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Tipo della colonna
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Indice della colonna (serve per stabilire in che posizione si colloca la colonna nella tabella)
        /// </summary>
        public int IndexOrder { get; set; }
        /// <summary>
        /// Elementi della colonna
        /// </summary>
        public List<object> Elements { get; set; }

        /// <summary>
        /// Contiene tutte le informazioni di configurazione della colonna
        /// </summary>
        public Infrastruttura.Models.Configuration.TableExportConfig ColumnConfiguration { get; set; }

        /// <summary>
        /// Converte nell'oggetto Column degli elementi presi da una tabella
        /// </summary>
        /// <param name="name">Nome della colonna</param>
        /// <param name="indexOrder">Indice degli elementi che costituiscono la colonna</param>
        /// <param name="elements">Lista degli elementi della tabella</param>
        /// <returns></returns>
        public static Column ConvertToColumn(string name, int indexOrder, List<List<object>> elements, Infrastruttura.Models.Configuration.TableExportConfig columnConfiguration)
        {
            var column = new Column
            {
                Name = name,
                Type = GetSafeType(elements, indexOrder),
                //Type = elements[0][indexOrder] != null ? elements[0][indexOrder].GetType() : typeof(string),
                IndexOrder = indexOrder,
                ColumnConfiguration = columnConfiguration
            };
            column.Elements = new List<object>();
            foreach (var riga in elements)
            {
                column.Elements.Add(riga[indexOrder]);
            }
            return column;
        }

        private static Type GetSafeType(List<List<object>> elements, int indexOrder)
        {
            int dim = elements.Count;
            int index = 0;
            while (index < dim)
            {
                if(elements[index][indexOrder] != null)
                {
                    return elements[index][indexOrder].GetType();
                }
                index++;
            }
            return typeof(string);  //Se non ho trovato nessuna riga con la colonna valorizzata restituisco come type la string
        }
    }
}
