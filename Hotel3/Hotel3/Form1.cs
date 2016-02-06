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
        Model.Client mClient;
        


        public Form1()
        {
            InitializeComponent();
            //инициализируем переменную - подключаемся к базе
            sql = new MySQL("localhost", "root", "", "hotel2");
            mClient = new Model.Client(sql);
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
            dataGridView.DataSource = mClient.SelectClients();
        }

        /// <summary>
        /// добавление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

             mClient = new Model.Client(sql);
            mClient.SetClient("Катя");
            mClient.SetAddress("Н.Ч.");
            mClient.SetInfo("Shie like Egypt");
            mClient.InsertClient();
            MessageBox.Show("Добавлена запись № "+mClient.id);

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView.DataSource = mClient.SelectClients(textBox1.Text);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long id = long.Parse(dataGridView[0, e.RowIndex].Value.ToString());
            Random rand = new Random();
            mClient.SelectClients(id);
            string telephone = rand.Next(000000001, 999999999).ToString();
            MessageBox.Show("Введен телефон:"+telephone);
            mClient.SetPhone(telephone);
            mClient.UPDATEClients(id);
            dataGridView.DataSource = mClient.SelectClients();
        }
    }
}
