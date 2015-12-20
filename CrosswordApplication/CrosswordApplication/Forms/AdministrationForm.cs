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
using CrosswordApplication.Forms;

namespace CrosswordApplication
{
    public partial class AdministrationForm : Form
    {
        public AdministrationForm()
        {
            InitializeComponent();
        }

        private void dictionaryManagerButton_Click(object sender, EventArgs e)
        {
            DictionaryManagerForm dictionaryManagerForm = new DictionaryManagerForm();
            CommonUtils.FormUtils.OpenFormAndSaveHierarchy(this, dictionaryManagerForm);
        }

        private void crosswordManagerButton_Click(object sender, EventArgs e)
        {
            FormUtils.OpenFormAndSaveHierarchy(this, new CrosswordEditForm(UserRole.Administrator));
        }
    }
}
