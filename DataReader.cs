using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ICDcodeSearchTool
{
    public class DataReader
    {
        public static DataTable DatStr;
        public static DataColumn C1;
        public static DataColumn C2;
        public static DataColumn C3;
        public static DataColumn C4;
        public static string[] Sep;

        static DataReader()
        {

            DatStr = new DataTable();
            C1 = new DataColumn("ID", typeof(string));
            C2 = new DataColumn("ICD10", typeof(string));
            C3 = new DataColumn("Type", typeof(string));
            C4 = new DataColumn("Description", typeof(string));
            DatStr.Columns.Add(C1);
            DatStr.Columns.Add(C2);
            DatStr.Columns.Add(C3);
            DatStr.Columns.Add(C4);
            C2.MaxLength = 7;
            C3.MaxLength = 1;
            Sep = new string[] { "\r\n" };

        }

        public static DataTable Read(string infile)
        {
            DataTable Data = DatStr.Clone();
            var sr = new StreamReader(infile);
            string txt = sr.ReadToEnd().ToString();
            string[] rows = txt.Split(Sep, StringSplitOptions.None);

            for (int i = 1; i < rows.Count() - 1; i++)
            {
                string[] rowArray = rows[i].Split('\t');
                DataRow row = Data.NewRow();
                row[0] = rowArray[0];
                row[1] = rowArray[1];
                row[2] = rowArray[2];
                row[3] = rowArray[3];
                Data.Rows.Add(row);
            }
            return Data;
        }
    }
}