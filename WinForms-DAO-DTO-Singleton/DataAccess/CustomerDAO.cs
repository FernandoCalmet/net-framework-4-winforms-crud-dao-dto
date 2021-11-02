using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WinForms_DAO_DTO_Singleton.Contracts;
using WinForms_DAO_DTO_Singleton.DataTransfer;
using WinForms_DAO_DTO_Singleton.Entities;

namespace WinForms_DAO_DTO_Singleton.DataAccess
{
    public class CustomerDAO : ConnectionToSql, ICustomerDAO
    {
        /// <summary>
        /// This class inherits from the ConnectionToSql class.
        /// Here the different transactions and queries to the entity's database are carried out
        /// customer.
        /// </summary>
        /// 

        public int Create(Customer customer)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "create_customer_sp";
                    command.Parameters.AddWithValue("@first_name", customer.FirstName);
                    command.Parameters.AddWithValue("@last_name", customer.LastName);
                    command.Parameters.AddWithValue("@address", customer.Address);
                    command.Parameters.AddWithValue("@city", customer.City);
                    command.Parameters.AddWithValue("@email", customer.Email);
                    command.Parameters.AddWithValue("@phone", customer.Phone);
                    command.Parameters.AddWithValue("@job", customer.Job);
                    command.CommandType = CommandType.StoredProcedure;
                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        public int Edit(Customer customer)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update_customer_sp";
                    command.Parameters.AddWithValue("@first_name", customer.FirstName);
                    command.Parameters.AddWithValue("@last_name", customer.LastName);
                    command.Parameters.AddWithValue("@address", customer.Address);
                    command.Parameters.AddWithValue("@city", customer.City);
                    command.Parameters.AddWithValue("@email", customer.Email);
                    command.Parameters.AddWithValue("@phone", customer.Phone);
                    command.Parameters.AddWithValue("@job", customer.Job);
                    command.Parameters.AddWithValue("@id", customer.Id);
                    command.CommandType = CommandType.StoredProcedure;
                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        public int Remove(Customer customer)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "delete_customer_sp";
                    command.Parameters.AddWithValue("@id", customer.Id);
                    command.CommandType = CommandType.StoredProcedure;
                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        public Customer GetById(int id)
        {
            Customer customer = null;

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select_customer_sp";
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var obj = new Customer
                        {
                            Id = (int)reader[0],
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Address = reader[3].ToString(),
                            City = reader[4].ToString(),
                            Email = reader[5].ToString(),
                            Phone = reader[6].ToString(),
                            Job = reader[7].ToString()
                        };

                        customer = obj;
                    }
                }
            }

            return customer;
        }

        public List<CustomerDTO> GetAll()
        {
            var customers = new List<CustomerDTO>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select_all_customers_sp";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var customer = new CustomerDTO
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader[1].ToString(),
                                LastName = reader[2].ToString(),
                                City = reader[4].ToString(),
                                Email = reader[5].ToString(),
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }

        public List<Customer> Search(string name)
        {
            var customers = new List<Customer>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "search_customer_sp";
                    command.Parameters.AddWithValue("@name", name);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer
                            {
                                Id = (int)reader[0],
                                FirstName = reader[1].ToString(),
                                LastName = reader[2].ToString(),
                                Address = reader[3].ToString(),
                                City = reader[4].ToString(),
                                Email = reader[5].ToString(),
                                Phone = reader[6].ToString(),
                                Job = reader[7].ToString()
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }
    }
}