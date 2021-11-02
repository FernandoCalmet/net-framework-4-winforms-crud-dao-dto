using System.Data.SqlClient;

namespace WinForms_DAO_DTO_Singleton.DataAccess
{
    public abstract class ConnectionToSql
    {
        /// <summary>
        /// This abstract class is responsible for establishing the connection string
        /// and get the connection to SQL.
        /// </summary>
        /// 

        private readonly string connectionString;

        public ConnectionToSql()
        {
            //Set the connection string.
            connectionString = @"Data Source=DESKTOP-HOQ564O\KHANAKAT;Initial Catalog=CustomersDB;Integrated Security=true";
        }

        protected SqlConnection GetConnection()
        {
            //This method is responsible for establishing and returning the connection object to SQL Server.
            return new SqlConnection(connectionString);
        }
    }
}
