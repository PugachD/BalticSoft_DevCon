using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace BalticSoft_DevTest
{
    public static class DBWork
    {
        static string connectionString = @"Data Source=Dmitriy;Initial Catalog=OrderDB;Integrated Security=True";
        /// <summary>
        /// Выгружает из БД заказ с номером заказа, если такой есть
        /// </summary>
         public static Order LoadOrder(int ID)
        {
            Order order = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlParameter sqlParameter = new SqlParameter("@idOrder", System.Data.SqlDbType.Int);
                sqlParameter.Value = ID;
                string sqlQuery = String.Format("Select IDOrder,DocNumber,TotalAmountOrder,IDClient,Status,IDSupplier From dbo.OrderTable Where IDOrder = @idOrder");
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.Add(sqlParameter);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    if (sqlDataReader.IsDBNull(5) && sqlDataReader.IsDBNull(3))
                        throw new Exception("Столбцы IDSupplier и IDClient одновременно не могут быть null");
                    else if (sqlDataReader.IsDBNull(5))
                        order = new OrderFromBuyer();
                    else if (sqlDataReader.IsDBNull(3))
                        order = new OrderToTheSupplier();
                    order.IDOrder = sqlDataReader.GetInt32(0);
                    order.DocNumber = sqlDataReader.GetInt32(1);
                    order.TotalAmountOrder = sqlDataReader.GetSqlMoney(2);
                    order.IDClient = sqlDataReader.GetInt32(3);
                    order.Status = sqlDataReader.GetString(4);
                    order.IDSupplier = sqlDataReader.GetSqlInt32(5);
                }
            }
            return order;
        }

        public static void InsertOrderInDB(Order order)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = null;
                sqlConnection.Open();

                //Запрос на добавление заказа
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = @"INSERT INTO OrderDB.dbo.OrderTable (DocNumber, TotalAmountOrder, IDClient, Status, IDSupplier) VALUES (" +
                    order.DocNumber + "," + order.TotalAmountOrder.Value.ToString().Replace(",", ".") + "," + order.IDClient + ",'" + order.Status + "'," + order.IDSupplier + ");";
                sqlCommand.ExecuteNonQuery();

                string sqlQuery = String.Format("SELECT Top 1 * FROM OrderDB.dbo.OrderTable ORDER BY IDOrder DESC;");
                sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    order.IDOrder = sqlDataReader.GetInt32(0);
                sqlDataReader.Close();
                Console.WriteLine("Данные успешно добавлены!!!!!!!!!!");
            }
        }

        /// <summary>
        /// Изменение заказа с ID
        /// </summary>
        public static void UpdateOrder(Order order)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = @"UPDATE OrderDB.dbo.OrderTable Set DocNumber = " + order.DocNumber + ", TotalAmountOrder = " +
                    order.TotalAmountOrder.Value.ToString().Replace(",", ".") + ", IDClient = " + order.IDClient + ", Status= " + order.Status +
                    ", IDSupplier= " + order.IDSupplier + " where IDOrder = " + order.IDOrder + ";";
                Console.WriteLine("Данные успешно изменены");
            }
        }

        public static Order CreateOrderFromBuyer(OrderFromBuyer order)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                SqlDataReader sqlDataReader = null;

                Console.WriteLine("Выберите ID Клиента, на которого следует оформить заказ");
                order.IDClient = (SqlInt32)(int.Parse(Console.ReadLine()));

                string sqlQuery = String.Format("Select * From dbo.Client Where IDClient =" + order.IDClient);
                sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (!sqlDataReader.HasRows)
                {
                    Console.WriteLine("Такого клиента нет в базе данных. Добавим.\n");

                    EnterData.EnterDataClient(order);

                    sqlDataReader.Close();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = @"INSERT INTO Client (Name, Adress) VALUES ('" +
                        order.NameClient + "','" + order.AdressClient + "');";
                    sqlCommand.ExecuteNonQuery();

                    sqlQuery = String.Format("SELECT Top 1 * FROM OrderDB.dbo.Client ORDER BY IDClient DESC;");
                    sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                        order.IDClient = sqlDataReader.GetInt32(0);
                }
                sqlDataReader.Close();
            }

            return order;
        }

        public static Order CreateOrderToTheSupplier(OrderToTheSupplier order)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                SqlDataReader sqlDataReader = null;

                Console.WriteLine("ID Поставщика");
                order.IDSupplier = (SqlInt32)(int.Parse(Console.ReadLine()));

                string sqlQuery = String.Format("Select * From dbo.Supplier Where IDSupplier =" + order.IDSupplier);
                sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (!sqlDataReader.HasRows)
                {
                    Console.WriteLine("Такого поставщика нет в Базе Данных. Добавим.\n");

                    EnterData.EnterDataSupplier(order);

                    sqlDataReader.Close();
                    sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = @"INSERT INTO Supplier (INN, PhysicalAddress, LegalAddress) VALUES (" +
                        order.INN + ",'" + order.PhysicalAdress + "','" + order.LegalAdress + "');";
                    sqlCommand.ExecuteNonQuery();

                    sqlQuery = String.Format("SELECT Top 1 * FROM OrderDB.dbo.Supplier ORDER BY IDSupplier DESC;");
                    sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                        order.IDSupplier = sqlDataReader.GetInt32(0);
                }
                sqlDataReader.Close();
            }

            return order;
        }
    }
}
