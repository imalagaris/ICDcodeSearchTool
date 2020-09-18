using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
// aek
namespace ICDcodeSearchTool
{
    public partial class Form1 : Form
    {
        public DataTable icd10;
        public DataView view;
        public List<CodeOperator> genCode1;
        public List<CodeOperator> genCode2;
        public List<CodeType> genType;
        public List<DescOperator> genDesc;
        public Form1()
        {
            InitializeComponent();

            GridView.ColumnHeadersDefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
            GridView.ColumnCount = 4;
            GridView.Columns[0].Width = 60;
            GridView.Columns[1].Width = 55;
            GridView.Columns[2].Width = 85;
            GridView.Columns[3].Width = 545;
            GridView.Columns[0].Name = GridView.Columns[0].DataPropertyName = "ID";
            GridView.Columns[1].Name = GridView.Columns[1].DataPropertyName = "Type";
            GridView.Columns[2].Name = GridView.Columns[2].DataPropertyName = "ICD10";
            GridView.Columns[3].Name = GridView.Columns[3].DataPropertyName = "Description";
            GridView.AutoGenerateColumns = false;
            GridView.RowTemplate.Height = Font.Height + 3;
            GridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //GridView.RowTemplate.Height = Font.Height + 4;

            genCode1 = (new Codes()).Data1;
            genCode2 = (new Codes()).Data2;
            genType = (new Types()).Data;
            genDesc = (new Descs()).Data;

            combo1.DisplayMember = "Name";
            combo1.DataSource = genCode1;

            combo2.DisplayMember = combo3.DisplayMember = combo4.DisplayMember = "Name";
            combo2.DataSource = (new Codes()).Data2;
            combo3.DataSource = (new Codes()).Data2;
            combo4.DataSource = (new Codes()).Data2;

            combo5.DisplayMember = "Name";
            combo5.ValueMember = "Val";
            combo5.DataSource = genType;

            combo7.DisplayMember  = combo8.DisplayMember = combo9.DisplayMember = "Name";
            combo7.DataSource = (new Descs()).Data;
            combo8.DataSource = (new Descs()).Data;
            combo9.DataSource = (new Descs()).Data;

        }
        private void SetRowNumber(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells["Sr"].Value = row.Index + 1;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void CopyAlltoClipboard()
        {
            GridView.SelectAll();
            DataObject dataObj = GridView.GetClipboardContent();
            GridView.ClearSelection();
            if (dataObj != null) Clipboard.SetDataObject(dataObj);
        }
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) {
                txt1.Enabled = combo1.Enabled = pan2.Visible = true;
            }
            else {
                DisableTxt(txt1);
                DisableTxt(txt2);
                DisableTxt(txt3);
                DisableTxt(txt4);
                DisableCombo(combo1);
                DisableCombo(combo2);
                DisableCombo(combo3);
                DisableCombo(combo4);
                checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;
                pan2.Visible = pan3.Visible = pan4.Visible = false;
            }
        }
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) {
                txt2.Enabled = combo2.Enabled = pan3.Visible = true;
            }
            else {
                DisableTxt(txt2);
                DisableTxt(txt3);
                DisableTxt(txt4);
                DisableCombo(combo2);
                DisableCombo(combo3);
                DisableCombo(combo4);
                checkBox3.Checked = checkBox4.Checked = false;
                pan3.Visible = pan4.Visible = false;
            }
        }
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) {
                txt3.Enabled = combo3.Enabled = pan4.Visible = true;
            }
            else {
                DisableTxt(txt3);
                DisableTxt(txt4);
                DisableCombo(combo3);
                DisableCombo(combo4);
                checkBox4.Checked = false;
                pan4.Visible = false;
            }
        }
        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) {
                txt4.Enabled = combo4.Enabled = true;
            }
            else {
                DisableTxt(txt4);
                DisableCombo(combo4);
            }
        }
        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) { combo5.Enabled = true; }
            else {
                combo5.SelectedIndex = 0;
                combo5.Enabled       = false;
            }
        }
        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) {
                txt6.Enabled = combo6.Enabled = pan7.Visible = true;
            }
            else {
                DisableTxt(txt6);
                DisableTxt(txt7);
                DisableTxt(txt8);
                DisableTxt(txt9);
                DisableCombo(combo6);
                DisableCombo(combo7);
                DisableCombo(combo8);
                DisableCombo(combo9);
                checkBox7.Checked = checkBox8.Checked = checkBox9.Checked = false;
                pan7.Visible = pan8.Visible = pan9.Visible = false;
            }
        }
        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) {
                txt7.Enabled = combo7.Enabled = pan8.Visible = true;
            }
            else {
                DisableTxt(txt7);
                DisableTxt(txt8);
                DisableTxt(txt9);
                DisableCombo(combo7);
                DisableCombo(combo8);
                DisableCombo(combo9);
                checkBox8.Checked = checkBox9.Checked = pan8.Visible = pan9.Visible = false;
            }
        }
        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) {
                txt8.Enabled = combo8.Enabled = pan9.Visible = true;
            }
            else {
                DisableTxt(txt8);
                DisableTxt(txt9);
                DisableCombo(combo8);
                DisableCombo(combo9);
                checkBox9.Checked = pan9.Visible = false;
            }
        }
        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked) {
                txt9.Enabled = combo9.Enabled = true;
            }
            else {
                DisableTxt(txt9);
                DisableCombo(combo9);
            }
        }
        private void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked) {
                txt10.Enabled = txt11.Enabled = true;
                txt10.BackColor = txt11.BackColor = System.Drawing.Color.White;
            }
            else {
                txt10.Text = txt11.Text = string.Empty;
                txt10.Enabled = txt11.Enabled = false;
                txt10.BackColor = txt11.BackColor = System.Drawing.SystemColors.ControlLight;
            }

        }
        private void DisableTxt(System.Windows.Forms.TextBox txtbox)
        {
            txtbox.Text = string.Empty;
            txtbox.Enabled = false;
        }
        private void DisableCombo(System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.SelectedIndex = 0;
            comboBox.Enabled = false;
        }
        private void Apply_Click(object sender, EventArgs e)
        {
            bool ch1 = (checkBox1.Checked & txt1.Text.Length > 0);
            bool ch10 = (checkBox10.Checked & txt10.Text.Length > 0 & txt11.Text.Length > 0);
            bool ch2 = checkBox5.Checked;
            bool ch3 = (checkBox6.Checked & txt6.Text.Length > 0);
            string str, str1, str10, str2, str3;
            str = str1 = str10 = str2 = str3 = string.Empty;

            if (ch1 | ch2 | ch3 | ch10) {
                if (ch1) {
                    int idx1 = combo1.SelectedIndex;
                    str1 = genCode1[idx1].Start + txt1.Text.ToString() + genCode1[idx1].End; 
                    if (checkBox2.Checked & txt2.Text.Length > 0) {
                        int idx2 = combo2.SelectedIndex;
                        str1 = str1 + genCode2[idx2].Start + txt2.Text.ToString() + genCode2[idx2].End;
                        if (checkBox3.Checked & txt3.Text.Length > 0) {
                            int idx3 = combo3.SelectedIndex;
                            str1 = str1 + genCode2[idx3].Start + txt3.Text.ToString() + genCode2[idx3].End;
                            if (checkBox4.Checked & txt4.Text.Length > 0) {
                                int idx4 = combo4.SelectedIndex;
                                str1 = str1 + genCode2[idx4].Start + txt4.Text.ToString() + genCode2[idx4].End;
                            }
                        }
                    }
                    str1 = "(" + str1 + ")";
                }
                if (ch10)
                {
                    str10 = "ICD10 >= '" + txt10.Text.ToString() + "' AND ICD10 <= '" + txt11.Text.ToString() + "'";
                    str10 = "(" + str10 + ")";
                }

                if (ch2) {
                    int idx2 = combo5.SelectedIndex;
                    str2 = "(" + genType[idx2].Start + genType[idx2].Val + ")";
                }

                if (ch3) {
                    str3 = "Description " + combo6.Text.ToString() + " LIKE '*" + txt6.Text.ToString() + "*'";
                    if (checkBox7.Checked & txt7.Text.Length > 0) {
                        int idx7 = combo7.SelectedIndex;
                        str3 = str3 + genDesc[idx7].Start + txt7.Text.ToString() + genDesc[idx7].End;
                        if (checkBox8.Checked & txt8.Text.Length > 0) {
                            int idx8 = combo8.SelectedIndex;
                            str3 = str3 + genDesc[idx8].Start + txt8.Text.ToString() + genDesc[idx8].End;
                            if (checkBox9.Checked & txt9.Text.Length > 0) {
                                int idx9 = combo9.SelectedIndex;
                                str3 = str3 + genDesc[idx9].Start + txt9.Text.ToString() + genDesc[idx9].End;
                            }
                        }
                    }
                    str3 = "(" + str3 + ")";
                }
                if (ch1) { str = str1; }
                if (ch2) { if (ch1)       { str = str + " AND " + str2; } else { str = str2; } }
                if (ch3) { if (ch1 | ch2) { str = str + " AND " + str3; } else { str = str3; } }
                if (ch10) { if (ch1 | ch2 | ch3) { str = str + " AND " + str10; } else { str = str10; } }
                view.RowFilter = str;
                GridView.DataSource = view;
                textBox6.Text = view.Count.ToString();
            }
        }
        private void Copy_Click(object sender, EventArgs e)
        {
            CopyAlltoClipboard();
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            view.RowFilter = string.Empty;
            GridView.DataSource = view;
        }
        private void Load_Click(object sender, EventArgs e)
        {
            string infile = @"C:\Users\iomalaga\Google Drive\ps\ICD10.txt";
            icd10 = DataReader.Read(infile);
            icd10.CaseSensitive = false;
            view = new DataView(icd10);
            GridView.DataSource = view;
            textBox6.Text = view.Count.ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void GridView_Scroll(object sender, ScrollEventArgs e)
        {
            GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
        }
    }
}
