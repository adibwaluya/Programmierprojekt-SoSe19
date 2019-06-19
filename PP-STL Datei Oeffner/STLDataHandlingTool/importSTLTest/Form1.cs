using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace importSTLTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void stlSelectBt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Browse STL Data",

                CheckFileExists = true,
                CheckPathExists = true,

                Filter = "STL Files|*.stl;*.txt;"

            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                importSTL.DataReader read = new importSTL.DataReader();
                read.ReadASCIIFile(openFile.FileName);
            }
        }
    }
}
