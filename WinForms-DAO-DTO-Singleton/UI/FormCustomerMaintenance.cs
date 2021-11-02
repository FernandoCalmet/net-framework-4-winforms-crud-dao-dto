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
using WinForms_DAO_DTO_Singleton.Entities;

namespace WinForms_DAO_DTO_Singleton.UI
{
    public partial class FormCustomerMaintenance : Form
    {
        #region -> Fields
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
        public FormCustomerMaintenance(Customer customer, TransactionAction _transaction)
        {
            InitializeComponent();


        }
        #endregion

        #region -> Methods

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
