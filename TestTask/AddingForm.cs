using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace TestTask
{
    public partial class AddingForm : Form
    {
        private string sex;
        private bool add;
        private int id;
        private DataManager dm = new DataManager();

        public AddingForm()
        {
            this.add = true;
            InitializeComponent();

            PersonAddLabel.Visible = false;
            PersonReAddLabel.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            comboBox6.Visible = false;
            this.Height = 385;
            AcceptButton.Location = new Point(12, 310);
            CancelButton.Location = new Point(180, 310);


            MaleRadioButton.Checked = true;
            FillComboBoxes();


        }
        public AddingForm(int id)
        {
            add = false;
            this.id = id;
            InitializeComponent();
            Text = "Карточка сотрудника";
            AcceptButton.Text = "Подтвердить";
            FillComboBoxes();

            Person person = dm.FillCard(id);

            //MessageBox.Show(person.GetInfo(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);


            nameTextBox.Text = person.name;
            secondNameTextBox.Text = person.secondName;
            lastNameTextBox.Text = person.lastName;
            if (person.sex == "М") MaleRadioButton.Checked = true; else FemaleRadioButton.Checked = true;
            birthdayTextBox.Text = person.birthday.Day.ToString();
            comboBox1.Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(person.birthday.Month).ToString();
            comboBox2.Text = person.birthday.Year.ToString();
            divisionComboBox.Text = person.division;
            tabNumTextBox.Text = person.tableNum;
            educationComboBox.Text = person.education;

            textBox1.Text = person.contractDate.Day.ToString();
            comboBox4.Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(person.contractDate.Month).ToString();
            comboBox3.Text = person.contractDate.Year.ToString();

            if (person.exContractDate != null)
            {
                textBox2.Text = person.exContractDate.Day.ToString();
                comboBox6.Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(person.exContractDate.Month).ToString();
                comboBox5.Text = person.exContractDate.Year.ToString();
            }

        }

        private void FillComboBoxes()
        {
            List<string> data = dm.LoadDivision();
            foreach (var item in data) { divisionComboBox.Items.Add(item); }
            data = dm.LoadEducation();
            foreach (var item in data) { educationComboBox.Items.Add(item); }

            for (int i = 1; i <= 12; i++)
            {
                comboBox1.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i).ToString());
                comboBox6.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i).ToString());
                comboBox4.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i).ToString());
            }

            for (int i = 0; i < 100; i++)
            {
                comboBox2.Items.Add(1924 + 100 - i);
                comboBox3.Items.Add(1924 + 100 - i);
                comboBox5.Items.Add(1924 + 100 - i);
            }
        }

        public int GetMounthNum(string mounth)
        {
            switch (mounth)
            {
                case "Январь": return 1;
                case "Февраль": return 2;
                case "Март": return 3;
                case "Апрель": return 4;
                case "Май": return 5;
                case "Июнь": return 6;
                case "Июль": return 7;
                case "Август": return 8;
                case "Сентябрь": return 9;
                case "Октябрь": return 10;
                case "Ноябрь": return 11;
                case "Декабрь": return 12;
            }
            return 0;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            if (add)
            {
                if(nameTextBox.Text == "" && secondNameTextBox.Text == "" && tabNumTextBox.Text == "" && birthdayTextBox.Text == "" &&
                    comboBox1.Text == "" && comboBox2.Text == "" && educationComboBox.Text == "" && divisionComboBox.Text == "")
                {
                    MessageBox.Show("Введите все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DateTime date = new DateTime(Int32.Parse(comboBox2.Text), GetMounthNum(comboBox1.Text), Int32.Parse(birthdayTextBox.Text));
                Person person = new Person(nameTextBox.Text, secondNameTextBox.Text, lastNameTextBox.Text, tabNumTextBox.Text, sex, date, divisionComboBox.Text, educationComboBox.Text);

                if (dm.AddPerson(person)) MessageBox.Show("Сотрудник успешно добавлен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Ошибка добавления сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (nameTextBox.Text == "" && secondNameTextBox.Text == "" && tabNumTextBox.Text == "" && birthdayTextBox.Text == "" &&
                    comboBox1.Text == "" && comboBox2.Text == "" && educationComboBox.Text == "" && divisionComboBox.Text == "" && textBox1.Text == "" &&
                    comboBox3.Text == "" && comboBox4.Text == "")
                {
                    MessageBox.Show("Введите все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime date = new DateTime(Int32.Parse(comboBox2.Text), GetMounthNum(comboBox1.Text), Int32.Parse(birthdayTextBox.Text));
                DateTime dateContract = new DateTime(Int32.Parse(comboBox3.Text), GetMounthNum(comboBox4.Text), Int32.Parse(textBox1.Text));
                DateTime dateExContract = new DateTime(Int32.Parse(comboBox5.Text), GetMounthNum(comboBox6.Text), Int32.Parse(textBox2.Text));
                
                Person person;
                if(textBox2.Text == "" && comboBox5.Text == "" && comboBox6.Text == "")
                {
                    person = new Person(nameTextBox.Text, secondNameTextBox.Text, lastNameTextBox.Text, tabNumTextBox.Text, sex, date, divisionComboBox.Text, educationComboBox.Text, dateContract);
                }
                else
                {
                    person = new Person(nameTextBox.Text, secondNameTextBox.Text, lastNameTextBox.Text, tabNumTextBox.Text, sex, date, divisionComboBox.Text, educationComboBox.Text, dateContract, dateExContract);
                }

                if (dm.EditPerson(person, id)) MessageBox.Show("Редактирование успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Ошибка редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e) { Close(); }

        private void MaleRadioButton_CheckedChanged(object sender, EventArgs e) { sex = "М"; }

        private void FemaleRadioButton_CheckedChanged(object sender, EventArgs e) { sex = "Ж"; }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e) { DataManager.on = true; }

        private void birthdayTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }
    }
}
