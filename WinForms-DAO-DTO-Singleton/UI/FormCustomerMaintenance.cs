using System;
using System.Drawing;
using System.Windows.Forms;
using WinForms_DAO_DTO_Singleton.Common;
using WinForms_DAO_DTO_Singleton.Contracts;
using WinForms_DAO_DTO_Singleton.DataAccess;
using WinForms_DAO_DTO_Singleton.Entities;
using WinForms_DAO_DTO_Singleton.Utils;
using WinForms_DAO_DTO_Singleton.Helpers;

namespace WinForms_DAO_DTO_Singleton.UI
{
    public partial class FormCustomerMaintenance : Form
    {
        #region -> Fields
        private readonly ICustomerDAO customerData;
        private Customer customer;
        private TransactionAction transaction; // Transaction action for persistence.
        private string lastRecord = ""; /*Field to store the last data inserted or edited.
                                          This will allow you to select and view the changes in the datagridview of the Customers form. */
        #endregion

        #region -> Properties
        public string LastRecord
        {/*Property to store the last data inserted or edited.
           This will allow you to select and view the changes in the datagridview of the Customers form.*/
            get { return lastRecord; }
            set { lastRecord = value; }
        }
        #endregion

        #region -> Constructor
        public FormCustomerMaintenance(Customer _customer, TransactionAction _transaction)
        {
            InitializeComponent();

            //Initialize fields
            customerData = new CustomerDAO();
            customer = _customer;
            transaction = _transaction;

            //Initialize control properties
            FillFields(_customer);
            InitializeTransactionUI();
        }
        #endregion

        #region -> Methods
        private void InitializeTransactionUI()
        {//This method is responsible for setting the appearance properties based on the action of the transaction.
            switch (transaction)
            {
                case TransactionAction.View:
                    LastRecord = null;
                    lblTitle.Text = "Customer details";
                    lblTitle.ForeColor = Color.MediumSlateBlue;
                    btnSave.Visible = false;
                    ReadOnlyFields();
                    break;
                case TransactionAction.Add:
                    lblTitle.Text = "Add new customer";
                    lblTitle.ForeColor = Color.SeaGreen;
                    break;
                case TransactionAction.Edit:
                    lblTitle.Text = "Edit customer";
                    lblTitle.ForeColor = Color.RoyalBlue;
                    break;
                case TransactionAction.Remove:
                    lblTitle.Text = "Remove Customer";
                    lblTitle.ForeColor = Color.IndianRed;
                    ReadOnlyFields();
                    break;
            }
        }
        private void ClearFields()
        {//Clear the form fields.
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtJob.Clear();
        }
        private void ReadOnlyFields()
        {//Make the form fields read-only.
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtCity.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtJob.ReadOnly = true;
        }
        private void FillFields(Customer _customer)
        {//Load the data from the customer into the fields of the form.
            txtFirstName.Text = _customer.FirstName;
            txtLastName.Text = _customer.LastName;
            txtAddress.Text = _customer.Address;
            txtCity.Text = _customer.City;
            txtEmail.Text = _customer.Email;
            txtPhone.Text = _customer.Phone;
            txtJob.Text = _customer.Job;
        }
        private Customer FillViewModel()
        {//Fill and return the data of the form fields in a new customer object.
            var customerView = new Customer();

            customerView.Id = customer.Id;
            customerView.FirstName = customer.FirstName;
            customerView.LastName = customer.LastName;
            customerView.Address = customer.Address;
            customerView.City = customer.City;
            customerView.Email = customer.Email;
            customerView.Phone = customer.Phone;
            customerView.Job = customer.Job;

            return customerView;
        }
        private void AddCustomer(Customer _customer)
        {
            var result = customerData.Create(_customer);
            if (result > 0)
            {
                LastRecord = _customer.FirstName;
                MessageBox.Show("Customer added successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                LastRecord = null;
                MessageBox.Show("Operation was not performed, try again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void EditCustomer(Customer _customer)
        {
            var result = customerData.Edit(_customer);
            if (result > 0)
            {
                LastRecord = _customer.FirstName;
                MessageBox.Show("Customer updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                LastRecord = null;
                MessageBox.Show("Operation was not performed, try again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void RemoveCustomer(Customer _customer)
        {
            var result = customerData.Remove(_customer);
            if (result > 0)
            {
                LastRecord = "";
                MessageBox.Show("Customer deleted successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                LastRecord = null;
                MessageBox.Show("Operation was not performed, try again", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void PersistData()
        {
            try
            {
                var customerObject = FillViewModel(); //Get view model.
                var validateData = new DataValidation(customerObject); //Validate fields of the object.

                if (validateData.Result == true)
                {
                    var customerModel = customer;
                    switch (transaction)
                    {
                        case TransactionAction.Add://Add user
                            AddCustomer(customerModel);
                            break;
                        case TransactionAction.Edit://Edit user
                            EditCustomer(customerModel);
                            break;
                        case TransactionAction.Remove://Remove user
                            RemoveCustomer(customerModel);
                            break;
                    }
                }
                else
                {
                    if (validateData.Result == false)
                        MessageBox.Show(validateData.ErrorMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                LastRecord = null;//Set null as last record.
                var message = ExceptionManager.GetMessage(ex);//Get exception message.
                MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);//Show message.
            }
        }
        #endregion

        #region -> Event methods
        private void btnSave_Click(object sender, EventArgs e)
        {
            PersistData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LastRecord = null;
            this.Close();
        }
        #endregion
    }
}
