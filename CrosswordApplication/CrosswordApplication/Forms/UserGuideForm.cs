using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CrosswordApplication.Forms
{
    public partial class UserGuideForm : Form
    {
        public UserGuideForm()
        {
            InitializeComponent();

            // StreamReader streamReader = new StreamReader(System.IO.Path.GetFullPath(@"..\..\pages") + @"\help 2.html");
            // webBrowser1.DocumentStream = streamReader.BaseStream;
            //OR
            //webBrowser1.Navigate(System.IO.Path.GetFullPath(@"..\..\pages") + @"\help 2.html");
            //webBrowser1.ScrollBarsEnabled = false;

            
        }
    }
}
