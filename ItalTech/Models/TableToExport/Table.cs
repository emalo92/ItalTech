using System.Collections.Generic;
using System.Linq;
using ItalTech.Models.Interfaces;
using Infrastruttura.ExtensionMethods;

namespace ItalTech.Models.TableToExport
{
    public class Table : IConvertToTableExport
    {
        /// <summary>
        /// Titolo della tabella
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Elenco dei nomi delle colonne
        /// </summary>
        public List<string> ColumnNames { get; set; }

        /// <summary>
        /// Matrice con gli elementi della tabella
        /// </summary>
        public List<List<object>> Elements { get; set; }

        /// <summary>
        /// Elenco delle colonne della tabella (usata per le espotazioni in excel)
        /// </summary>
        public List<Infrastruttura.Models.Configuration.TableExportConfig> ColumnsConfig { get; set; }

        public TableExport ConvertToTabellaExport()
        {
            return ConvertToTabellaExport(new List<string>());
        }
        public TableExport ConvertToTabellaExport(List<string> Colonne)
        {
            if (Elements == null || Elements.Count == 0)
                return null;
            if (Colonne != null && Colonne.Count > 0)
            {
                var colonneIndexList = Colonne.Select(c => int.Parse(c.Split(',')[0])).ToList();
                var indexColumn = 0;
                foreach (var columnConfig in ColumnsConfig)
                {
                    columnConfig.IsSelected = false;
                }
                foreach (var col in Colonne)
                {
                    var coppia = col.Split(",");
                    var colIndex = int.Parse(coppia[0]);
                    var elementConfig = ColumnsConfig.Where(e => e.OriginalIndexOrder == colIndex).Single();
                    elementConfig.IsSelected = true;
                    elementConfig.CurrentIndexOrder = indexColumn;
                    elementConfig.CurrentColumnName = coppia[1];
                    indexColumn++;
                }
            }
            
            var tab = new TableExport
            {
                Title = new Title { Caption = Title },
                IsShowTitle = true,
                Columns = new List<Column>()
            };
            var index = 0;
            foreach (var cap in ColumnsConfig)
            {
                var columnName = cap.CurrentColumnName ?? cap.OriginalColumnName;
                var col = Column.ConvertToColumn(columnName, index, Elements, cap);
                index++;
                tab.Columns.Add(col);
            }
            return tab;
        }

        public void AddColumn(string tableName, string columnName, int originalIndexOrder)
        {
            if (ColumnNames == null)
            {
                ColumnNames = new List<string>();
            }
            ColumnNames.Add(columnName);
            if (ColumnsConfig == null)
            {
                ColumnsConfig = new List<Infrastruttura.Models.Configuration.TableExportConfig>();
            }
            ColumnsConfig.AddNewColum(tableName, columnName, originalIndexOrder);
        }

        public void SetColumnsByColumnsConfig()
        {
            if (ColumnNames == null)
            {
                ColumnNames = new List<string>();
            }
            foreach (var element in ColumnsConfig.OrderBy(c => c.OriginalIndexOrder))
            {
                ColumnNames.Add(element.OriginalColumnName);
            }
        }
    }
}
