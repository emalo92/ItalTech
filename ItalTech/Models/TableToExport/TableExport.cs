using System;
using System.Collections.Generic;
using System.Linq;

namespace ItalTech.Models.TableToExport
{
    public class TableExport
    {
        /// <summary>
        /// Titolo della tabella
        /// </summary>
        public Title Title { get; set; }

        /// <summary>
        /// True per visualizzare il titolo
        /// </summary>
        public bool IsShowTitle { get; set; }

        /// <summary>
        /// Matrice con tutte le colonne della tabella ed i valori per ciascuna colonna 
        /// </summary>
        public List<Column> Columns { get; set; }

        /// <summary>
        /// True per visualizzare le righe con colori alternati
        /// </summary>
        public bool IsAlternateRowsColors { get; set; } = true;

        /// <summary>
        /// True per visualizzare il bordo della tabella
        /// </summary>
        public bool IsShowBorder { get; set; } = true;



        /// <summary>
        /// Crea un nuovo oggetto TableExport, a partire dall'oggetto TableExport istanziato, usando solo le colonne indicate in input.
        /// Permette di cambiare il nome e la posizione delle colonne
        /// </summary>
        /// <param name="colonne">Lista di colonne da usare. Ogni elemento è costituito da due parti separate da ','
        /// La prima parte è l'indice in cui compare la colonna nell'oggetto di partenza
        /// La seconda parte è il nome che avrà la colonna</param>
        /// <returns></returns>
        public TableExport CreateByColumns(List<string> colonne)
        {
            if (Columns == null || Columns.Count == 0)
                throw new Exception("Columns is null or empty");

            var newTab = new TableExport() { Title = this.Title, IsShowTitle = true };
            newTab.Columns = new List<Column>();
            var count = 0;
            foreach (var col in Columns.Where(c => c.ColumnConfiguration.IsSelected.HasValue && c.ColumnConfiguration.IsSelected.Value).OrderBy(c => c.ColumnConfiguration.CurrentIndexOrder.Value))
            {
                //var colElements = col.Split(',');
                //var index = int.Parse(colElements[0]);
                //var newName = colElements[1];
                if (!col.ColumnConfiguration.IsSelected.HasValue || !col.ColumnConfiguration.IsSelected.Value)
                {
                    continue;
                }
                var newColumn = new Column
                {
                    Name = col.ColumnConfiguration.CurrentColumnName,
                    IndexOrder = col.ColumnConfiguration.CurrentIndexOrder.Value // count++
                };
                //if (index >= Columns.Count)
                //    throw new Exception($"Indice index: {index} eccede i limiti della lista Columns che ha dim: {Columns.Count}");
                newColumn.Type = col.Type;// Columns[index].Type;
                newColumn.Elements = col.Elements;// Columns[index].Elements.ToList();
                newTab.Columns.Add(newColumn);
            }
            return newTab;
        }
    }
}
