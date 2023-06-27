using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ekzam
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void buttonJoin_Click(object sender, EventArgs e)
        {

            string login = textBox1.Text;
            string password = textBox2.Text;

            DataBase dataBase = new DataBase();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM ТАБЛИЦА WHERE `КОЛОНКАсЛОГИНОМ` = @login AND `КОЛОНКАсПАРОЛЕМ` = @password", dataBase.GetConnection);

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

            dataBase.OpenConnection();
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Вход выполнен");
                DataForm dataForm = new DataForm();
                dataForm.Show();
                this.Hide();
                
            }
            dataBase.CloseConnection();

        }
    }
}
