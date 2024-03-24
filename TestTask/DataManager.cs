using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
//1.создать базу данных SQL +
//2.создать в ней таблицы: 
//  -список персонала(код(ID), табельный номер, ФИО, пол, дату рождения, подразделение, образование, дату принятия и дату увольнения; +
//  -справочник "образование"(среднее, высшее и т.п.); +
//  -справочик подразделений; +
//  -список на предстоящее повышение з/п ( работники и % повышения оклада);
//3.написать программу на С#, которая показывает список персонала c возможностьями:
//  -фильтрации данных;
//  -удаления данных; +
//  -вноса новых сотрудников и редактирование существующих через заполнение карточки сотрудника; +
//  -выбор подразделения и образования осуществлять через справочники; +
//  -составление списка на предстоящее повышение з/п;
//  -отчет с выгрузкой в excel "предстоящее повышение з/п";
//  -отчет "Список работников без повышения З/П"(достаточно текст запроса к базе данных);


namespace TestTask
{
    class DataManager
    {
        public SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-5L65RAI;Initial Catalog=TestTask;Integrated Security=True");
        static public bool on = false; 
        public bool OpenConnect()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
            return false;
        }
        public bool CloseConnect()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            return false;
        }

        public bool AddPerson(Person person)
        {
            string sql = $"INSERT INTO Personal (name, secondName, lastName, tableNum, sex, birthday, division, education, contractDate) " +
                         $"VALUES ('{person.name}', '{person.secondName}', '{person.lastName}', '{person.tableNum}', '{person.sex}', " +
                         $"'{person.birthday:yyyy.MM.dd}', '{person.division}', '{person.education}', '{DateTime.Today.ToString("yyyy.MM.dd")}')";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                SqlCommand command = new SqlCommand(sql, sqlConnection);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                on = true;
                return true;
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка AddPerson", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
                
        }
        public bool EditPerson(Person person, int id)
        {
            string query;
            if (person.exContractDate != null)
            {
                query = $"UPDATE Personal SET name = '{person.name}', secondName = '{person.secondName}', lastName = '{person.lastName}'," +
                        $" tableNum = '{person.tableNum}', sex = '{person.sex}', birthday = '{person.birthday}', division = '{person.division}'," +
                        $" education = '{person.education}', contractDate = '{person.contractDate}' WHERE id = {id}";
            }
            else
            {
                query = $"UPDATE Personal SET name = '{person.name}', secondName = '{person.secondName}', lastName = '{person.lastName}'," +
                        $" tableNum = '{person.tableNum}', sex = '{person.sex}', birthday = '{person.birthday}', division = '{person.division}'," +
                        $" education = '{person.education}', contractDate = '{person.contractDate}', contractCloseDate = '{person.exContractDate}' WHERE id = {id}";
            }

            try
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка EditPerson", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }
        public bool DeletePerson(int id)
        {
            string query = $"DELETE FROM Personal WHERE id = {id}";

            try
            {
                OpenConnect();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                if (MessageBox.Show("Вы уверены что хотите удалить сотрудника?", "Удаление сотрудника", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    command.ExecuteNonQuery();
                    CloseConnect();
                    MessageBox.Show("Сотрудник удален из базы данных", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else return false;
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка DeletePerson", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<string[]> Load()
        {
            string query = "SELECT * FROM Personal";

            try
            {
                OpenConnect();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[5]);

                    data[data.Count - 1][0] = reader[0].ToString().Trim();
                    data[data.Count - 1][1] = reader[1].ToString().Trim() + " " + reader[2].ToString().Trim() + " " + reader[3].ToString().Trim();
                    data[data.Count - 1][2] = reader[4].ToString();
                    data[data.Count - 1][3] = reader[5].ToString();

                    data[data.Count - 1][4] = reader[6].ToString();
                }
                reader.Close();
                CloseConnect();
                return data;
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка LoadPerson", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public List<string> LoadDivision()
        {
            List<string> data = new List<string>();
            string query = "SELECT * FROM Divisions";
            
            try
            {
                OpenConnect();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(reader[1].ToString().Trim());
                }
                reader.Close();
                CloseConnect();
                return data;
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка LoadDivisin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public List<string> LoadEducation()
        {
            List<string> data = new List<string>();
            string query = "SELECT * FROM Education";
            
            try
            {
                OpenConnect();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(reader[1].ToString().Trim());
                }
                reader.Close();
                CloseConnect();
                return data;
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка LoadEducation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        public Person FillCard(int id)
        {
            Person person = new Person();
            string query = $"SELECT * FROM Personal WHERE id = {id}";
            
            try
            {
                OpenConnect();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < 11; i++) person.AddInfo(reader[i].ToString().Trim());
                }
                reader.Close();
                CloseConnect();
                return person;
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка FillCard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<string[]> LoadReising()
        {
            string query = "SELECT Personal.name, Personal.secondName, Personal.lastName, RaisingPersonal.persent " +
                           "FROM RaisingPersonal, Personal " +
                           "WHERE RaisingPersonal.idPerson = Personal.id";
            try
            {
                OpenConnect();
                SqlCommand command = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                List<string[]> data = new List<string[]>();
                while (reader.Read())
                {
                    data.Add(new string[2]);
                    data[data.Count - 1][0] = reader[0].ToString().Trim() + " " + reader[1].ToString().Trim() + " " + reader[2].ToString().Trim();
                    data[data.Count - 1][1] = reader[3].ToString();
                }
                reader.Close();
                CloseConnect();
                return data;
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка LoadRaising", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void LoadNoRaising()
        {
            string query = "SELECT Personal.name, Personal.secondName, Personal.lastName " +
                           "FROM Personal LEFT JOIN RaisingPersonal ON RaisingPersonal.idPerson = Personal.id " +
                           "WHERE RaisingPersonal.idPerson IS NULL";
        }
    }
}
