using System.Collections.Generic;
using System.Windows.Forms;

namespace ICDcodeSearchTool {
    public class CodeOperator {
        public string Start { get; set; }
        public string Name { get; set; }
        public string End { get; set; }
    }
    public class CodeType {
        public string Start { get; set; }
        public string Val { get; set; }
        public string Name { get; set; }
    }
    public class DescOperator {
        public string Start { get; set; }
        public string Name { get; set; }
        public string End { get; set; }
    }
    public class ChooseFile {
        public string Val { get; set; }
        public string Name { get; set; }
    }
    public class Codes {
        public List<CodeOperator> Data1;
        public List<CodeOperator> Data2;
        public Codes() {
            Data1 = new List<CodeOperator>() {
                new CodeOperator() {Start = "ICD10 LIKE '",     Name = "Starts with",     End = "*'"},
                new CodeOperator() {Start = "ICD10 = '",        Name = "Equals",  End = "'"},
                new CodeOperator() {Start = "ICD10 NOT LIKE '", Name = "Does NOT start with", End = "*'"},
            };
            Data2 = new List<CodeOperator>() {
                new CodeOperator() {Start = "OR ICD10 LIKE '",      Name = "OR starts with",     End = "*'"},
                new CodeOperator() {Start = "OR ICD10 = '",         Name = "OR equals",  End = "'"},
                new CodeOperator() {Start = "AND ICD10 NOT LIKE '", Name = "AND does NOT start with", End = "*'"},
            };
        }
    }
    public class Types {
        public List<CodeType> Data;
        public Types() {
            Data = new List<CodeType>() {
                new CodeType() { Start = "Type IN ", Val = "('0', '1')", Name = "All types"},
                new CodeType() { Start = "Type = ",  Val = "'0'",        Name = "0 - Header"},
                new CodeType() { Start = "Type = ",  Val = "'1'",        Name = "1 - Valid code"}
            };
        }
    }
    public class Descs {
        public List<DescOperator> Data;
        public Descs() {
            Data = new List<DescOperator>() {
                new DescOperator() {Start = " AND Description LIKE '*",     Name = "AND",     End = "*'"},
                new DescOperator() {Start = " OR Description LIKE '*",      Name = "OR",      End = "*'"},
                new DescOperator() {Start = " AND Description NOT LIKE '*", Name = "AND NOT", End = "*'"}
            };
        }
    }
    public class ChFiles
    {
        public List<ChooseFile> Data;
        public ChFiles()
        {
            Data = new List<ChooseFile>()
            {
                new ChooseFile() {Val = "", Name = "   Choose file"},
                new ChooseFile() {Val = @"Data\icd10cm_order_2021.txt",  Name = "ICD-10-CM 2021"},
                new ChooseFile() {Val = @"Data\icd10pcs_order_2021.txt", Name = "ICD-10-PCS 2021"},
                new ChooseFile() {Val = @"Data\icd10pcs_order_2021.txt", Name = "ICD-9-CM 2014"},
                new ChooseFile() {Val = @"Data\icd10pcs_order_2021.txt", Name = "ICD-9-PCS 2014"}
            };
        }
    }
}