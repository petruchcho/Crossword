using System;
using System.Windows.Forms;
using CrosswordApplication.CommonUtils;
using CrosswordApplication.Forms;

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

        private void solvingButton_Click(object sender, EventArgs e)
        {
            FormUtils.OpenFormAndSaveHierarchy(this, new CrosswordEditForm(UserRole.User));
        }
    }
}
