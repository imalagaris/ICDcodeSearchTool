using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ICDcodeSearchTool
{
    public partial class Form1 : Form
    {
        public DataSet aek;
        public DataView view;
        public List<CodeOperator> genCode1;
        public List<CodeOperator> genCode2;
        public List<CodeType> genType;
        public List<DescOperator> genDesc;
        public List<ChooseFile> genFile;
        public Form1()
        {
            InitializeComponent();
            aek = new DataSet();
            DataTable icd10cm = DataReader.Read10(@"Data\icd10cm_order_2021.txt");
            DataTable icd10pcs = DataReader.Read10(@"Data\icd10pcs_order_2021.txt");
            DataTable icd9cm = DataReader.Read9(@"Data\CMS32_DESC_LONG_DX.txt", true);
            DataTable icd9pcs = DataReader.Read9(@"Data\CMS32_DESC_LONG_SG.txt", false);
            aek.Tables.Add(icd10cm);
            aek.Tables.Add(icd10pcs);
            aek.Tables.Add(icd9cm);
            aek.Tables.Add(icd9pcs);
            view = null;

            genCode1 = (new Codes()).Data1;
            genCode2 = (new Codes()).Data2;
            genType = (new Types()).Data;
            genDesc = (new Descs()).Data;
            genFile = (new ChFiles()).Data;

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

            combo10.DisplayMember = "Name";
            combo10.ValueMember = "Val";
            combo10.DataSource = (new ChFiles()).Data;
            
            GridView.ColumnHeadersDefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
            GridView.RowTemplate.Height = Font.Height + 3;
            GridView.AutoGenerateColumns = false;
            GridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
        private void GridStyle10()
        {
            GridView.DataSource = null;
            GridView.ColumnCount = 3;
            GridView.Columns[0].Width = 55;
            GridView.Columns[1].Width = 85;
            GridView.Columns[2].Width = 545;
            GridView.Columns[0].Name = GridView.Columns[0].DataPropertyName = "Type";
            GridView.Columns[1].Name = GridView.Columns[1].DataPropertyName = "ICD10";
            GridView.Columns[2].Name = GridView.Columns[2].DataPropertyName = "Description";
            checkBox5.Enabled = combo6.Enabled = true;
            groupBox2.BackColor = Color.White;
        }
        private void GridStyle9()
        {
            GridView.DataSource = null;
            GridView.ColumnCount = 2;
            GridView.Columns[0].Width = 100;
            GridView.Columns[1].Width = 575;
            GridView.Columns[0].Name = "ICD9";
            GridView.Columns[0].DataPropertyName = "ICD10";
            GridView.Columns[1].Name = GridView.Columns[1].DataPropertyName = "Description";
            checkBox5.Enabled = checkBox5.Checked = false;
            combo5.Enabled = false;
            combo5.SelectedIndex = 0;
            groupBox2.BackColor = Color.Gainsboro;

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
                txt1.Enabled = combo1.Enabled = true;
                checkBox2.Visible = txt2.Visible = combo2.Visible = true;
            }
            else {
                DisableControls(checkBox1);
            }
        }
        private void DisableControls(CheckBox check)
        {
            TableLayoutPanel par = (TableLayoutPanel)check.Parent;
            int i = par.Controls.IndexOf(check);

            foreach (var combo in par.Controls.OfType<ComboBox>())
            {
                int j = par.Controls.IndexOf(combo);
                if (j > i)
                {
                    combo.SelectedIndex = 0;
                    combo.Enabled = false;
                    if ( j > i + 2) { combo.Visible = false; }
                }
            }
            foreach (var txt in par.Controls.OfType<TextBox>())
            {
                int j = par.Controls.IndexOf(txt);
                if (j > i)
                {
                    txt.Text = string.Empty;
                    txt.Enabled = false;
                    if ( j > i + 2) { txt.Visible = false; }

                }
            }
            foreach (var ch in par.Controls.OfType<CheckBox>())
            {
                int j = par.Controls.IndexOf(ch);
                if (j > i)
                {
                    ch.Checked = ch.Visible = false;
                }
            }
        }
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) {
                txt2.Enabled = combo2.Enabled = true;
                checkBox3.Visible = txt3.Visible = combo3.Visible = true;
            }
            else {
                DisableControls(checkBox2);
            }
        }
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) {
                txt3.Enabled = combo3.Enabled = true;
                checkBox4.Visible = txt4.Visible = combo4.Visible = true;
            }
            else {
                DisableControls(checkBox3);
            }
        }
        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) {
                txt4.Enabled = combo4.Enabled = true;
            }
            else {
                DisableControls(checkBox4);
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
                txt6.Enabled = combo6.Enabled = true;
                checkBox7.Visible = txt7.Visible = combo7.Visible = true;
            }
            else {
                DisableControls(checkBox6);
            }
        }
        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) {
                txt7.Enabled = combo7.Enabled = true;
                checkBox8.Visible = txt8.Visible = combo8.Visible = true;
            }
            else {
                DisableControls(checkBox7);
            }
        }
        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) {
                txt8.Enabled = combo8.Enabled = true;
                checkBox9.Visible = txt9.Visible = combo9.Visible = true;
            }
            else {
                DisableControls(checkBox8);
            }
        }
        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked) {
                txt9.Enabled = combo9.Enabled = true;
            }
            else {
                DisableControls(checkBox9);
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
            if (view == null) 
            {
                MessageBox.Show("Load a file before applying a filter");
                return; 
            }
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

        }
        private void Load_Click(object sender, EventArgs e)
        {
            int k = combo10.SelectedIndex;
            if (k == 0)
            {
                MessageBox.Show("Please select file to load");
            }
            else
            {
                view = new DataView(aek.Tables[k - 1]);
                if (k > 2) { GridStyle9(); } else { GridStyle10(); }
                GridView.DataSource = view;
                textBox6.Text = view.Count.ToString();
            }
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
