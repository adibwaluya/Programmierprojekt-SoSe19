using System.Windows;
using OpenGlUserControl;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using DataModel;
using Point = DataModel.Point;


namespace ViewModelTest
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // EventHandler for Btn in MAinWindow
        private void CreateAndShowModel_Click(object sender, RoutedEventArgs e)
        {
            var dm = new DataModel.DataModel();
            // Face 1
            dm.AddPoint(1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddPoint(1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddPoint(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddEdge(0, 1);
            dm.AddEdge(0, 2);
            dm.AddEdge(1, 2);
            dm.AddFace(0, 1, 2, new Normal(0.000000e+00, -1.000000e+00, 0.000000e+00));
            // Face 2
            dm.AddPoint(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddPoint(1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddPoint(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddEdge(3, 4);
            dm.AddEdge(3, 5);
            dm.AddEdge(4, 5);
            dm.AddFace(3,4,5,new Normal(0.000000e+00, - 1.000000e+00, 0.000000e+00));
            // Face 3
            dm.AddPoint(1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddPoint(1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddPoint(1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddEdge(6, 7);
            dm.AddEdge(6, 8);
            dm.AddEdge(7, 8);
            dm.AddFace(6, 7, 8, new Normal(1.000000e+00, 0.000000e+00, -0.000000e+00));
            // Face 4
            dm.AddPoint(1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddPoint(1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddPoint(1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddEdge(9, 10);
            dm.AddEdge(9, 11);
            dm.AddEdge(10, 11);
            dm.AddFace(9, 10, 11, new Normal(1.000000e+00, 0.000000e+00, 0.000000e+00));
            // Face 5
            dm.AddPoint(1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddPoint(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddPoint(1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddEdge(12, 13);
            dm.AddEdge(12, 14);
            dm.AddEdge(13, 14);
            dm.AddFace(12, 13, 14, new Normal(0.000000e+00, 0.000000e+00, 1.000000e+00));
            // Face 6
            dm.AddPoint(1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddPoint(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddPoint(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddEdge(15, 16);
            dm.AddEdge(15, 17);
            dm.AddEdge(16, 17);
            dm.AddFace(15, 16, 17, new Normal(0.000000e+00, 0.000000e+00, 1.000000e+00));
            // Face 7
            dm.AddPoint(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddPoint(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddPoint(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddEdge(18, 19);
            dm.AddEdge(18, 20);
            dm.AddEdge(19, 20);
            dm.AddFace(18, 19, 20, new Normal(-1.000000e+00, -0.000000e+00, -0.000000e+00));
            // Face 8
            dm.AddPoint(-1.000000e+01, -1.000000e+01, 1.000000e+01);
            dm.AddPoint(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddPoint(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddEdge(21, 22);
            dm.AddEdge(21, 23);
            dm.AddEdge(22, 23);
            dm.AddFace(21, 22, 23, new Normal(-1.000000e+00, 0.000000e+00, 0.000000e+00));
            // Face 9
            dm.AddPoint(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddPoint(1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddPoint(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddEdge(24, 25);
            dm.AddEdge(24, 26);
            dm.AddEdge(25, 26);
            dm.AddFace(24, 25, 26, new Normal(0.000000e+00, 0.000000e+00, -1.000000e+00));
            // Face 10
            dm.AddPoint(-1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddPoint(1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddPoint(1.000000e+01, -1.000000e+01, -1.000000e+01);
            dm.AddEdge(27, 28);
            dm.AddEdge(27, 29);
            dm.AddEdge(28, 29);
            dm.AddFace(27, 28, 29, new Normal(0.000000e+00, 0.000000e+00, -1.000000e+00));
            // Face 11
            dm.AddPoint(-1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddPoint(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddPoint(1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddEdge(30, 31);
            dm.AddEdge(30, 32);
            dm.AddEdge(31, 32);
            dm.AddFace(30, 31, 32, new Normal(0.000000e+00, 1.000000e+00, 0.000000e+00));
            // Face 12
            dm.AddPoint(1.000000e+01, 1.000000e+01, -1.000000e+01);
            dm.AddPoint(-1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddPoint(1.000000e+01, 1.000000e+01, 1.000000e+01);
            dm.AddEdge(33, 34);
            dm.AddEdge(33, 35);
            dm.AddEdge(34, 35);
            dm.AddFace(33, 34, 35, new Normal(0.000000e+00, 1.000000e+00, -0.000000e+00));
        }
    }
}