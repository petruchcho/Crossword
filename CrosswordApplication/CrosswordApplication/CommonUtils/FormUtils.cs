using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrosswordApplication.CommonUtils
{
    class FormUtils
    {
        public static void OpenFormAndSaveHierarchy(Form currentForm, Form newForm)
        {
            currentForm.Hide();
            newForm.FormClosed += (s, args) =>
            {
                if (currentForm != null) currentForm.Show();
            };
            newForm.Show();
        }

        public static void OpenFormAndCloseCurrent(Form currentForm, Form newForm)
        {
            currentForm.Hide();
            newForm.FormClosed += (s, args) => { if (currentForm != null) currentForm.Close(); };
            newForm.Show();
        }

        public static void OpenFormOnTop(Form form)
        {
            form.Show();
        }
    }
}
