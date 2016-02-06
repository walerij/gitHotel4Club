using System.Data;

namespace Hotel3.Model
{
    class Client
    {

        public long id { get; private set; }
        public string client { get; private set; }
        public string email { get; private set; }
        public string phone { get; private set; }
        public string address { get; private set; }
        public string info { get; private set; }

        private MySQL sql;


        public Client(MySQL sql)
        {
            id= 0;
            client = "";
            email = "";
            phone = "";
            address = "";
            info = "";
            this.sql = sql;
        }


        /// <summary>
        /// Установка данных
        /// </summary>
        /// <param name="client">ФИО клиента</param>
       public void SetClient(string client)
        {
            this.client = client;
        }


        /// <summary>
        /// Установка данных
        /// </summary>
        /// <param name="email">email клиента</param>
        public void SetEmailt(string email)
        {
            this.email = email;
        }

        /// <summary>
        /// Установка данных
        /// </summary>
        /// <param name="phone">телефон клиента</param>
        public void SetPhone(string phone)
        {
            this.phone = phone;
        }

        /// <summary>
        /// Установка данных
        /// </summary>
        /// <param name="address">адрес клиента</param>
        public void SetAddress(string address)
        {
            this.address = address;
        }

        /// <summary>
        /// Установка данных
        /// </summary>
        /// <param name="info"> доп информация</param>
        public void SetInfo(string info)
        {
            this.info = info;
        }

        /// <summary>
        /// регистрация нового клиента
        /// </summary>
         public void InsertClient()
            {
            string query = @"INSERT INTO client(client, email, phone, address, info)
                            VALUES(   '"+sql.addslashes(client)+@"', 
                                      '" + sql.addslashes(email) +@"',
                                      '" + sql.addslashes(phone) +@"', 
                                      '" + sql.addslashes(address) +@"',
                                      '" + sql.addslashes(info) +"')  ";
            do this.id= sql.Insert(query);
            while (sql.SqlError());
        


             }

        /// <summary>
        ///Получение списка клиентов 
        /// </summary>
        /// <returns></returns>
        public DataTable SelectClients()
            {
            DataTable client;
            do client = sql.Select(@"SELECT id, client, email, phone, address, info
                  FROM client");
            while (sql.SqlError());
            return client;
        }


        /// <summary>
        ///Получение списка клиентов 
        /// </summary>
        /// <returns></returns>
        public DataTable SelectClients(string find)
        {
            DataTable client;
            do client = sql.Select(@"SELECT id, client, email, phone, address, info
                                     FROM client" + 
                                   @" WHERE client LIKE '%"+find +
                                   @"%' OR email LIKE '%" + find  +
                                   @"%' OR phone LIKE '%" + find +
                                   @"%'  OR address LIKE '%" + find  +
                                   @"%' OR info LIKE '%" + find +
                                   @"%' OR id = '" + find  + "'");
                   while (sql.SqlError());
            return client;
        }



        /// <summary>
        /// загрузить данные для указанного клиента
        /// </summary>
        /// <param name="client_id">идентификатор клиента</param
        ///  <returns>true, если успешно, false, если нет</returns>
        public bool SelectClients(long client_id)
        {
            DataTable client;
            do client = sql.Select(@"SELECT id, client, email, phone, address, info
                                     FROM client  WHERE id = '" + sql.addslashes(client_id.ToString()) + "'");
            while (sql.SqlError());
            if (client.Rows.Count == 0)
                return false;

            this.id = long.Parse(client.Rows[0]["id"].ToString());
            this.client = client.Rows[0]["client"].ToString();
            this.email = client.Rows[0]["email"].ToString();
            this.phone = client.Rows[0]["phone"].ToString();
            this.address = client.Rows[0]["address"].ToString();
            this.info = client.Rows[0]["info"].ToString();
           
            return true;
        }

        /// <summary>
        /// Изменение текущей записи
        /// </summary>
        /// <param name="client_id">идентификатор клиента</param>
        /// <returns>true, если нормально прошло, false если не прошло</returns>
        public bool UPDATEClients(long client_id)
        {
            int result = 0;
            do result = sql.Update(@"UPDATE client  SET address =   '"+ sql.addslashes(this.address) + "',"+
                                                          " phone = '" + sql.addslashes(this.phone) + "'," +
                                                          " email = '" + sql.addslashes(this.email)+"',"+
                                                          " info = '" + sql.addslashes(this.info) + "',"+
                                                          " client = '" + sql.addslashes(this.client) + "'" +
                                                          " WHERE id = '" + sql.addslashes(client_id.ToString()) + "'");
            while (sql.SqlError());

            if (result <= 0)
                return false;
            return true;


        }
       

    }


}
