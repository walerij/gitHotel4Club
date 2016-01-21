using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Hotel3
{
   public class MySQL
    {
        /*параметры соединения (эх, как web.config не хватает)*/
        string host;//имя или ip хоста
        string user;//имя пользователя
        string pass;//пароль пользователя
        string baze;//база данных
        string connectionstring; // переменная для соединения
        
        /**/
        MySqlConnection conn; // коннектор
        public string error { get; private set; } // запись ошибки
        public string query { get; private set; } //запись текста запроса
        MySqlCommand cmd;//обработчик наших команд (мозговой центр этого класса)

      /// <summary>
      /// Конструктор MYSQL  - подключение к БД
      /// </summary>
      /// <param name="host">имя хоста</param>
      /// <param name="user">логин пользователя</param>
      /// <param name="pass">пароль</param>
      /// <param name="baze">база данных</param>
        public MySQL(string host, string user, string pass, string baze)
        {
            this.host = host;
            this.user = user;
            this.pass = pass;
            this.baze = baze;
            this.connectionstring = "SERVER=" + host +
                                    ";DATABASE=" + baze + 
                                    ";UID=" + user +
                                    ";PASSWORD=" + pass+";CHARSET=utf8";
            }


        /// <summary>
        /// Открытие БД
        /// </summary>
        /// <returns></returns>
        protected bool Open()
        {
            error = "";
            try
            {
                conn = new MySqlConnection(connectionstring);
                conn.Open();
                return true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                query = "CONNECTION OPEN: host" +host +" USER: "+user;
                return false;
            }

        }


      /// <summary>
      /// Закрывает БД
      /// </summary>
      /// <returns>true, если закрыли, false если нет</returns>
        protected bool Close()
        {
            try { 
            conn.Close();
            return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                query = "CONNECTION CLOSE: host" + host + " USER: " + user;
                return false;
            }
        }

        /// <summary>
        /// Возвращает скалярное (числовое) значение из запроса query
        /// </summary>
        /// <param name="query">текст SQL запроса</param>
        /// <returns>число, если выполнился запрос или null, если не выполнился (но не наоборот)</returns>
        public string Scalar(string query)
        {
            error = "";
            if (!Open())
                return null;
            string res = "";
            this.query = query;
            try
            {
                cmd = new MySqlCommand(query, conn);
                res = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
            Close();
            return res;
        }

        /// <summary>
        /// Выполняет запрос типа SELECT
        /// </summary>
        /// <param name="query">текст запроса SELECT</param>
        /// <returns></returns>
        public DataTable Select(string query)
        {
            DataTable table = null;
            this.query = query;
            if (!Open()) return table; //не открылось = вернем пустую таблицу
            try
            {
                cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader=cmd.ExecuteReader();
                table = new DataTable("table");
                table.Load(reader);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return table;
            }
            Close(); //закрываем таблицу
            return table;
        }
        
        /// <summary>
        /// Добавление записи в таблицу из БД
        /// </summary>
        /// <param name="query">текст запроса типа insert</param>
        /// <returns>id последней добавл записи, если выполнен или 0 если не выполнен</returns>
        public long Insert(string query)
        {
            int rows = Update(query);
            if (rows > 0)
                return cmd.LastInsertedId;
            else return 0;
        }

        /// <summary>
        /// Изменение записи в таблице БД
        /// </summary>
        /// <param name="query">текст запроса (типа update)</param>
        /// <returns>номер записи, если выполнен, или -1 не выполнен</returns>
        public int Update(string query)
        {
            int rows = 0;
            this.query = query;
            if (!Open()) return -1;
            try {
                cmd = new MySqlCommand(query, conn);
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;
            }
            Close();
            return rows;
         }

        /// <summary>
        /// экранирует пробелы
        /// </summary>
        /// <param name="text">текстовая переменная, где что-то надо экранировать</param>
        /// <returns>текст с заэкранированными некорректными символами</returns>
        public string addslashes(string text)
        {
            return text.Replace("'", "\\'");
        }



        /// <summary>
        /// Обработка ошибок
        /// </summary>
        /// <returns>false, если ошибок нет, и результат обработки ошибок,если вдруг они есть</returns>
        public bool SqlError()
        {
            if (error == "")
                return false; //если ошибки нет, возвращаем false
            DialogResult dr=
            System.Windows.Forms.MessageBox.Show(error+"\n "+
                                                   "Зарпос:"+query
                                                   +"\nAbort: закрыть программу "
                                                   + "\nRetry: повторить запрос" 
                                                   +"\nIgnore:  продолжить выполнение", "Ошибка базы  данных "+user+"@"+host,
                                                   System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore);
            if (dr == DialogResult.Abort)
            {
                Application.Exit();
                return false;
            }
            if (dr == DialogResult.Retry)
                return true;
           
            return false;
        }


    }
}
