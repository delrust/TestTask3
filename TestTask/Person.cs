using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask
{
    class Person
    {
        public string name;
        public string secondName;
        public string lastName;
        public string tableNum;
        public string sex;
        public DateTime birthday;
        public string division;
        public string education;
        public DateTime contractDate;
        public DateTime exContractDate;

        private static int AddCount = -1;

        public Person() { }
        public Person(string name, string secondName, string lastName, string tableNum, string sex, DateTime birthday, string division, string education)
        {
            this.name = name;
            this.secondName = secondName;
            this.lastName = lastName;
            this.tableNum = tableNum;
            this.sex = sex;
            this.birthday = birthday;
            this.division = division;
            this.education = education;
        }
        public Person (string name, string secondName, string lastName, string tableNum, string sex, DateTime birthday, string division, string education, DateTime contractDate) : this(name, secondName, lastName, tableNum, sex, birthday, division, education)
        {
            this.contractDate = contractDate;
        }
        public Person(string name, string secondName, string lastName, string tableNum, string sex, DateTime birthday, string division, string education, DateTime contractDate, DateTime exContractDate) : this(name, secondName, lastName, tableNum, sex, birthday, division, education, contractDate)
        {
            this.exContractDate = exContractDate;
        }

        public void AddInfo(string info)
        {
            AddCount++;
            switch(AddCount)
            {
                case 0: break;
                case 1: name = info; break;
                case 2: secondName = info; break;
                case 3: lastName = info; break;
                case 4: tableNum = info; break;
                case 5: sex = info; break;
                case 6: birthday = new DateTime(); birthday = DateTime.Parse(info); break;
                case 7: division = info; break;
                case 8: education = info; break;
                case 9: contractDate = DateTime.Parse(info); break;
                case 10: if (info == null && info == "") exContractDate = DateTime.Parse(info); AddCount = -1; break;
            }
        }

        public string GetInfo()
        {
            return name + " " + secondName + " " + lastName + "\n" + tableNum + "\n" + sex + "\n" + birthday.ToString() + "\n" + division + "\n" + education +
                "\n" + contractDate.ToString() + " - " + exContractDate.ToString();
        }
    }
}
