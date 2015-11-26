using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrosswordApplication
{
    public partial class MainForm : Form, AuthDialogForm.IAuthDialogResultListener
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void administrationButton_Click(object sender, EventArgs e)
        {
            AuthDialogForm dialogForm = new AuthDialogForm(this);
            dialogForm.ShowDialog();
        }

        public void PasswordReceived(string password)
        {
            if (CommonUtils.AuthUtils.CheckAdminPassword(password))
            {
                CommonUtils.FormUtils.OpenFormAndSaveHierarchy(this, new AdministrationForm());
            }
            else
            {
                // TODO Show error
            }
        }
    }
}
