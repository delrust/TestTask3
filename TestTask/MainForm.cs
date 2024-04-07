using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Spire.Xls;

namespace TestTask
{
    public partial class MainForm : Form
    {
        DataManager dm = new DataManager();
        public MainForm()
        {
            InitializeComponent();

            dm.CreatePersonalTable();
            dm.CreateEducationTable();
            dm.CreateDivisionTable();
            dm.CreateRaisingTable();

            foreach (string[] s in dm.Load()) dataGridView1.Rows.Add(s);
        }

        private void AddingButton_Click(object sender, EventArgs e)
        {
            AddingForm addingForm = new AddingForm();
            addingForm.Show();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (DataManager.on == true)
            {
                dataGridView1.Rows.Clear();
                foreach (string[] s in dm.Load()) dataGridView1.Rows.Add(s);
            }
            DataManager.on = false;
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            dm.DeletePerson(id);

            dataGridView1.Rows.Clear();
            foreach (string[] s in dm.Load())
            {
                dataGridView1.Rows.Add(s);
            }
        }
        private void RedactionButton_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            AddingForm addingForm = new AddingForm(id);
            addingForm.Show();
        }
        private void ExitButton_Click(object sender, EventArgs e) { Close(); }
        private void ReportButton_Click(object sender, EventArgs e)
        {
            List<string[]> data = dm.LoadReising();
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];

            worksheet.Range[1, 1].Value = "№ п/п";
            worksheet.Range[1, 2].Value = "ФИО";
            worksheet.Range[1, 3].Value = "Процент повышения оклада";

            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Range[i + 2, 1].Value = (i + 1).ToString();
                for (int j = 0; j < 2; j++)
                {
                    if(j == 0) worksheet.Range[i+2, j+2].Value = data[i][j];
                    else worksheet.Range[i + 2, j + 2].Value = data[i][j] + "%";
                }
            }
            worksheet.AllocatedRange.AutoFitColumns();
            workbook.SaveToFile("Список на повышение.xlsx", ExcelVersion.Version2016);
            MessageBox.Show("Отчет создан", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (!dataGridView1.Rows[i].Cells[1].Value.ToString().ToLower().Contains(textBox1.Text))
                    dataGridView1.Rows[i].Visible = false;
                else
                    dataGridView1.Rows[i].Visible = true;
            }
        }
    }
}
