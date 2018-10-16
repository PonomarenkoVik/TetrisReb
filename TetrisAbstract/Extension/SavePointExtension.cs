using System.Collections.Generic;
using System.Data;
using TetrisAbstract.GameClasses;

namespace TetrisAbstract.Extension
{
    public static class SavePointExtension
    {      
        public static DataTable ToDataTable(this List<GameBoardData> source)
        {
            DataTable table = new DataTable("Save points");
            table.Columns.Add(new DataColumn("№"));
            table.Columns.Add(new DataColumn("Date"));
            table.Columns.Add(new DataColumn("Level"));
            table.Columns.Add(new DataColumn("Burned line"));
            table.Columns.Add(new DataColumn("Score"));
            for (int i = 0; i < source.Count; i++)
            {
                var row = table.NewRow();
                row["№"] = i + 1;
                row["Date"] = source[i].Date;
                row["Level"] = source[i].Level + 1;
                row["Burned line"] = source[i].BurnedLine;
                row["Score"] = source[i].Score;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
