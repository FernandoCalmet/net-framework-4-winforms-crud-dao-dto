using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinForms_DAO_DTO_Singleton.Common;
using WinForms_DAO_DTO_Singleton.Contracts;
using WinForms_DAO_DTO_Singleton.DataAccess;
using WinForms_DAO_DTO_Singleton.Entities;
using WinForms_DAO_DTO_Singleton.UI;

namespace WinForms_DAO_DTO_Singleton
{
    public partial class FormCustomers : Form
    {
        #region -> Fields
        private readonly ICustomerDAO customerData = new CustomerDAO();
        private List<Customer> customerList;
        private FormCustomerMaintenance maintenanceForm;
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
        { //Search customers.
            customerList = customerData.Search(name); // Filter customer by value.
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
                FindCustomer(txtSearch.Text);//Find customer if enter key is pressed in search text box.
            }
        }
        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("No data to select", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var customer = GetCustomer((int)dataGridView1.CurrentRow.Cells[0].Value); // Get customer ID and search using GetCustomer(id) method.
                if (customer == null) return;
                var frm = new FormCustomerMaintenance(customer, TransactionAction.View); // Instantiate form, and send parameters (view and action model).
                frm.ShowDialog(); // Show form.
            }
            else
                MessageBox.Show("Please select a row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            maintenanceForm = new FormCustomerMaintenance(new Customer(), TransactionAction.Add);
            maintenanceForm.FormClosed += new FormClosedEventHandler(MaintenanceFormClosed); // Associate closed event, to update the datagridview after the maintenance form is closed.
            maintenanceForm.ShowDialog(); // Show maintenance form.
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("No data to select", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var customer = GetCustomer((int)dataGridView1.CurrentRow.Cells[0].Value); // Get customer ID and search using GetCustomer(id) method.
                if (customer == null) return;
                var frm = new FormCustomerMaintenance(customer, TransactionAction.Edit); // Instantiate form, and send parameters (view and action model).
                frm.ShowDialog(); // Show form.
            }
            else
                MessageBox.Show("Please select a row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0)
            {
                MessageBox.Show("No data to select", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var customer = GetCustomer((int)dataGridView1.CurrentRow.Cells[0].Value);//Get customer ID and search using GetCustomer(id) method.
                if (customer == null) return;

                maintenanceForm = new FormCustomerMaintenance(customer, TransactionAction.Remove); // Instantiate form, and send parameters (view model and action ).
                maintenanceForm.FormClosed += new FormClosedEventHandler(MaintenanceFormClosed); // Associate closed event, to update the datagrdiview after the maintenance form is closed.
                maintenanceForm.ShowDialog(); // Show maintenance form.            
            }
            else
                MessageBox.Show("Please select a row", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //Refresh datagridview
        private void MaintenanceFormClosed(object sender, FormClosedEventArgs e)
        {// Refresh the datagridview after the maintenance form closes.
            var lastData = maintenanceForm.LastRecord; // Get the last record of the maintenance form.
            if (lastData != null) // If you have a last record.
            {
                LoadCustomerData(); // Update the datagridview.
                if (lastData != "") // If the last record field is different from an empty string, then you should highlight and display the added or edited customer.
                {
                    var index = customerList.FindIndex(c => c.FirstName == lastData); // Find the index of the last customer registered or modified.
                    dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[0]; // Focus the cell of the last record.
                    dataGridView1.Rows[index].Selected = true; // Select row.
                    // Note, if you added multiple customers at the same time (Bulk insert) the first record in the customer collection will be selected.
                }
            }
        }
        #endregion
    }
}
