using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrosswordApplication.CommonUtils;

namespace CrosswordApplication
{
    public partial class AuthDialogForm : Form
    {
        public AuthDialogForm()
        {
            InitializeComponent();
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            if (AuthUtils.CheckAdminPassword(passwordTextBox.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Неверный пароль", "Ошибка");
            }
        }
    }
}
