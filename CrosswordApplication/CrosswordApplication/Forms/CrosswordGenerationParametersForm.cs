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
        private String type;
        private Dictionary.Dictionary dictionary;
        private Action action;


        //delete?
        private String dictionaryPath;
        private int height = 15;
        private int width = 15;

        OpenFileDialog openFileDialog = new OpenFileDialog();


        public CrosswordGenerationParametersForm(String type, Dictionary.Dictionary dictionary, Action action)
        {//type.Equals("Generation"))
            InitializeComponent();

            this.type = type;
            this.action = action;
            this.dictionary = dictionary;
            if (dictionary != null)
            {
                label1.Text += " уже загружен, но можно изменить:";
            }
        }

        public CrosswordGenerationParametersForm(String type, Action action)
        {//type.Equals("Parameters"))

            InitializeComponent();

            this.type = type;
            this.action = action;

            dictionaryFilePathTextBox.Visible = false;
            label1.Visible = false;
            generateButton.Text = "Сохранить";
            this.Text = "Параметры";

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            //TODO verify all parameters

            if (type.Equals("Generation"))
            {
                if (dictionaryFilePathTextBox.Text != "")
                {
                    dictionaryPath = dictionaryFilePathTextBox.Text;
                }
            }

            height = (int)heightNumericUpDown.Value;
            width = (int)widthNumericUpDown.Value;

          //  if (type.Equals("Generation"))
              //  action.Invoke(new Values(height, width, dictionaryPath));
           // else if (type.Equals("Parameters"))
                //action.Invoke(new Values(height, width));

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

    public class Values{
        int height;
        int width;
        String dictionaryPath;

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public string DictionaryPath
        {
            get
            {
                return dictionaryPath;
            }

            set
            {
                dictionaryPath = value;
            }
        }

        public Values(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        public Values(int height, int width, String dictionaryPath)
        {
            this.height = height;
            this.width = width;
            this.dictionaryPath = dictionaryPath;
        }
    }

}
