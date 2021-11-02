using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms_DAO_DTO_Singleton.Common;
using WinForms_DAO_DTO_Singleton.Contracts;
using WinForms_DAO_DTO_Singleton.DataAccess;
using WinForms_DAO_DTO_Singleton.Entities;

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
        #endregion

        #region -> Event methods
        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LastRecord = null;
            this.Close();
        }
        #endregion
    }
}
