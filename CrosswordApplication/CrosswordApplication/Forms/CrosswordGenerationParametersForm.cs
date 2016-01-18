using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrosswordApplication.Forms
{
    public partial class CrosswordGenerationParametersForm : Form
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();


        public CrosswordGenerationParametersForm()
        {
            InitializeComponent();

            /*IF IT PARAMETERS FORM
            dictionaryFilePathTextBox.Visible = false;
            label1.Visible = false;
            generateButton.Text = "Сохранить";
            this.Text = "Параметры";*/
        }



        private void generateButton_Click(object sender, EventArgs e)
        {
            //TODO verify all parameters
        }



        private void openDictionaryFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "DICT|*.dict";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dictionaryFilePathTextBox.Text = openFileDialog.FileName;
            }

        }
    }
}

   