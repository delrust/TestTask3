using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask
{
    public partial class OtherForm : Form
    {
        DataManager dm = new DataManager();
        public OtherForm()
        {
            InitializeComponent();
            FillComboBoxes();
            label2.Text = "";
        }

        private void FillComboBoxes()
        {

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            List<string> data = dm.LoadDivision();
            foreach (var item in data) { comboBox1.Items.Add(item); }
            data = dm.LoadEducation();
            foreach (var item in data) { comboBox2.Items.Add(item); }

            List<Person> persons = dm.LoadReisingForEdit();
            foreach (var person in persons) { comboBox4.Items.Add(person.GetFullName()); }
            persons = dm.LoadNoRaising();
            foreach (var person in persons) { comboBox3.Items.Add(person.GetFullName()); }
        }
        private void FillComboBoxAndPersent()
        {
            List<Person> persons = dm.LoadReisingForEdit();
            foreach (var person in persons)
            {
                if (person.GetFullName() == comboBox4.Text)
                {
                    label2.Text = person.reisingPersent.ToString() + "%";
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != null)
                dm.AddDivision(textBox1.Text);
            textBox1.Clear();
            FillComboBoxes();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem != null)
            dm.DeleteDivision(comboBox1.SelectedItem.ToString());
            FillComboBoxes();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
                dm.AddEducation(textBox2.Text);
            textBox2.Clear();
            FillComboBoxes();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
                dm.DeleteEducation(comboBox2.SelectedItem.ToString());
            FillComboBoxes();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxAndPersent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                Person person = new Person(comboBox4.SelectedItem.ToString());
                dm.DeleteFromRaising(person);
                label2.Text = "";
                FillComboBoxes();
                FillComboBoxAndPersent();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Person person = new Person(comboBox3.SelectedItem.ToString(), Int32.Parse(textBox3.Text));
            dm.AddReising(person);
            textBox3.Clear();
            FillComboBoxes();
            FillComboBoxAndPersent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
