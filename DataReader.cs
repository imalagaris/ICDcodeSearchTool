using System;
using System.Data;
using System.IO;
using System.Linq;

namespace ICDcodeSearchTool
{
    public class DataReader
    {
        public static DataTable DatStr10;
        public static DataTable DatStr9;
        public static string[] Sep;

        static DataReader()
        {
            DataColumn C1 = new DataColumn("ICD10", typeof(string));
            DataColumn C2 = new DataColumn("Type", typeof(string));
            DataColumn C3 = new DataColumn("Description", typeof(string));
            C1.MaxLength = 7;
            C2.MaxLength = 1;
            DatStr10 = new DataTable();
            DatStr10.Columns.Add(C1);
            DatStr10.Columns.Add(C2);
            DatStr10.Columns.Add(C3);
            DataColumn C4 = new DataColumn("ICD10", typeof(string));
            DataColumn C5 = new DataColumn("Description", typeof(string));
            DatStr9 = new DataTable();
            DatStr9.Columns.Add(C4);
            DatStr9.Columns.Add(C5);
            Sep = new string[] { "\r\n" };
        }
        public static DataTable Read10(string infile)
        {
            int[][] Istr = new int[][] 
            {
                new int[] { 6, 14, 77 },
                new int[] { 7, 1, -77 }
            };
            DataTable Data = DatStr10.Clone();
            var sr = new StreamReader(infile);
            string txt = sr.ReadToEnd().ToString();
            string[] rows = txt.Split(Sep, StringSplitOptions.None);
            string[] output = new string[3];

            for (int i = 0; i < rows.Count() - 1; i++)
            {
                output[0] = rows[i].Substring(Istr[0][0], Istr[1][0]);
                output[1] = rows[i].Substring(Istr[0][1], Istr[1][1]);
                output[2] = rows[i].Substring(Istr[0][2], rows[i].Length + Istr[1][2]);
                Data.Rows.Add(output);
            }
            return Data;
        }
        public static DataTable Read9(string infile, bool dx)
        {
            DataTable Data = DatStr9.Clone();
            var sr = new StreamReader(infile);
            string txt = sr.ReadToEnd().ToString();
            string[] rows = txt.Split('\n');
            string[] output = new string[2];
            int k;
            if (dx) { k = 5; } else { k = 4; }

            for (int i = 0; i < rows.Count() - 1; i++)
            {
                output[0] = rows[i].Substring(0, k);
                output[1] = rows[i].Substring(k + 1, rows[i].Length - k - 1);
                Data.Rows.Add(output);
            }
            return Data;
        }
    }
}