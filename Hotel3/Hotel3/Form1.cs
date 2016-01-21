using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel3
{
    public partial class Form1 : Form
    {
        MySQL sql;
        
        public Form1()
        {
            InitializeComponent();
            //инициализируем переменную - подключаемся к базе
            sql = new MySQL("localhost", "root", "", "hotel2");
        }
        /// <summary>
        /// событие по клику этой архиважной кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {           
           
            
            MessageBox.Show(sql.Scalar("SELECT NOW()")+"\n Скоро Новый 2017 год! \n 100-летие знаменитого выстрела из пушки!\n С наступающим!","Точное время на крейсере 'Аврора'");
        }
        /// <summary>
        /// наша первая MVC выборка SELECT 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable client;
            do client = sql.Select("SELECT id AS NR, client AS FIO, email AS email, phone  FROM Client");
            while (sql.SqlError());
            //MessageBox.Show(client.Rows[1]["client"].ToString());
            dataGridView.DataSource = client;
        }

        /// <summary>
        /// добавление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            do sql.Insert(@"INSERT INTO client (client, email, phone, address, info) 
                       VALUES('Tatiana', 'tttt@gmail.com', '223-322', 'Saratov', 'Newa')");
            while (sql.SqlError());
            //MessageBox.Show("Добавлено");
        }
        /// <summary>
        /// что делается по обработке таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            do label1.Text = sql.Scalar("SELECT COUNT(*) FROM Client");
            while (sql.SqlError());
        }
    }
}
