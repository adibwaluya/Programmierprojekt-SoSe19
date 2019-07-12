using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrorHandling;

namespace importSTLTest
{
    public partial class Form1 : Form
    {
        DataModel.DataStructure dm = new DataModel.DataStructure();

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
                importSTL.DataReader read = new importSTL.DataReader(openFile.FileName);
                dm = read.ReadFile();

                textBox1.Text = Convert.ToString("import finished");
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ErrorFinding errorFinding = new ErrorFinding();
            errorFinding.FindError(dm, new System.Drawing.Color());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; dm.edges.GetEdge(i) != null; i++)
            {
                if (dm.edges.GetEdge(i).CurrentCondition != DataModel.Edge.Condition.NotFaulty)
                {
                    sb.AppendLine("ID " + Convert.ToString(i) + " " + Convert.ToString(dm.edges.GetEdge(i).CurrentCondition));
                }
            }
            sb.AppendLine("Edges not Listed here are not faulty");
            textBox1.Text = Convert.ToString(sb);
        }
    }
}
