using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JsonSerialize
{
    public class DataTable2ChartJson
    {
        public static string DataTable2ChartTable(DataTable myDataTable, string myUnitX, string myUnitY, string myMaxY)
        {
            StringBuilder Json = new StringBuilder();

            if (myDataTable.Rows.Count == 0)
            {
                return "[]";
            }

            Json.Append("{\"total\":" + myDataTable.Columns.Count + ",");
            if (myDataTable.Columns.Count > 0)
            {
                Json.Append("\"rows\":");
                Json.Append(JsonHelper.TransformTableToJson(myDataTable));
                Json.Append(",\"columns\":[");
            }

            Json.Append("{");
            Json.Append("\"title\":\"名称\"");
            Json.Append(", \"field\":\"RowId\"");
            Json.Append("}");

            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                Json.Append(",{");
                Json.Append("\"title\":\"" + (i+1).ToString() + "\"");
                Json.Append(", \"field\":\"Col" + (i+1).ToString() + "\"");
                Json.Append("}");
            }
            Json.Append("],\"Units\":{");
            Json.Append("\"UnitX\":\"" + myUnitX + "\"");
            Json.Append(",\"UnitY\":\"" + myUnitY + "\"");
            Json.Append(",\"MaxY\":\"" + myMaxY + "\"");
            Json.Append("}}");

            return Json.ToString();
        }
    }
}
