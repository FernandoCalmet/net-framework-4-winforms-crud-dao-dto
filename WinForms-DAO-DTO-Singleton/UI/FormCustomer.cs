using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinForms_DAO_DTO_Singleton.Contracts;
using WinForms_DAO_DTO_Singleton.DataAccess;
using WinForms_DAO_DTO_Singleton.Entities;

namespace WinForms_DAO_DTO_Singleton
{
    public partial class FormCustomers : Form
    {
        #region -> Fields
        private readonly ICustomerDAO customerData = new CustomerDAO();
        private List<Customer> customerList;
        #endregion

        #region -> Constructor
        public FormCustomers()
        {
            InitializeComponent();
        }
        #endregion

        #region -> Methods
        private void LoadCustomerData()
        {// Fill the data grid with the list of customers.
            customerList = customerData.GetAll(); // Get all customers.
            dataGridView1.DataSource = customerList; // Set the data source.
        }
        private void FindCustomer(string name)
        { //Search users.
            customerList = customerData.Search(name); // Filter user by value.
            dataGridView1.DataSource = customerList; // Set the data source with the results.
        }
        private Customer GetCustomer(int id)
        {//Get customer by ID.
            var customer = customerData.GetById(id); // Find a single customer.
            if (customer != null) // If there is a result, return a customer object.
                return customer;
            else // Otherwise, return a null value, and show message.
            {
                MessageBox.Show("No customer with id " + id.ToString() + " found", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        #endregion

        #region -> Event methods      
        private void FormCustomers_Load(object sender, EventArgs e)
        {
            LoadCustomerData(); // Load data.
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindCustomer(txtSearch.Text); //Search customer
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindCustomer(txtSearch.Text);//Find user if enter key is pressed in search text box.
            }
        }
        private void btnDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
