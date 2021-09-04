using Infrastruttura.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastruttura.ExtensionMethods
{
    public static class TableExportConfigExtension
    {
        /// <summary>
        /// Aggiunge una nuova colonna alla lista
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="tableName"></param>
        /// <param name="originalColumnName"></param>
        /// <param name="originalIndexOrder"></param>
        /// <param name="isSelected"></param>
        /// <param name="currentColumnName"></param>
        /// <param name="currentIndexOrder"></param>
        /// <returns></returns>
        public static List<TableExportConfig> AddNewColum(this List<TableExportConfig> lista, string tableName, string originalColumnName, int originalIndexOrder,
            bool? isSelected = null, string currentColumnName = null, int? currentIndexOrder = null)
        {
            if (lista == null)
                lista = new List<TableExportConfig>();
            lista.Add(new TableExportConfig()
            {
                TableName = tableName,
                OriginalColumnName = originalColumnName,
                OriginalIndexOrder = originalIndexOrder,
                IsSelected = isSelected,
                CurrentColumnName = currentColumnName,
                CurrentIndexOrder = currentIndexOrder
            });
            return lista;
        }
    }
}
