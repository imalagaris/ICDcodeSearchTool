using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ICDcodeSearchTool
{
    public class DataReader
    {
        public static DataTable DatStr;
        public static DataColumn C1;
        public static DataColumn C2;
        public static DataColumn C3;
        public static string[] Sep;
        public static int[][] Istr;

        static DataReader()
        {
            DatStr = new DataTable();
            C1 = new DataColumn("ICD10", typeof(string));
            C2 = new DataColumn("Type", typeof(string));
            C3 = new DataColumn("Description", typeof(string));
            DatStr.Columns.Add(C1);
            DatStr.Columns.Add(C2);
            DatStr.Columns.Add(C3);
            C1.MaxLength = 7;
            C2.MaxLength = 1;
            Sep = new string[] { "\r\n" };
            Istr = new int[][] 
            {
                new int[] { 6, 14, 77 }, 
                new int[] { 7, 1, -77 } 
            };
        }
        public static DataTable Read(string infile)
        {
            DataTable Data = DatStr.Clone();
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
    }
}