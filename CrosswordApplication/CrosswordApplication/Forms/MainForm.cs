using System;
using System.Windows.Forms;
using CrosswordApplication.CommonUtils;
using CrosswordApplication.Forms;
using System.Diagnostics;
using System.ComponentModel;

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

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram aboutForm = new AboutProgram();
            aboutForm.ShowDialog();
        }

        private void userguideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UserGuideForm userguide = new UserGuideForm();
            //userguide.Show();

            String text = System.IO.File.ReadAllText(System.IO.Path.GetFullPath(@"..\..\pages") + @"\help.html");
            if (!(text.Contains("<head>") || text.Contains("</head>") || text.Contains("<body>") || text.Contains("</body>")))
            {
                MessageBox.Show("Файл справки некорректен!");
                return;
            }

            try {
                Process.Start(System.IO.Path.GetFullPath(@"..\..\pages") + @"\help.html");
            }
            catch(Win32Exception e1)
            {
                MessageBox.Show("Файл справки отсутствует!");
            }
        }
    }
}
