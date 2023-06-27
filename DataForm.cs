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
    public partial class DataForm : Form
    {
        public DataForm()
        {
            InitializeComponent();
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();   
            
            MySqlCommand command = new MySqlCommand("SELECT * FROM ТАБЛИЦА", db.GetConnection);
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].HeaderText = "ЗДЕСЬ МОЖНО ПОМЕНЯТЬ НАЗВАНИЕ КОЛОНКИ ДЛЯ datagridview";
            dataGridView1.Columns[1].HeaderText = "ЗДЕСЬ МОЖНО ПОМЕНЯТЬ НАЗВАНИЕ КОЛОНКИ ДЛЯ datagridview";

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            try
            {             
                db.OpenConnection();

                MySqlCommand command = new MySqlCommand("INSERT INTO `ТАБЛИЦА` (`КОЛОНКА`, `КОЛОНКА`, `КОЛОНКА`, `КОЛОНКА`) VALUES (@param1, @param2, @param3, @param4)", db.GetConnection);
                command.Parameters.AddWithValue("@param1", textBox2.Text);
                command.Parameters.AddWithValue("@param2", textBox3.Text);
                command.Parameters.AddWithValue("@param3", textBox4.Text);
                command.Parameters.AddWithValue("@param4", textBox5.Text);
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно добавлена в базу данных.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM ТАБЛИЦА", db.GetConnection);
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].HeaderText = "ЗДЕСЬ МОЖНО ПОМЕНЯТЬ НАЗВАНИЕ КОЛОНКИ ДЛЯ datagridview";
            dataGridView1.Columns[1].HeaderText = "ЗДЕСЬ МОЖНО ПОМЕНЯТЬ НАЗВАНИЕ КОЛОНКИ ДЛЯ datagridview";
        }     

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (MessageBox.Show("Точно удалить данные?", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {                 
                    DataBase db = new DataBase();
                    int id = Convert.ToInt32(row.Cells["АйдиКолонка"].Value);

                    MySqlCommand command = new MySqlCommand("DELETE FROM `ТАБЛИЦА` WHERE `АйдиКолонка` = @id", db.GetConnection);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        db.OpenConnection();
                        command.ExecuteNonQuery();

                        dataGridView1.Rows.Remove(row);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка: " + ex.Message);
                    }
                    finally
                    {
                        db.CloseConnection();
                    }
                }
            }
            MessageBox.Show("Данные удалены!", "Внимание!");
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {                
                DataBase db = new DataBase();
                int id = Convert.ToInt32(row.Cells["АйдиКолонка"].Value);

                MySqlCommand command = new MySqlCommand("UPDATE `Таблица` SET `Колонка1` = @param1, `Колонка2` = @param2 WHERE `АйдиКолонка` = @id", db.GetConnection);
                command.Parameters.AddWithValue("@param1", textBox2.Text);
                command.Parameters.AddWithValue("@param2", textBox3.Text);
                command.Parameters.AddWithValue("@param3", textBox4.Text);
                command.Parameters.AddWithValue("@param4", textBox5.Text);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    db.OpenConnection();

                    //выполянем запрос
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
                finally
                {
                    db.CloseConnection();
                }
            }
            MessageBox.Show("Данные успешно обновлены!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSearch.Text))
            {
                dataGridView1.ClearSelection();
                return;
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBoxSearch.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }
    }
}
