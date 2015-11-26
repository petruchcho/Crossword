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
    public partial class AuthDialogForm : Form
    {
        public interface IAuthDialogResultListener
        {
            void PasswordReceived(string password);
        }

        private IAuthDialogResultListener listener;

        public AuthDialogForm(IAuthDialogResultListener listener)
        {
            InitializeComponent();
            this.listener = listener;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            if (listener != null)
            {
                listener.PasswordReceived(passwordTextBox.Text);
            }
            Close();
        }
    }
}
